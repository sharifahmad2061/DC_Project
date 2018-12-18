﻿using System;
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
                    case "syncresponse":
                        SyncNodes.HandleSyncResponse(JsonData);
                        break;
                    case "findtoken":
                        SendData.Send(new DataObject("findtokenresponse",LeaderSelection.tokenHolder,"",""));
                        break;
                    case "findtokenresponse":
                        LeaderSelection.HandleFindTokenResponse(JsonData);
                        break;
                    case "request":
                        DownloadMetaData.Url = JsonData.data;
                        break;
                    case "partallocation":
                        if (JsonData.receiver == Globals.nodeId)
                            DownloadMetaData.StoreRange(JsonData.data);
                        break;
                    default:
                        Console.WriteLine("packet of some type else");
                        break;
                }
            }
        }
    }
}
