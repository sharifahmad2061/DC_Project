using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace Downloader
{
    public static class Downloader
    {
        public static bool IsWebsiteSupported(string Url)
        {
            //check if the website is supported!
            //D:\Code\Node\DCProject\Downloader\utils\IsWebsiteSupported_.js
            //http://file-examples.com/wp-content/uploads/2017/02/zip_10MB.zip

            String workingDir = @"C:\Users\sharifahmad\Google Drive\University Material\7th Semester\1. Distributed Computing (3+1)\project\MultiCast_Test\Downloader";
            String command = @"node " + workingDir + @"\utils\IsWebsiteSupported.js " + "\"" + Url + "\"";
            string output = ExecShell(command);

            return Convert.ToBoolean(output.Trim());
        }

        public static Tuple<String, IEnumerable<int>> GetNameAndPartSizes(string Url, int NoOfParts)
        {
            int contentLength = 12345; //get contentLengthFirst
            string fileName = ""; //get fileName

            try
            {
                String workingDir = @"C:\Users\sharifahmad\Google Drive\University Material\7th Semester\1. Distributed Computing (3+1)\project\MultiCast_Test\Downloader";
                String command = @"node " + workingDir + @"\utils\FileDetails.js " + "\"" + Url + "\"";
                String output = ExecShell(command);

                fileName = output.Split(';')[0];
                contentLength = Convert.ToInt32(output.Split(';')[1]);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Tuple.Create(fileName, DistributeInteger(contentLength, NoOfParts));
        }

        public static string DownloadFile(String Url, int StartByte, int EndByte)
        {
            try
            {
                String workingDir = @"C:\Users\sharifahmad\Google Drive\University Material\7th Semester\1. Distributed Computing (3+1)\project\MultiCast_Test\Downloader";
                String command = @"node " + workingDir + @"\utils\DownloadFilePart.js " + "\"" + Url + "\" " + StartByte + " " + EndByte;
                string output = ExecShell(command).Trim();

                if (output == "error")
                {
                    throw new Exception(output);
                }
                else
                {
                    return output;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static string JoinParts(String[] FilePartsPathsInSortedArray, String Destination)
        {
            //paths can not include a quote/double-quote
            try
            {
                String workingDir = @"C:\Users\sharifahmad\Google Drive\University Material\7th Semester\1. Distributed Computing (3+1)\project\MultiCast_Test\Downloader";

                string jsnPaths = JsonConvert.SerializeObject(FilePartsPathsInSortedArray);
                string jsnString = (jsnPaths).Replace("\"", "\\\"");
                String command = @"node " + workingDir + @"\utils\JoinFiles.js " + "\"" + jsnString + "\" \"" + (Destination) + "\"";
                string output = ExecShell(command).Trim();
                //var lp = JsonConvert.DeserializeObject<List<string>>(output);
                if ("done" == output)
                {
                    return output;
                }
                else
                {
                    throw new Exception(output);
                }
            }
            catch (Exception e)
            {
                throw;
            }
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

        private static string ExecShell(string Command)
        {

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
