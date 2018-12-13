using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class SendData
    {
        public static void Send(ref UdpClient udpClient, ref IPEndPoint iPEndPoint, ref DataObject dataObject, ref Encoding encoding)
        {
            String temp = JsonConvert.SerializeObject(dataObject);
            Byte[] temp1 = encoding.GetBytes(temp);
            udpClient.Send(temp1, temp1.Length, iPEndPoint);
        }
    }
}
