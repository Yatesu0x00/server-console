using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1300);
            listener.Start();

            while (true) 
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client accepted");

                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());

                try
                {
                    byte[] buffer = new byte[1024];

                    stream.Read(buffer, 0, buffer.Length);
                    //Count the length of the message as bytes
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0) 
                        {
                            recv++;
                        }
                    }

                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("request received: " + request);
                    sw.WriteLine("Response: Hey I need to tell you that IT WOOOOORKS! ＼(^o^)／");
                    sw.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    sw.WriteLine(ex.ToString());
                }
            }
        }
    }
}
