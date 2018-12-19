using System;
using System.Collections.Generic;

namespace Client
{
    class DownloadMetaData
    {
        public static string Url { get; set; }
        public static List<Tuple<int, int>> downloadrange;
        public static List<Tuple<int, string>> partsandnodes;

        public static void Initialize()
        {
            Url = "";
            downloadrange = new List<Tuple<int,int>>();
            partsandnodes = new List<Tuple<int, string>>();
        }
        public static void StoreRange(string range)
        {
            char[] arr = {' '};
            string[] temp = range.Split(arr);
            downloadrange.Add(Tuple.Create(int.Parse(temp[0]),int.Parse(temp[1])));
        }
        public static void AllocateParts(IEnumerable<int> parts)
        {
            int startbyte = 0;
            int index = 0;
            foreach(int endbyte in parts)
            {
                if(!(index == 0))
                {
                    string temp1 = (startbyte + 1).ToString() + " " + (startbyte + endbyte).ToString();
                    StoreRange(temp1);
                    //SendData.Send(new DataObject("partallocation", temp1, Globals.nodeId, Globals.nodes[index - 1]));
                    startbyte = startbyte + endbyte;
                    index += 1;
                    
                }
                else
                {
                    string temp = startbyte.ToString() + " " + endbyte.ToString();
                    StoreRange(temp);
                    startbyte = endbyte;
                    index += 1;
                    continue;
                }
            }
        }
        public static void ShareParts()
        {
            partsandnodes.Add(Tuple.Create(0, Globals.nodeId));
            for(int i= 1; i < downloadrange.Count; ++i)
            {
                string temp = downloadrange[i].Item1 + " " + downloadrange[i].Item2;
                partsandnodes.Add(Tuple.Create(1, Globals.nodes[i]));
                DataObject dataObject = new DataObject("partallocation", temp, Globals.nodeId, Globals.nodes[i]);
                SendData.Send(dataObject);
            }
        }
    }
}
