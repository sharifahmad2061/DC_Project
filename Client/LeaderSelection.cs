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

        public static void Initialize()
        {
            tokenHolder = null;
            tokenCreationTime = DateTime.Now;
        }
        public static void FindTokenHolder()
        {
            DataObject dataObject = new DataObject("findtoken", "", Globals.nodeId, "");

        }
        public static void elect()
        {
            DataObject election=
        }
    }
}
