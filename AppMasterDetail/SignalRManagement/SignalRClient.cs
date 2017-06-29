using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRManagement
{
    public class SignalRClient
    {
        public HubConnection Connection { get; set; }
        private IHubProxy ChatHubProxy;

        public delegate void BroadcastMessageReceived(string senderUserName, string message);
        public delegate void GroupMessageReceived(string senderUserName, string message, string groupName);
        public delegate void UserMessageReceived(string senderUserName, string message);

        public delegate void UserTypingReceived(string message);

        public event BroadcastMessageReceived OnBroadcastMessageReceived;
        public event GroupMessageReceived OnGroupMessageReceived;
        public event UserMessageReceived OnUserMessageReceived;

        public event UserTypingReceived OnUserTypingReceived;

        public SignalRClient(string url, string myUserName)
        {
            Connection = new HubConnection(url, "UserName=" + myUserName);

            Connection.StateChanged += (StateChange obj) =>
            {
               
            };

            ChatHubProxy = Connection.CreateHubProxy("Chat");
            ChatHubProxy.On<string, string>("BroadcastMessageReceived", (senderUserName, message) =>
            {
                OnBroadcastMessageReceived?.Invoke(senderUserName, message);
            });

            ChatHubProxy.On<string, string, string>("GroupMessageReceived", (senderUserName, message, groupName) =>
            {
                OnGroupMessageReceived?.Invoke(senderUserName, message, groupName);
            });

            ChatHubProxy.On<string, string>("UserMessageReceived", (senderUserName, message) =>
            {
                OnUserMessageReceived?.Invoke(senderUserName, message);
            });


            ChatHubProxy.On<string>("UserTypingReceived", (user) =>
            {
                OnUserTypingReceived?.Invoke(user);
            });
        }

        public void SendBroadcastMessage(string senderUserName, string text)
        {
            ChatHubProxy.Invoke("SendBroadcastMessage", senderUserName, text);
        }

        public void SendIsTypingMessage(string senderUserName)
        {
            ChatHubProxy.Invoke("IsTyping", senderUserName);
        }

        public void SendGroupMessage(string senderUserName, string text, string groupName)
        {
            ChatHubProxy.Invoke("SendGroupMessage", senderUserName, text, groupName);
        }

        public void SendUserMessage(string receiverUsername, string senderUserName, string text)
        {
            ChatHubProxy.Invoke("SendUserMessage", receiverUsername, senderUserName, text);
        }

        public void RegisterUser(string myUserName)
        {
            ChatHubProxy.Invoke("Connect", myUserName);
        }


        public Task Start()
        {
            return Connection.Start();
        }

        public void Stop()
        {
            Connection.Stop();
        }

        public bool IsConnectedOrConnecting
        {
            get
            {
                return Connection.State != ConnectionState.Disconnected;
            }
        }

        public ConnectionState ConnectionState { get { return Connection.State; } }
    }
}
