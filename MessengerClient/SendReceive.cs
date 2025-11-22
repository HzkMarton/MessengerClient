using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
namespace MessengerClient
{
    internal class SendReceive
    {
        public static NetworkStream stream;
        public static string Message = null;
        private struct Credentials
        {
            public int UserID { get; set; }
            public string UUID { get; set; }
        }
        public static bool UserAuthenticated = false;
        bool listen = true;
        private class MessageHandler //változtatás internal-ról!!!
        {
            public string Content { get; set; }
            public int SenderID { get; set; }
            public string Nickname { get; set; }
        }
        public class UserHandler
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Nickname { get; set; }
            public bool IsLogin { get; set; }
        }
        public static UserHandler User = new UserHandler();
        public SendReceive()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 45000);
                stream = client.GetStream();
                Thread backGroundThread = new Thread(new ThreadStart(UpdateMessage));
                backGroundThread.IsBackground = true;
                backGroundThread.Name = "MessageUpdater";
                backGroundThread.Start();
            }
            catch (Exception e)
            {
                //MainWindow.messageTextBlock.Text += e.Message;
            }
            
        }
        private void SendMessage()
        {
            MessageHandler m = new MessageHandler();
            m.SenderID = credentials.UserID;
            m.Content = Message;
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(m));
            stream.Write(data, 0, data.Length);
        }
        public static void SendCredentials()
        {
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(User));
            stream.Write(data, 0, data.Length);
        }
        public void Stop()
        {
            listen = false;
        }
        private Credentials credentials = new Credentials();
        private void UpdateMessage()
        {
            do
            {
                if (stream.DataAvailable)
                {
                    byte[] data = new byte[1024];
                    int length = stream.Read(data, 0, data.Length);
                    string d = Encoding.UTF8.GetString(data, 0, length);
                    credentials = JsonSerializer.Deserialize<Credentials>(d);
                    if (credentials.UUID != null)
                    {
                        UserAuthenticated = true;
                        data = Crc32.Hash(Encoding.UTF8.GetBytes($"{credentials.UUID.Replace("-", "")}:{credentials.UserID}"));
                        stream.Write(data, 0, data.Length);
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Login.Helytelen();
                        });
                    }
                    
                }
                } while (credentials.UUID == null);
            List<MessageHandler> messages = new List<MessageHandler>();
            do
            {
                if (stream.DataAvailable)
                {
                    byte[] data = new byte[4];
                    int read = 0;
                    while (read < 4)
                    {
                        int bytes = stream.Read(data, read, 4 - read);
                        if (bytes == 0) throw new IOException("A hálozati kapcsolat le lett zárva az olvasás alatt");
                        read += bytes;
                    }
                    int length = BitConverter.ToInt32(data, 0);
                    data = new byte[length];
                    read = 0;
                    while (read < length)
                    {
                        int bytes = stream.Read(data, 0, length-read);
                        if (bytes == 0) throw new IOException("A hálozati kapcsolat le lett zárva az olvasás alatt");
                        read += bytes;
                    }
                    messages = JsonSerializer.Deserialize<List<MessageHandler>>(Encoding.UTF8.GetString(data, 0, length));
                    foreach (MessageHandler m in messages)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MainWindow.AddBubble(m.Content, m.Nickname, m.SenderID == credentials.UserID);
                        });
                    }
                    messages.Clear();
                        
                    
                }
                else if (Message != null)
                {
                    SendMessage();
                    Message = null;
                }
                //Thread.Sleep(300);
            } while (listen);
        }
    }   
}

