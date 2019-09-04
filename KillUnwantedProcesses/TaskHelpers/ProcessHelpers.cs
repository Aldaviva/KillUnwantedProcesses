using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using KillUnwantedProcesses.Utilities;

namespace KillUnwantedProcesses.TaskHelpers {

    internal static class ProcessHelpers {

        public static void KillProcess(string processName, bool alsoKillChildren = false) {
            processName = StripExeSuffix(processName);
            Process[] processesToKill = Process.GetProcessesByName(processName);
            foreach (Process processToKill in processesToKill) {
                using (processToKill) {
                    if (alsoKillChildren) {
                        foreach (Process descendentToKill in ParentProcessUtilities.GetDescendentProcesses(processToKill)) {
                            try {
                                KillProcess(descendentToKill);
                            } catch (Exception) {
                                //probably already closed or can't be killed
                            }
                        }
                    }

                    try {
                        KillProcess(processToKill);
                    } catch (Exception) {
                        //probably already closed or can't be killed
                    }
                }
            }
        }

        private static void KillProcess(Process process) {
            Console.WriteLine($"Killing {process.ProcessName} ({process.Id})");
            process.Kill();
        }

        public static bool IsProcessRunning(string processName) {
            processName = StripExeSuffix(processName);
            using (Process process = Process.GetProcessesByName(processName).FirstOrDefault()) {
                return process != null;
            }
        }

        private static string StripExeSuffix(string processName) {
            return Regex.Replace(processName, @"\.exe$", string.Empty);
        }

    }

}