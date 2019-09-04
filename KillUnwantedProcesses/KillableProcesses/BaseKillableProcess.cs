using System.ServiceProcess;
using KillUnwantedProcesses.TaskHelpers;

namespace KillUnwantedProcesses.KillableProcesses {

    public abstract class BaseKillableProcess: KillableProcess {

        protected static void KillProcess(string processName, bool alsoKillChildren = false) {
            ProcessHelpers.KillProcess(processName, alsoKillChildren);
        }

        protected static bool IsProcessRunning(string processName) {
            return ProcessHelpers.IsProcessRunning(processName);
        }

        protected static void StopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) { 
            ServiceHelpers.StopService(serviceName, postKillServiceStartMode);
        }

        protected static bool IsServiceRunning(string serviceName) {
            return ServiceHelpers.IsServiceRunning(serviceName);
        }
    }

}