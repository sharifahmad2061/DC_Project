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


    class SyncNodes
    {
        private static DataObject SyncRequest;
        public static void Sync(String nodeId)
        {
            SyncRequest = new DataObject("sync", nodeId, nodeId, "");
            SendData.Send(ref Globals.udpClient, ref Globals.remoteEndPoint, ref SyncRequest, ref Globals.encoding);
        }
    }
    //token passing algorithm need to implemented so that anybody having the token can request
    //data for download and the token will rotated.
    class DataObject
    {
        // type => {sync, request, response, part allocation, datasharing,leader election, leader election response,}
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

    class SendData
    {
        public static void Send(ref UdpClient udpClient,ref IPEndPoint iPEndPoint,ref DataObject dataObject, ref Encoding encoding)
        {
            String temp = JsonConvert.SerializeObject(dataObject);
            Byte[] temp1 = encoding.GetBytes(temp);
            udpClient.Send(temp1, temp1.Length, iPEndPoint);
        }
    }

    class token
    {
        public static String tokenHolder;
        public static DateTime tokenCreationTime;

        public token(String nodeId)
        {
            tokenHolder = nodeId;
            tokenCreationTime = DateTime.Now;
        }
        public void selectLeader()
        {
            DataObject = 
        }
    }

    class Program
    {
        //shared for received data
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
            Globals.Initialize();

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
