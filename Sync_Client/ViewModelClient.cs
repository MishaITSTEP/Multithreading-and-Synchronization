using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sync_Client
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelClient
    {
        public string text { set; get; }
        public ObservableCollection<ClientMessage> Messagess { get; set; } = new ObservableCollection<ClientMessage>();
        // адрес и порт сервера, к которому будем подключаться
        public int port { set; get; } = 8080; // порт сервера
        public string address { set; get; } = "127.0.0.1"; // адрес сервера

        public ViewModelClient()
        {
        }

        public void Start()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                EndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                // IP4 samples: 123.5.6.3    0.0.255.255    10.7.123.184
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                string message = "";
                while (message != "end")
                {
                    Console.Write("Enter a message: ");
                    message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);

                    socket.SendTo(data, ipPoint);

                    // при використанні UDP протоколу, Connect() лише встановлює дані для відправки
                    //socket.Connect(ipPoint);
                    //socket.Send(data);

                    // получаем ответ получаем сообщение
                    int bytes = 0;
                    string response = "";
                    data = new byte[1024]; // 1KB
                    do
                    {
                        bytes = socket.ReceiveFrom(data, ref remoteIpPoint);
                        response += Encoding.Unicode.GetString(data, 0, bytes);
                    } while (socket.Available > 0);

                    Console.WriteLine("server response: " + response);
                }
                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
