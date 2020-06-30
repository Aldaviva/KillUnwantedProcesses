﻿#nullable enable

using System.ServiceProcess;
using KillUnwantedProcesses.TaskHelpers;

namespace KillUnwantedProcesses.KillableProcesses {

    public abstract class BaseKillableProcess: KillableProcess {

        /// <summary>
        /// End a running process on the current computer. Returns successfully whether or not the process was running or was killed
        /// successfully.
        /// </summary>
        /// <param name="processName">The base name of the executable, with or without the trailing <c>.exe</c>.</param>
        /// <param name="alsoKillChildren">If <c>true</c>, descendent processes (those whose parents or higher ancestors have the given
        /// <c>processName</c>) should also be killed. If <c>false</c>, leave them running and only kill the process with the given
        /// basename.</param>
        protected static void killProcess(string processName, bool alsoKillChildren = false) {
            ProcessHelpers.killProcess(processName, alsoKillChildren);
        }

        /// <summary>Get whether or not a process is running on the current computer.</summary>
        /// <param name="processName">The base name of the executable, with or without the trailing <c>.exe</c>.</param>
        /// <returns><c>true</c> if there is at least one process with the given base name running on the computer, or <c>false</c>
        /// otherwise.</returns>
        protected static bool isProcessRunning(string processName) {
            return ProcessHelpers.isProcessRunning(processName);
        }

        protected static void stopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) {
            ServiceHelpers.stopService(serviceName, postKillServiceStartMode);
        }

        protected static bool isServiceRunning(string serviceName) {
            return ServiceHelpers.isServiceRunning(serviceName);
        }

        protected static ServiceStartMode getServiceStartMode(string serviceName) {
            return ServiceHelpers.getServiceStartMode(serviceName);
        }

        protected static bool isProcessSuspended(string processName) {
            return ProcessHelpers.isProcessSuspended(processName);
        }

    }

}