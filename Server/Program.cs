using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void sendThread()
        {

        }
        static void receiveThread()
        {

        }
        static void Main(string[] args)
        {
            Thread receive = new Thread(receiveThread);
            receive.Start();

            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 2222);

            UdpClient sender = new UdpClient(iPEndPoint);
            sender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.MulticastInterface, 1);

            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            sender.JoinMulticastGroup(iPAddress);

            Console.WriteLine("Press ENTER to start sending messages");
            Console.ReadLine();

            Byte[] buffer = null;

            buffer = Encoding.Unicode.GetBytes("hello there");
            sender.Send(buffer, buffer.Length, iPEndPoint);

            for (int i = 0; i <= 8000; i++)
            {
                buffer = Encoding.Unicode.GetBytes(i.ToString());
                sender.Send(buffer, buffer.Length, iPEndPoint);
                Console.WriteLine("Sent " + i);
            }

            Console.WriteLine("All Done! Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}
