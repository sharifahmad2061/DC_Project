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
                //Console.WriteLine("printed from receive");
                //Console.WriteLine(strData);
                //receive_queue.Enqueue(strData);
                DataObject JsonData = JsonConvert.DeserializeObject<DataObject>(strData);
                switch (JsonData.type)
                {
                    case "sync":
                        Console.WriteLine("sync request received");
                        SyncNodes.RecognizeNode(JsonData);
                        break;
                    case "syncresponse":
                        Console.WriteLine("sync response");
                        SyncNodes.HandleSyncResponse(JsonData);
                        break;
                    case "findtoken":
                        SendData.Send(new DataObject("findtokenresponse",LeaderSelection.tokenHolder,"",""));
                        break;
                    case "findtokenresponse":
                        LeaderSelection.HandleFindTokenResponse(JsonData);
                        break;
                    case "request":
                        Console.WriteLine("file download request received");
                        DownloadMetaData.Url = JsonData.data;
                        break;
                    case "partallocation":
                        Console.WriteLine("part allocation request received");
                        if (JsonData.receiver == Globals.nodeId)
                        {
                            Console.WriteLine("downloading file");
                            DownloadMetaData.StoreRange(JsonData.data);
                            Downloader.Downloader.DownloadFile(DownloadMetaData.Url, DownloadMetaData.downloadrange[0].Item1, DownloadMetaData.downloadrange[0].Item2);
                            Console.WriteLine("file downloaded");
                        }
                        break;
                    case "datasharing":
                        Console.WriteLine("data sharing request received");
                        break;
                    default:
                        Console.WriteLine("packet of some type else");
                        break;
                }
            }
        }
    }
}
