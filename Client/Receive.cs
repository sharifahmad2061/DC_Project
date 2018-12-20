using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LiteNetLib.Utils;
using LiteNetLib;

namespace Client
{
    class Receive
    {
        public static EventBasedNetListener listener = new EventBasedNetListener();
        public static NetManager client = new NetManager(listener,"");

        public static string localfilename = "";
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
                            localfilename = Downloader.Downloader.DownloadFile(DownloadMetaData.Url, DownloadMetaData.downloadrange[0].Item1, DownloadMetaData.downloadrange[0].Item2);
                            Console.WriteLine("file downloaded");
                        }
                        break;
                    case "datasharing":
                        client.Start();
                        client.Connect(Globals.multicastAddress.ToString(), 2222);
                        listener.NetworkReceiveEvent += (fromPeer, dataReader) =>
                        {
                            Console.WriteLine("We got: {0}", dataReader.GetString(100));
                            dataReader.Clear();
                        };
                        Console.WriteLine("data sharing request received");
                        SendData.Send(new DataObject("filesending","",Globals.nodeId,JsonData.sender));
                        Console.WriteLine(localfilename);
                        Globals.udpClient.Client.SendFile(localfilename);
                        break;
                    case "filesending":
                        Byte[] file_data = Globals.udpClient.Receive(ref Globals.remoteEndPoint);
                        var fs = new FileStream("file1.tmp", FileMode.Create, FileAccess.Write);
                        fs.Write(file_data, 0, file_data.Length);
                        break;
                    default:
                        Console.WriteLine("packet of some type else");
                        break;
                }
            }
        }

        private static void Listener_NetworkReceiveEvent(NetPeer peer, NetDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
