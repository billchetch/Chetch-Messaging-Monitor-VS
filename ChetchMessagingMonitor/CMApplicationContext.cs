using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chetch.Application;
using Chetch.Messaging;
using Chetch.Services;

namespace ChetchMessagingMonitor
{
    public class CMApplicationContext : SysTrayApplicationContext
    {
        private TCPClientManager _clientMgr = new TCPClientManager();
        private String _currentServer = null;
        private Dictionary<String, ClientConnection> _clients = new Dictionary<String, ClientConnection>();
        private Dictionary<String, CMDataSource> _datasources = new Dictionary<String, CMDataSource>();
        private System.Timers.Timer _timer;

        public ClientConnection CurrentClient
        {
            get
            {
                return _clients.ContainsKey(_currentServer) ? _clients[_currentServer] : null;
            }
        }

        public CMDataSource CurrentDataSource
        {
            get
            {
                return _datasources.ContainsKey(_currentServer) ? _datasources[_currentServer] : null;
            }
        }

        public CMApplicationContext()
        {
            
        }

        private void ConnectClient()
        {
            if (!_clients.ContainsKey(_currentServer))
            {
                var client = _clientMgr.Connect(_currentServer, "CM-Monitor", 10000);
                client.HandleError += HandleClientError;
                client.HandleMessage += HandleClientMessage;
                client.ModifyMessage += ModifyMessage;
                client.Context = ClientConnection.ClientContext.CONTROLLER;
                
                _clients[_currentServer] = client;
                _datasources[_currentServer] = new CMDataSource(_currentServer);

                client.RequestServerStatus();
            }
        }


        public void Init()
        {
            bool startTimer = false;
            if (_currentServer == null)
            {
                _currentServer = "default";
                _clientMgr.AddServer(_currentServer, TCPServer.LocalCS(TCPMessagingServer.CONNECTION_REQUEST_PORT));
                startTimer = true;
            }
            ConnectClient();
            if (startTimer)
            {
                _timer = new System.Timers.Timer(60000);
                // Hook up the Elapsed event for the timer. 
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Start();
            }
        }

        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CurrentClient?.RequestServerStatus();
        }
        
        public void HandleClientError(Connection cnn, Exception e)
        {
           
        }

        private void ModifyMessage(Connection cnn, Chetch.Messaging.Message message)
        {
            LogMessage(CMDataSource.MessageDirection.OUTBOUND, message);
        }
        
        private void LogMessage(CMDataSource.MessageDirection direction, Chetch.Messaging.Message message)
        {
            if (message.Type == Chetch.Messaging.MessageType.TRACE)
            {
                CurrentDataSource.AddTraceData(message);
            }
            else
            {
                CurrentDataSource.AddMessageData(direction, message);
            }
        }

        public void HandleClientMessage(Connection cnn, Chetch.Messaging.Message message)
        {
            //record this
            LogMessage(CMDataSource.MessageDirection.INBOUND, message);

            switch (message.Type)
            {
                case MessageType.SHUTDOWN:
                    break;

                case MessageType.STATUS_RESPONSE:
                    if (message.HasValue("ServerID"))
                    {
                        //send status request to all connected clients
                        var clients = message.GetList<String>("Connections");
                        foreach(var cs in clients)
                        {
                            var data = cs.Split(' ');
                            var clientName = data[1];
                            if (clientName != null && clientName != String.Empty)
                            {
                                CurrentClient.RequestClientConnectionStatus(clientName);
                            }
                        }
                    }
                    break;

                default:
                    break;
            } //ehd switch
        }


        protected override Form CreateMainForm()
        {
            return new MainForm(this);
        }

        override protected void InitializeContext()
        {
            Init();

            base.InitializeContext();

            NotifyIcon.Text = "Chetch Messaging Monitor";
            NotifyIcon.Icon = Properties.Resources.icon_white;
        }
    }
}
