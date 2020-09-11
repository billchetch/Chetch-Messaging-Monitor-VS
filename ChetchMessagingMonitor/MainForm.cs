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
using System.Diagnostics;

namespace ChetchMessagingMonitor
{
    

    public partial class MainForm : Form
    {
        CMApplicationContext appCtx;
        Chetch.Messaging.Message MessageSent = null;
        Chetch.Messaging.Message ServerCommandSent = null;
        Trace2Form _traceListener = null;

        public MainForm(CMApplicationContext ctx)
        {
            InitializeComponent();

            appCtx = ctx;

            _traceListener = new Trace2Form();
            Trace.Listeners.Add(_traceListener);
            listViewTraceOutput.Trace = false;
            listViewTraceOutput.ItemsSource = _traceListener.Lines;
            listViewTraceOutput.PrependItems = true;
            listViewTraceOutput.AddFilter("Line", tbSearchTrace);

            //CLIENTS TAB
            //clients list view
            listViewClients.ItemsSource = appCtx.CurrentDataSource.Clients;
            
            //messages list view
            listViewMessages.ItemsSource = appCtx.CurrentDataSource.Messages;
            listViewMessages.PrependItems = true;

            listViewMessages.AddFilter("Target|Sender", listViewClients);
            cmbFilterMessageDirection.Items.Add("All");
            foreach (var dir in Enum.GetValues(typeof(CMDataSource.MessageDirection)))
            {
                cmbFilterMessageDirection.Items.Add(dir.ToString());
            }
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
            
            cmbSendType.SelectedIndex = 0;


            //SERVER TAB
            //connection strings to different servers
            cmbServerConnection.DataSource = appCtx.ServerConnectionStrings;

            //server connections list view
            listViewServerConnections.ItemsSource = appCtx.CurrentDataSource.ServerConnections;
            
            foreach (Server.CommandName cmd in Enum.GetValues(typeof(Server.CommandName)))
            {
                if (cmd == Server.CommandName.NOT_SET) continue;
                cmbServerCommands.Items.Add(cmd.ToString());
            }

            //FORM catch data changes
            appCtx.CurrentDataSource.Messages.ListChanged += HandleMessage;
            appCtx.CurrentDataSource.PropertyChanged += HandlePropertyChanged;

            Trace.WriteLine("MainForm created");
        }


        private void HandleException(Exception e)
        {
            MessageBox.Show(e.Message);
        }

        private void HandlePropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            CMDataSource dataSource = (CMDataSource)sender;
            switch (e.PropertyName)
            {
                case "ServerDetails":
                    PopulateTextBox(tbServerDetails, dataSource.Get<String>(e.PropertyName));
                    break;

                case "TraceOutput":
                    PopulateTextBox(tbTraceServerOutput, dataSource.Get<String>(e.PropertyName) + Environment.NewLine, dataSource.Trace.Count > 0);
                    break;
            }
        }

