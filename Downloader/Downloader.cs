using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Downloader
{
    public static class Downloader
    {
        public static bool IsWebsiteSupported(string Url)
        {
            //check if the website is supported!
            //D:\Code\Node\DCProject\Downloader\utils\IsWebsiteSupported_.js
            //http://file-examples.com/wp-content/uploads/2017/02/zip_10MB.zip

            String workingDir = @"D:\Code\Node\DCProject\Downloader";
            String command = @"node " + workingDir + @"\utils\IsWebsiteSupported.js " + "\"" + Url + "\"";
            string output = ExecShell(command);

            return Convert.ToBoolean(output.Trim());
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

        private static string ExecShell(string Command) {

            //String workingDir = @"D:\Code\Node\DCProject\Downloader";
            //String command = @"/C node " + workingDir + @"\utils\IsWebsiteSupported_.js " + "\"http://file-examples.com/wp-content/uploads/2017/02/zip_10MB.zip\"";

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            string strCmdText;
            strCmdText = @"/C " + Command;

            // Correct way to launch a process with arguments
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = strCmdText;
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            return output;
        }

    }
}
