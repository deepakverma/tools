using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace xperfoncpuspike {
    class Program {
        static void Main(string[] args) {
            Environment.SetEnvironmentVariable("STACK", "Profile+CSwitch+ReadyThread+FileCreate+FileClose");
            //var s = Process.Start("setup.bat");
            //s.OutputDataReceived += P_OutputDataReceived;
           // s.WaitForExit();
            var p = Process.GetProcessesByName("msmpeng");
            long prev;
            
            if (p.Length == 1) {
                Console.WriteLine($"found msmpeng {p[0].WorkingSet64}");
                prev = p[0].WorkingSet64;
                while (true) {
                    Console.WriteLine($"found msmpeng {p[0].WorkingSet64}");
                    Thread.Sleep(100);
                    if (p[0].WorkingSet64 >= 80438144) {
                        Console.WriteLine("memory spiked");
                        var s1 = Process.Start("run.bat");
                        s1.OutputDataReceived += P_OutputDataReceived;
                        s1.WaitForExit();
                    }
                }
            }
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            Console.WriteLine(e.Data);
        }
    }
}
