#nullable enable

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using KillUnwantedProcesses.Utilities;

namespace KillUnwantedProcesses.TaskHelpers {

    internal static class ProcessHelpers {

        public static void killProcess(string processName, bool alsoKillChildren = false) {
            processName = stripExeSuffix(processName);
            Process[] processesToKill = Process.GetProcessesByName(processName);
            foreach (Process processToKill in processesToKill) {
                using Process kill = processToKill;
                if (alsoKillChildren) {
                    foreach (Process descendentToKill in ParentProcessUtilities.getDescendentProcesses(processToKill)) {
                        try {
                            killProcess(descendentToKill);
                        } catch (Exception) {
                            //probably already closed or can't be killed
                        }
                    }
                }

                try {
                    killProcess(processToKill);
                } catch (Exception) {
                    //probably already closed or can't be killed
                }
            }
        }

        private static void killProcess(Process process) {
            Console.WriteLine($"Killing {process.ProcessName} ({process.Id})");
            process.Kill();
        }

        public static bool isProcessRunning(string processName) {
            processName = stripExeSuffix(processName);
            using Process? process = Process.GetProcessesByName(processName).FirstOrDefault();
            return process != null;
        }

        private static string stripExeSuffix(string processName) {
            return Regex.Replace(processName, @"\.exe$", string.Empty, RegexOptions.IgnoreCase);
        }

    }

}