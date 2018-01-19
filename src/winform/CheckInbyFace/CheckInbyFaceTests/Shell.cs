using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.Tests
{
    public static class Shell
    {
        public static void CMD(
            string commandNameOrFileName,
            string arguments,
            DataReceivedEventHandler callback)
        {
            Process process = new Process();
            process.StartInfo.FileName = commandNameOrFileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            // process.OutputDataReceived += new DataReceivedEventHandler(callback);
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
        }
    }
}
