using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace Client
{
    //token passing algorithm need to implemented so that anybody having the token can request
    //data for download and the token will rotated.
    class DataObject
    {
        // type => {request, response, part allocation, datasharing}
        public string type;
        public string data;
        public string sender;
        public string receiver;
    }
    class Program
    {
        //shared for received data
        private static Queue<String> receive_queue;
        private static Queue<String> send_queue;
        private static string nodeId;
        private static Thread receiveThread;
        private static UnicodeEncoding unicodeEncoding;
        private static UdpClient udpClient;
        //ctor
        Program()
        {
            receive_queue = new Queue<string>();
            send_queue = new Queue<string>();

            Random random = new Random();
            nodeId = random.Next(1000,9999).ToString();

            unicodeEncoding = new UnicodeEncoding();

            udpClient = new UdpClient();
            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2222);
            udpClient.Connect(iPEndPoint);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.MulticastInterface, 1);
            udpClient.JoinMulticastGroup(iPAddress);
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
        public static void Receive()
        {
            while (true)
            {
                IPEndPoint iPEndPoint = (IPEndPoint)udpClient.Client.LocalEndPoint;
                Byte[] data = udpClient.Receive(ref iPEndPoint);
                String strData = unicodeEncoding.GetString(data);
                receive_queue.Enqueue(strData);
            }
        }

        public static void Send()
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
            receiveThread = new Thread(Receive);
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
