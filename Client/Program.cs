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
        public String type;
        public String data;
        public String sender;
        public String receiver;

        public DataObject(String type, String data, String sender, String receiver)
        {
            this.type = type;
            this.data = data;
            this.sender = sender;
            this.receiver = receiver;
        }
    }

    class Program
    {
        //shared for received data
        private static Queue<String> receive_queue;
        private static Queue<String> send_queue;
        private static String nodeId;
        private static Thread receiveThread;
        private static Encoding encoding;
        // private static UnicodeEncoding unicodeEncoding;
        private static UdpClient udpClient;
        private static IPEndPoint localEndPoint;
        private static IPEndPoint remoteEndPoint;
        private static IPAddress multicastAddress;
        //ctor

        public static IPAddress AdaptersAddress(NetworkInterfaceType networkInterfaceType)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType != networkInterfaceType)
                    continue;
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                    continue;
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                    continue;
                if (!adapter.SupportsMulticast)
                    continue; // multicast is meaningless for this type of connection
                if (adapter.OperationalStatus != OperationalStatus.Up)
                    continue; // this adapter is off or not connected
                IPv4InterfaceProperties p = adapter.GetIPProperties().GetIPv4Properties();
                if (p == null)
                    continue; // IPv4 is not configured on this adapter
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.Address;
                    }
                }
            }
            return null;
        }

        //public static void PrioritizeMultiCastInterface(ref UdpClient udpClient)
        //{
        //        //udpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, (int)IPAddress.HostToNetworkOrder(p.Index));
        //        //udpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, p.Index);
        //}
        
        public static void InitializeData()
        {
            receive_queue = new Queue<String>();
            send_queue = new Queue<String>();

            Random random = new Random();
            nodeId = random.Next(1000, 9999).ToString();
            Console.WriteLine(nodeId);
            encoding = new UTF8Encoding();

            // unicodeEncoding = new UnicodeEncoding();
            IPAddress iPAddress = AdaptersAddress(NetworkInterfaceType.Wireless80211);
            localEndPoint = new IPEndPoint(iPAddress, 4444);
            udpClient = new UdpClient(localEndPoint);

            multicastAddress = IPAddress.Parse("232.0.0.2");
            remoteEndPoint = new IPEndPoint(multicastAddress, 2222);

            udpClient.JoinMulticastGroup(multicastAddress);
            udpClient.MulticastLoopback = false;
            //Console.WriteLine("Initializations done.");
        }

        public static void Receive()
        {
            while (true)
            {
                Byte[] data = udpClient.Receive(ref remoteEndPoint);
                String strData = encoding.GetString(data);
                Console.Write("printed from receive");
                Console.WriteLine(strData);
                //receive_queue.Enqueue(strData);
            }
        }

        // public static void Send()
        // {
        //     while (true)
        //     {
        //         if (send_queue.Count >= 1)
        //         {

        //         }
        //     }
        // }
        static void Main(String[] args)
        {
            InitializeData();

            //receive thread
            receiveThread = new Thread(Receive);
            receiveThread.Start();

            DataObject dataObject = new DataObject("request", "hello there", nodeId, "");
            String data = JsonConvert.SerializeObject(dataObject);
            //Console.WriteLine(data);
            byte[] sending_data = encoding.GetBytes(data);
            //Console.WriteLine(sending_data.ToString());
            udpClient.Send(sending_data, sending_data.Length,remoteEndPoint);
            //send thread
            // while (true)
            // {
            // Byte[] data = udpClient.Receive(ref localEndPoint);
            // String strData = Encoding.Unicode.GetString(data);
            // Console.WriteLine(strData);


            // }
            receiveThread.Join();

        }
    }
}