        private void HandleMessage(Object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    //showing response
                    IList<CMDataSource.MessageData> l = (IList<CMDataSource.MessageData>)sender;
                    var md = l[e.NewIndex];
                    if(MessageSent != null && md.Message.ResponseID == MessageSent.ID)
                    {
                        PopulateMessageDetails(md);
                        MessageSent = null;
                    }
                    if (ServerCommandSent != null && md.Message.ResponseID == ServerCommandSent.ID)
                    {
                        String text = md.Message.ToStringHeader() + Environment.NewLine + md.Message.ToStringValues(true);
                        PopulateTextBox(tbServerCommandResponse, text);
                        ServerCommandSent = null;
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
                    PopulateMessageDetails(md);
                }
            }
        }

        private void PopulateMessageDetails(CMDataSource.MessageData md)
        {
            PopulateTextBox(tbMessageDetailsHeader, md.Message.ToStringHeader());
            PopulateTextBox(tbMessageDetails, md.Message.ToStringValues(true));
        }

        public void PopulateTextBox(TextBox tb, String text, bool append = false)
        {
            if(tb.IsDisposed)return;

            if (tb.InvokeRequired)
            {
                tb.Invoke((MethodInvoker)delegate ()
                {
                    if (append)
                    {
                        tb.AppendText(text);
                    }
                    else
                    {
                        tb.Text = text;
                    }
                });
            }
            else
            {
                if (append)
                {
                    tb.AppendText(text);
                }
                else
                {
                    tb.Text = text;
                }
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

                    case "SUBSCRIBE":
                        if (target == null)
                        {
                            throw new Exception("Please select client to subscribe to");
                        }
                        else
                        {
                            MessageSent = client.Subscribe(target);
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
                            throw new Exception("Use server tab to send commands to server");
                        }
                        break;

                    default:
                        throw new Exception("Send type " + sendType + " not recognised");
                }

                tbMessageDetails.Text = "Sending:" + Environment.NewLine + MessageSent.ToStringValues(true);
                tbMessageDetailsHeader.Text = "Sending:" + Environment.NewLine + MessageSent.ToStringHeader();
            } catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SendMessage("STATUS REQUEST");
            listViewClients.ClearItems(true);
            listViewMessages.PopulateItems();
            listViewServerConnections.PopulateItems();

            PopulateTextBox(tbServerDetails, appCtx.CurrentDataSource.ServerDetails);
        }

        
        private void btnSendPing_Click(object sender, EventArgs e)
        {
            SendMessage("PING");
        }

        private void btnSendStatusRequest_Click(object sender, EventArgs e)
        {
            SendMessage("STATUS REQUEST");
        }

        private void cbTraceServerOutput_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                //appCtx.CurrentDataSource.TraceOutput = "YEP tracing";
                appCtx.CurrentDataSource.Trace.Clear();
                appCtx.CurrentDataSource.TraceOutput = "";
                appCtx.CurrentClient.StartTracingToClient();
            } else
            {
                //appCtx.CurrentDataSource.TraceOutput = "YEP nnot tracing";
                appCtx.CurrentClient.StopTracingToClient();
            }
        }

        private void SendServerCommand()
        {
            try
            {
                var client = appCtx.CurrentClient;
                if(cmbServerCommands.SelectedIndex == -1)
                {
                    throw new Exception("Please select a command");
                }
                var s = cmbServerCommands.SelectedItem;
                Server.CommandName scmd = (Server.CommandName)Enum.Parse(typeof(Server.CommandName), s.ToString());
                List<Object> args = new List<object>();
                switch (scmd)
                {
                    case Server.CommandName.CLOSE_CONNECTION:
                        if(listViewServerConnections.SelectedItems.Count > 0)
                        {
                            args.Add(listViewServerConnections.SelectedItems[0].Name);
                        } else
                        {
                            throw new Exception("Please select a connection");
                        }
                        break;

                    default:
                        args = tbServerCommandLine.Text.Split(' ').ToList<Object>();
                        break;
                }
                
                ServerCommandSent = client.SendServerCommand(scmd, args);
            } catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void btnSendServerCommand_Click(object sender, EventArgs e)
        {
            SendServerCommand();
        }

        private void cmbSendType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tabClients":
                    SendMessage("STATUS REQUEST");
                    listViewClients.ClearItems(true);
                    listViewMessages.PopulateItems();
                    break;

                case "tabServer":
                    listViewServerConnections.PopulateItems();
                    break;
            }
        }

        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            listViewServerConnections.ClearItems(true);
            listViewClients.ClearItems(true);
            listViewMessages.ClearItems(true);
            appCtx.CurrentDataSource.Messages.ListChanged -= HandleMessage;
            appCtx.CurrentDataSource.PropertyChanged -= HandlePropertyChanged;

            try
            {
                String cnnString = cmbServerConnection.Text;
                if (cnnString == null || cnnString == String.Empty) throw new Exception("Please supply a connection string");
                appCtx.Init(cnnString);
                MessageBox.Show("Connection to " + cnnString + " successful.");

                //server connections listview
                listViewServerConnections.ItemsSource = appCtx.CurrentDataSource.ServerConnections;
                listViewServerConnections.PopulateItems();

                //client connections list view
                listViewClients.ItemsSource = appCtx.CurrentDataSource.Clients;
                listViewClients.PopulateItems();

                //messages list view
                listViewMessages.ItemsSource = appCtx.CurrentDataSource.Messages;
                listViewMessages.PopulateItems();

                //because data source has changed we must update event handlers to catch incoming messages
                appCtx.CurrentDataSource.Messages.ListChanged += HandleMessage;
                appCtx.CurrentDataSource.PropertyChanged += HandlePropertyChanged;

                //updates server connection strings list
                cmbServerConnection.DataSource = null;
                cmbServerConnection.DataSource = appCtx.ServerConnectionStrings;

                //show new server connection in text box
                PopulateTextBox(tbServerDetails, appCtx.CurrentDataSource.ServerDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearMessages_Click(object sender, EventArgs e)
        {
            listViewMessages.ClearItems(true);
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            SendMessage("SUBSCRIBE");
        }

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            SendMessage("UNSUBSCRIBE");
        }
    }
}
