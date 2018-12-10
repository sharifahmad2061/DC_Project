using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading;
namespace Client
{
    class Program
    {
        //shared for received data
        private static Queue<String> receive_queue;
        private static Queue<String> send_queue;
        private static Thread receiveThread;
        private static UnicodeEncoding unicodeEncoding;
        private static UdpClient receiver_sock;
        private static UdpClient sender_sock;
        //ctor
        Program()
        {
            receive_queue = new Queue<string>();
            send_queue = new Queue<string>();

            unicodeEncoding = new UnicodeEncoding();

            receiver_sock = new UdpClient();
            sender_sock = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 2222);
            receiver_sock.Connect(iPEndPoint);
            receiver_sock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.MulticastInterface, 1);
            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            receiver_sock.JoinMulticastGroup(iPAddress);
        }

        //private static IPAddress ActiveIp()
        //{
        //    foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up)
        //        {
        //            foreach (UnicastIPAddressInformation unicastIPAddressInformation in ni.GetIPProperties().UnicastAddresses)
        //            {
        //                if (unicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //                {
        //                    return unicastIPAddressInformation.Address;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
        public static void receive()
        {
            while (true)
            {
                IPEndPoint iPEndPoint = (IPEndPoint)receiver_sock.Client.LocalEndPoint;
                Byte[] data = receiver_sock.Receive(ref iPEndPoint);
                String strData = unicodeEncoding.GetString(data);
                receive_queue.Enqueue(strData);
            }
        }

        public static void send()
        {
            while (true)
            {
                if (send_queue.Count >= 1)
                {

                }
            }
        }
        static void Main(string[] args)
        {
            //receive thread
            receiveThread = new Thread(receive);
            receiveThread.Start();

            //send thread
            while (true)
            {
                Byte[] data = udpClient.Receive(ref iPEndPoint);
                string strData = Encoding.Unicode.GetString(data);
                Console.WriteLine(strData);
            }


        }
    }
}
