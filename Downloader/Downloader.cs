using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public class Downloader
    {
        public static bool IsWebsiteSupported(string Url)
        {
            //check if the website is supported!

            return true;
        }

        public static Tuple<String, IEnumerable<int>> GetNameAndPartSizes(string Url, int NoOfParts)
        {
            int contentLength = 12345; //get contentLengthFirst
            string fileName = ""; //get fileName

            return Tuple.Create(fileName, DistributeInteger(contentLength, NoOfParts));
        }

        public static string DownloadFile(int StartByte, int EndByte)
        {
            try
            {

            }
            catch (Exception e)
            {
                throw;
            }
            return "";
        }

        public static string JoinParts(String[] FilePartsPathsInSortedArray)
        {
            try
            {
            
            }
            catch (Exception e)
            {
                throw;
            }
            return "";
        }

        private static IEnumerable<int> DistributeInteger(int total, int divider)
        {
            //https://dotnetcodr.com/2015/11/03/divide-an-integer-into-groups-with-c/

            if (divider == 0)
            {
                yield return 0;
            }
            else
            {
                int rest = total % divider;
                double result = total / (double)divider;

                for (int i = 0; i < divider; i++)
                {
                    if (rest-- > 0)
                        yield return (int)Math.Ceiling(result);
                    else
                        yield return (int)Math.Floor(result);
                }
            }
        }
    }
}
