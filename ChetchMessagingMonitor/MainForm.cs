using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chetch.Messaging;

namespace ChetchMessagingMonitor
{
    public partial class MainForm : Form
    {
        CMApplicationContext appCtx;
        Chetch.Messaging.Message MessageSent = null;


        public MainForm(CMApplicationContext ctx)
        {
            InitializeComponent();

            appCtx = ctx;
            
            cmbFilterMessageDirection.Items.Add("All");
            foreach (var dir in Enum.GetValues(typeof(CMDataSource.MessageDirection)))
            {
                cmbFilterMessageDirection.Items.Add(dir.ToString());
            }

            listViewClients.ItemsSource = appCtx.CurrentDataSource.Clients;
            
            listViewMessages.ItemsSource = appCtx.CurrentDataSource.Messages;
            listViewMessages.AddFilter("Target|Sender", listViewClients);
            listViewMessages.AddFilter("Direction", cmbFilterMessageDirection, "All");

            listViewMessages.AddFilter("Type", cbMessageTypeStatus, new Object[] { MessageType.STATUS_REQUEST, MessageType.STATUS_RESPONSE });
            listViewMessages.AddFilter("Type", cbMessageTypePing, new Object[] { MessageType.PING, MessageType.PING_RESPONSE });
            listViewMessages.AddFilter("Type", cbMessageTypeCommand, new Object[] { MessageType.COMMAND, MessageType.COMMAND_RESPONSE });
            listViewMessages.AddFilter("Type", cbMessageTypeError, new Object[] { MessageType.ERROR, MessageType.ERROR_TEST, MessageType.WARNING });

            var mts = listViewMessages.GetFilter("Type").GetAllValues();
            var other = new List<Object>();
            foreach (MessageType mt in Enum.GetValues(typeof(MessageType)))
            {
                if (!mts.Contains(mt)) other.Add(mt);
            }
            listViewMessages.AddFilter("Type", cbMessageTypeOther, other);

            listViewMessages.SelectedIndexChanged += ShowMessageDetails;

            appCtx.CurrentDataSource.Messages.ListChanged += HandleMessage;

            listViewServerConnections.ItemsSource = appCtx.CurrentDataSource.ServerConnections;
        }


        private void HandleException(Exception e)
        {
            MessageBox.Show(e.Message);
        }

        private void HandleMessage(Object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    IList<CMDataSource.MessageData> l = (IList<CMDataSource.MessageData>)sender;
                    var md = l[e.NewIndex];
                    if(MessageSent != null && md.Message.ResponseID == MessageSent.ID)
                    {
                        String text = md.Message.ToStringHeader() + Environment.NewLine + md.Message.ToStringValues(true);
                        PopulateTextBox(tbMessageDetails, text);
                        MessageSent = null;
                    }
                    break;
            }
        }

        private void ShowMessageDetails(Object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if(lv.SelectedItems != null && lv.SelectedItems.Count > 0)
            {
                var md = appCtx.CurrentDataSource.GetMessageData(lv.SelectedItems[0].Name);
                if(md != null)
                {
                    String text = md.Message.ToStringHeader() + Environment.NewLine + md.Message.ToStringValues(true);
                    PopulateTextBox(tbMessageDetails, text);
                }
            }
        }

        
        private void PopulateTextBox(TextBox tb, String text)
        {
            if (tb.InvokeRequired)
            {
                tb.Invoke((MethodInvoker)delegate ()
                {
                    tb.Text = text;
                });
            }
            else
            {
                tb.Text = text;
            }
        }
        
        private void tbCommandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                SendMessage();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage(String sendType = null)
        {
            System.Diagnostics.Debug.Print("Send message");

            try
            {
                if(sendType == null)
                {
                    if (cmbSendType.SelectedItem != null)
                    {
                        sendType = cmbSendType.SelectedItem.ToString();
                    }
                }

                if(sendType == null || sendType == String.Empty)
                {
                    throw new Exception("No send type provided");
                }

                String target = null; 
                if (listViewClients.SelectedItems != null && listViewClients.SelectedItems.Count > 0)
                {
                    target = listViewClients.SelectedItems[0].Name;
                }
                ClientConnection client = appCtx.CurrentClient;
                String commandLine = tbCommandLine.Text;
                List<String> clArgs = commandLine.Split(' ').ToList();
                
                if (client == null)
                {
                    throw new Exception("No client from which to send");
                }

                switch (sendType.Trim().ToUpper())
                {
                    case "PING":
                        MessageSent = client.SendPing(target);
                        break;

                    case "STATUS REQUEST":
                        if (target == null)
                        {
                            MessageSent = client.RequestServerStatus();
                        } else
                        {
                            MessageSent = client.RequestClientConnectionStatus(target);
                        }
                        break;

                    case "COMMAND":
                        if (target != null)
                        {
                            var cmd = clArgs[0];
                            var cmdArgs = clArgs.Count > 1 ? clArgs.GetRange(1, clArgs.Count - 1).ToList<Object>() : null;
                            MessageSent = client.SendCommand(target, cmd, cmdArgs);
                        } else
                        {
                            throw new Exception("Not yet can send commands to server");
                        }
                        break;

                    default:
                        throw new Exception("Send type " + sendType + " not recognised");
                }

                tbMessageDetails.Text = "Sending:" + Environment.NewLine + MessageSent.ToString();
            } catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //PopulateClients();
            //PopulateMessages();
        }

        
        private void btnSendPing_Click(object sender, EventArgs e)
        {
            SendMessage("PING");
        }

        private void btnSendStatusRequest_Click(object sender, EventArgs e)
        {
            SendMessage("STATUS REQUEST");
        }
    }
}
