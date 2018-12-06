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
        private static UdpClient sender;
        //ctor
        Program()
        {
            receive_queue = new Queue<string>();
            send_queue = new Queue<string>();
            sender = new UdpClient();
            sender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.MulticastInterface, 1);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 2222);
            sender.Connect(iPEndPoint);
            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            sender.JoinMulticastGroup(iPAddress);
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
                IPEndPoint iPEndPoint = (IPEndPoint)sender.Client.LocalEndPoint;
                Byte[] data = sender.Receive(ref iPEndPoint);
                String strData = Encoding.Unicode.GetString(data);
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
