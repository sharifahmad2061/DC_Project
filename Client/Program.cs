using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace Client
{
    class Program
    {
        //shared for received data
        private Queue<String> queue;

        //ctor
        Program()
        {
            queue = new Queue<string>();
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
        static void receive(UdpClient udpClient)
        {

        }
        static void Main(string[] args)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any,2222);
            UdpClient udpClient = new UdpClient(iPEndPoint);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.MulticastInterface,1);
            IPAddress iPAddress = IPAddress.Parse("232.0.0.2");
            udpClient.JoinMulticastGroup(iPAddress);
            
            Console.WriteLine("Listening this will never quit so you will need to ctrl-c it");

            Byte[] msg = udpClient.Receive(ref iPEndPoint);
            Console.WriteLine(Encoding.Unicode.GetString(msg));

            while (true)
            {
                Byte[] data = udpClient.Receive(ref iPEndPoint);
                string strData = Encoding.Unicode.GetString(data);
                Console.WriteLine(strData);
            }


        }
    }
}
