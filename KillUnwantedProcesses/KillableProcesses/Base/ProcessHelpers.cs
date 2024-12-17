#nullable enable

using System;
using System.Diagnostics;
using Unfucked;
using Unfucked.Windows;

namespace KillUnwantedProcesses.KillableProcesses.Base;

internal static class ProcessHelpers {

    public static void killProcess(string processName, bool alsoKillChildren = false) {
        processName = Processes.StripExeSuffix(processName);
        Process[] processesToKill = Process.GetProcessesByName(processName);
        foreach (Process processToKill in processesToKill) {
            using (processToKill) {
                killProcess(processToKill, alsoKillChildren);
            }
        }
    }

    public static void killProcess(Process process, bool alsoKillChildren = false) {
        using (process) {
            if (alsoKillChildren) {
                foreach (Process descendentToKill in process.GetDescendantProcesses()) {
                    try {
                        killProcess(descendentToKill);
                    } catch (Exception) {
                        //probably already closed or can't be killed
                    }
                }
            }

            try {
                Console.WriteLine($"Killing {process.ProcessName} ({process.Id})");
                process.Kill();
            } catch (Exception) {
                //probably already closed or can't be killed
            }
        }
    }

    public static bool isProcessRunning(string processName) {
        using Process? process = getProcess(processName);
        return process != null;
    }

    public static bool isProcessSuspended(string processName) {
        using Process? process = getProcess(processName);
        return process?.IsProcessSuspended() ?? false;
    }

    private static Process? getProcess(string processName) {
        processName = Processes.StripExeSuffix(processName);
        Process? result = null;
        foreach (Process process in Process.GetProcessesByName(processName)) {
            if (result == null) {
                result = process;
            } else {
                result.Dispose();
            }
        }
        return result;
    }

}