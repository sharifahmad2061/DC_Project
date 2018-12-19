using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class SendData
    {
        public static void Send(DataObject dataObject)
        {
            String temp = JsonConvert.SerializeObject(dataObject);
            Byte[] temp1 = Globals.encoding.GetBytes(temp);
            Globals.udpClient.Send(temp1, temp1.Length, Globals.remoteEndPoint);
            Console.WriteLine("data sent");
        }
    }
}
