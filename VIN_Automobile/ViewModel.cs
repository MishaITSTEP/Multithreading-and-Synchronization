using PropertyChanged;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Windows;

namespace VIN_Automobile
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        const int port = 8080;              // порт сервера
        const string address = "127.0.0.1"; // адреса сервера
        public bool IsBlock { get; set; } = false;
        public Request Request { get; set; } = new Request();
        public void Send()
        {
            try
            {
                IsBlock=true;


                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address),port);
                TcpClient client = new TcpClient();
                client.Connect(ipPoint);

                try
                {
                    NetworkStream ns = client.GetStream();
                    // ns.Write() - send data to server
                    // ns.Read()  - get data from the server

                    // серіалізація об'єкта та відправка його
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ns,Request);

                    try
                    {
                        // отримуємо відповідь
                        StreamReader sr = new StreamReader(ns);

                        Request=JsonSerializer.Deserialize<Request>(sr.ReadToEnd())??new Request() {  };
                    } catch { }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } finally
                {
                    client.Close();
                }
            } finally
            {
                IsBlock=false;

            }
        }


    }
}