using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eyon.Utilities.API
{
    public class Defender
    {
        public void Run(string pathToFile)
        {
            // https://www.windowscentral.com/how-use-windows-defender-command-prompt-windows-10
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Program Files\\Windows Defender\\MpCmdRun.exe";
            startInfo.Arguments = "-Scan -ScanType 3 -File " + pathToFile;
            process.StartInfo = startInfo;
            var results = process.Start();
        }
    }
}
