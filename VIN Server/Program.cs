using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace np_sync_sockets
{
    class Program
    {
        public static List<Request> requests = new List<Request>(
             Enumerable.Range(1,1000)
             .Select(x => new Request() { vin=x.ToString(),info=$"Auto:\nVIN: {x}\nDATE: {DateTime.Now.ToShortTimeString()}" })
             );
        // порт та адреса для приймання вхідних підключень
        static int port = 8080;
        static string address = "127.0.0.1"; // localhost
        static void Main(string[] args)
        {
            // створення кінцевої точки для запуску сервера
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address),port);

            // створюємо сокет на вказаній кінцевій точці
            TcpListener listener = new TcpListener(ipPoint);

            // запуск приймання підключень на сервер
            listener.Start(10);

            while(true)
            {
                Console.WriteLine("Server started! Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient(); // wait until connection

                try
                {
                    while(client.Connected)
                    {
                        NetworkStream ns = client.GetStream();

                        // отримуємо переданий об'єкт та десеріалізуємо його
                        BinaryFormatter formatter = new BinaryFormatter();
                        var request = (Request)formatter.Deserialize(ns);
                        if(request!=null)
                        {
                            var r = requests.FirstOrDefault(x => x.vin==request.vin);
                            // серіалізація об'єкта та відправка його
                            formatter.Serialize(ns,r);
                        }
                    }

                    // закриваємо сокет
                    client.Close();
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            listener.Stop();
        }
    }
}