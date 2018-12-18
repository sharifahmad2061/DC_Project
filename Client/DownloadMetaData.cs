using System.Collections.Generic;

namespace Client
{
    class DownloadMetaData
    {
        public static string Url { get; set; }
        public static List<int> downloadrange;

        public static void Initialize()
        {
            Url = "";
            downloadrange = new List<int>();
        }
        public static void StoreRange(string range)
        {
            char[] arr = {' '};
            string[] temp = range.Split(arr);
            downloadrange.Add(int.Parse(temp[0]));
            downloadrange.Add(int.Parse(temp[1]));
        }
        public static void AllocateParts(IEnumerable<int> parts)
        {
            int startbyte = 0;
            int index = 0;
            foreach(int endbyte in parts)
            {
                if(index == 0)
                {
                    string temp = startbyte.ToString() + " " + endbyte.ToString();
                    StoreRange(temp);
                    startbyte = endbyte;
                    index += 1;
                    continue;
                }
                string temp1 = startbyte.ToString() + " " + endbyte.ToString();
                SendData.Send(new DataObject("partallocation", temp1, Globals.nodeId, Globals.nodes[index - 1]));
                startbyte = endbyte;
                index += 1;
            }
        }
    }
}
