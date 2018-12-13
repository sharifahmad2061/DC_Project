using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class LeaderSelection
    {
        public static string tokenHolder;
        public static DateTime tokenCreationTime;
        public static int TotalElectionRequests;
        public static SortedList<DateTime, string> TokensWaitingList;

        public static void Initialize()
        {
            tokenHolder = null;
            tokenCreationTime = DateTime.Now;
            TotalElectionRequests = 0;
            TokensWaitingList = new SortedList<DateTime, string>();
        }
        public static void FindTokenHolder()
        {
            DataObject dataObject = new DataObject("findtoken", "", Globals.nodeId, "");
            SendData.Send(dataObject);
        }
        public static void FindTokenResponse()
        {
            if(tokenHolder == null)
            {
                SendData.Send(new DataObject("findtokenresponse","",Globals.nodeId,""));
            }
            else
            {
                SendData.Send(new DataObject("findtokenresponse",tokenHolder,Globals.nodeId,""));
            }
        }
        public static void HandleFindTokenResponse(DataObject dataObject)
        {
            //do election
            if(dataObject.data == "")
            {

            }
            else
            {
                tokenHolder = dataObject.data;
            }
        }
        public static void Elect()
        {
            //DataObject election = new DataObject();
        }
    }
}
