using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    public static class Globals
    {
        public static Queue<string> receive_queue;
        public static Queue<string> send_queue;

        public static string nodeId;
        public static List<string> nodes;
        public static int numofnodes;

        public static Thread receiveThread;

        public static Encoding encoding;

        public static UdpClient udpClient;

        public static IPEndPoint localEndPoint;
        public static IPEndPoint remoteEndPoint;

        private static IPAddress multicastAddress;

        private static bool token;

        public static void Initialize()
        {
            receive_queue = new Queue<String>();
            send_queue = new Queue<String>();

            Random random = new Random();
            nodeId = random.Next(1000, 9999).ToString();
            //Console.WriteLine(nodeId);
            nodes = new List<string>();
            numofnodes = 1;

            encoding = new UTF8Encoding();

            // unicodeEncoding = new UnicodeEncoding();
            IPAddress iPAddress = AdapterHandling.AdaptersAddress(NetworkInterfaceType.Wireless80211);
            localEndPoint = new IPEndPoint(iPAddress, 2222);
            udpClient = new UdpClient(localEndPoint);

            multicastAddress = IPAddress.Parse("232.0.0.2");
            remoteEndPoint = new IPEndPoint(multicastAddress, 2222);

            udpClient.JoinMulticastGroup(multicastAddress);
            udpClient.MulticastLoopback = false;
        }
    }
}
