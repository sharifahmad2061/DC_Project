using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void RecognizeNode(DataObject dataObject)
        {
            if (!Globals.nodes.Contains(dataObject.sender))
            {
                Globals.nodes.Add(dataObject.sender);
            }
        }
    }
}
