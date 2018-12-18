using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    class SyncNodes
    {
        private static DataObject SyncRequest;
        public static void Sync(String nodeId)
        {
            Console.WriteLine("sent sync request");
            SyncRequest = new DataObject("sync", nodeId, nodeId, "");
            SendData.Send(SyncRequest);
        }
        public static void RecognizeNode(DataObject dataObject)
        {
            if (!Globals.nodes.Contains(dataObject.sender))
            {
                Globals.nodes.Add(dataObject.sender);
                Globals.numofnodes += 1;
                string temp = string.Join(",", Globals.nodes.ToArray());
                SendData.Send(new DataObject("syncresponse",temp,Globals.nodeId,dataObject.sender));
                Console.WriteLine("send sync response");
            }
        }
        public static void HandleSyncResponse(DataObject dataObject)
        {
            if (dataObject.receiver != Globals.nodeId)
                return;
            else
            {
                char[] sep = { ',' };
                string[] nodes = dataObject.data.Split(sep);
                foreach(string s in nodes)
                {
                    if (!Globals.nodes.Contains(s))
                        Globals.nodes.Add(s);
                }
            }
        }
    }
}
