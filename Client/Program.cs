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

    class Program
    {

        //public static void PrioritizeMultiCastInterface(ref UdpClient udpClient)
        //{
        //        //udpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, (int)IPAddress.HostToNetworkOrder(p.Index));
        //        //udpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, p.Index);
        //}

        static void Main(String[] args)
        {
            Globals.Initialize();

            //receive thread
            Globals.receiveThread = new Thread(Receive.receive);
            Globals.receiveThread.Start();

            //Sync Nodes
            SyncNodes.Sync(Globals.nodeId);

            while (true)
            {
                Console.Write("Do you want to download file >> ");
                string resp = Console.ReadLine(); //yes or no
                if (resp == "yes")
                {
                    Console.Write("Enter file URL >> ");
                    string url = Console.ReadLine();
                    
                }
                else
                {
                    Console.WriteLine("now handling downloads for others.");
                    break;
                }
            }
            //DataObject dataObject = new DataObject("request", "hello there", Globals.nodeId, "");
            //SendData.Send(dataObject);
            //send thread
            // while (true)
            // {
            // Byte[] data = udpClient.Receive(ref localEndPoint);
            // String strData = Encoding.Unicode.GetString(data);
            // Console.WriteLine(strData);


            // }
            Globals.receiveThread.Join();

        }
    }
}
