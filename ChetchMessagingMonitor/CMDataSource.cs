using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Chetch.Messaging;

namespace ChetchMessagingMonitor
{
    public class CMDataSource : Chetch.Utilities.DataSourceObject
    {
        public class ClientData : Chetch.Utilities.DataSourceObject
        {
            public String ID { get { return Get<String>(); } set { Set(value); } }
            public String Name { get { return Get<String>(); } set { Set(value); } }
            public String State { get { return Get<String>(); } set { Set(value); } }
            public String Context { get { return Get<String>(); } set { Set(value); } }
            public int MessagesReceived { get { return Get<int>(); } set { Set(value); } }
            public int GarbageReceived { get { return Get<int>(); } set { Set(value); } }
            public int MessagesSent { get { return Get<int>(); } set { Set(value); } }

            public ClientData(Message message)
            {
                ParseMessage(message);
            }

            public void ParseMessage(Message message)
            {
                ID = message.GetString("ConnectionID");
                Name = message.GetString("Name");
                State = message.GetString("State");
                Context = message.GetString("Context");
                MessagesReceived = message.GetInt("MessagesReceived");
                GarbageReceived = message.GetInt("GarbageReceived");
                MessagesSent = message.GetInt("MessagesSent");
            }
        }


        const int MESSAGE_LOG_MAX = 256;

        public enum MessageDirection
        {
            INBOUND,
            OUTBOUND
        }

        public class MessageData : Chetch.Utilities.DataSourceObject
        {
            public String ID { get { return Get<String>(); } set { Set(value); } }
            public MessageDirection Direction { get { return Get<MessageDirection>(); } set { Set(value); } }
            public MessageType Type { get { return Get<MessageType>(); } set { Set(value); } }
            public String Target { get { return Get<String>(); } set { Set(value); } }
            public String Sender { get { return Get<String>(); } set { Set(value); } }
            public String Summary { get { return Get<String>(); } set { Set(value); } }

            public Message Message;
            long Timestamp;

            public MessageData(MessageDirection direction, Chetch.Messaging.Message message)
            {
                ID = message.ID;
                Direction = direction;
                Type = message.Type;
                Target = message.Target;
                Sender = message.Sender;
                Summary = message.Value;

                Message = message;
                Timestamp = DateTime.Now.Ticks;
            }
        }

        public class ServerConnectionData : Chetch.Utilities.DataSourceObject
        {
            public String ID { get { return Get<String>(); } set { Set(value); } }
            public String Name { get { return Get<String>(); } set { Set(value); } }
            public String ConnectionType { get { return Get<String>(); } set { Set(value); } }
            public String State { get { return Get<String>(); } set { Set(value); } }
            public String Extras { get { return Get<String>(); } set { Set(value); } }

            public ServerConnectionData(String connectionType, String connectionDataString)
            {
                ConnectionType = connectionType;
                ParseConnectionDataString(connectionDataString);
            }

            public void ParseConnectionDataString(String connectionDataString)
            {
                var parts = connectionDataString.Split(' ').ToList();
                ID = parts[0];
                Name = parts[1];
                State = parts[2];
                if (parts.Count > 3)
                {
                    Extras = String.Join(" ", parts.GetRange(3, parts.Count - 3).ToArray());
                }
            }
        }

        public class TraceData : Chetch.Utilities.DataSourceObject
        {
            public String TraceMessage;
            long Timestamp = DateTime.Now.Ticks;

            public TraceData(String message)
            {
                TraceMessage = message;
            }
        }

        public String ServerName { get { return Get<String>(); } set { Set(value); } }
        public String ServerID { get { return Get<String>(); } set { Set(value); } }
        public String ServerDetails { get { return Get<String>(); } set { Set(value); } }

        public int MaxConnections { get { return Get<int>(); } set { Set(value); } }
        public int ConnectionsCount { get { return Get<int>(); } set { Set(value); } }
        public int RemainingConnections { get { return Get<int>(); } set { Set(value); } }

        public BindingList<ClientData> Clients { get; }  = new BindingList<ClientData>();
        public BindingList<MessageData> Messages { get; } = new BindingList<MessageData>();
        public BindingList<ServerConnectionData> ServerConnections { get; } = new BindingList<ServerConnectionData>();
        public BindingList<TraceData> Trace { get; } = new BindingList<TraceData>();
        public String TraceOutput { get { return Get<String>(); } set { Set(value); } }


        public CMDataSource(String serverName)
        {
            ServerName = serverName;
            System.Diagnostics.Debug.Print("CMDataSource constructor called for " + ServerName);
        }

        public void AddClientData(Message message)
        {
            ClientData cd = new ClientData(message);
            foreach(var c in Clients)
            {
                if(c.ID == cd.ID)
                {
                    c.ParseMessage(message);
                    return;
                }
            }

            System.Diagnostics.Debug.Print("Adding client " + cd.Name);
            Clients.Add(cd);
        }

        public void AddMessageData(MessageDirection direction, Message message)
        {
            var md = new MessageData(direction, message);
            System.Diagnostics.Debug.Print("Adding " + direction + " message " + message.ID + " " + message.Type);
            Messages.Insert(0, md);
            if(Messages.Count > MESSAGE_LOG_MAX)
            {
                Messages.RemoveAt(MESSAGE_LOG_MAX);
            }

            if(message.HasValue("ServerID") && message.Type == MessageType.STATUS_RESPONSE)
            {
                ServerID = message.GetString("ServerID");
                MaxConnections = message.GetInt("MaxConnections");
                ConnectionsCount = message.GetInt("ConnectionsCount");
                RemainingConnections = MaxConnections - ConnectionsCount;
                ServerDetails = String.Format("{0}: {1} Connections made, {2} Remaining", ServerID, ConnectionsCount, RemainingConnections);
                
                AddServerConnectionData("PRIMARY", message.GetString("PrimaryConnection"));
                foreach (var s in message.GetList<String>("SecondaryConnections"))
                {
                    AddServerConnectionData("SECONDARY", s);
                }
                foreach (var s in message.GetList<String>("Connections"))
                {
                    AddServerConnectionData("CLIENT", s);
                }
            }
        }

        public MessageData GetMessageData(String ID)
        {
            foreach (var md in Messages)
            {
                if (md.ID == ID) return md;
            }
            return null;
        }

        public void AddServerConnectionData(String connectionType, String connectionDataString)
        {
            var scd = new ServerConnectionData(connectionType, connectionDataString);
            
            foreach(var cnd in ServerConnections)
            {
                if(cnd.ID == scd.ID)
                {
                    cnd.ParseConnectionDataString(connectionDataString);
                    return;
                }
            }
            
            System.Diagnostics.Debug.Print("Adding server connection " + scd.ID);
            ServerConnections.Add(scd);
        }
        
        public void AddTraceData(Message message)
        {
            if(message.Type == MessageType.TRACE)
            {
                System.Diagnostics.Debug.Print("Adding trace " + message.Value);
                var td = new TraceData(message.Value);
                String traceOutput;
                if (Trace.Count > MESSAGE_LOG_MAX)
                {
                    Trace.RemoveAt(0);
                }
                //assign here to notify properties
                TraceOutput = td.TraceMessage;
                Trace.Add(td);
            }
        }
    }
}
