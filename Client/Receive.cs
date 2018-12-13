using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    class Receive
    {
        public static void receive()
        {
            while (true)
            {
                Byte[] data = Globals.udpClient.Receive(ref Globals.remoteEndPoint);
                String strData = Globals.encoding.GetString(data);
                Console.Write("printed from receive");
                //Console.WriteLine(strData);
                //receive_queue.Enqueue(strData);
                DataObject JsonData = JsonConvert.DeserializeObject<DataObject>(strData);
                switch (JsonData.type)
                {
                    case "sync":
                        SyncNodes.RecognizeNode(JsonData);
                        break;
                    case "":

                        break;
                    default:
                        Console.WriteLine("packet of some type else");
                        break;
                }
            }
        }
    }
}
