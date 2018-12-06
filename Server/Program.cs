using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            udpClient.JoinMulticastGroup(iPAddress);

            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2222);

            Console.WriteLine("Press ENTER to start sending messages");
            Console.ReadLine();

            Byte[] buffer = null;

            buffer = Encoding.Unicode.GetBytes("hello there");
            udpClient.Send(buffer, buffer.Length, iPEndPoint);

            for (int i = 0; i <= 8000; i++)
            {
                buffer = Encoding.Unicode.GetBytes(i.ToString());
                udpClient.Send(buffer, buffer.Length, iPEndPoint);
                Console.WriteLine("Sent " + i);
            }

            Console.WriteLine("All Done! Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}
