using System.ServiceProcess;
using KillUnwantedProcesses.TaskHelpers;

namespace KillUnwantedProcesses.KillableProcesses {

    public abstract class BaseKillableProcess: KillableProcess {

        protected static void killProcess(string processName, bool alsoKillChildren = false) {
            ProcessHelpers.killProcess(processName, alsoKillChildren);
        }

        protected static bool isProcessRunning(string processName) {
            return ProcessHelpers.isProcessRunning(processName);
        }

        protected static void stopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) { 
            ServiceHelpers.stopService(serviceName, postKillServiceStartMode);
        }

        protected static bool isServiceRunning(string serviceName) {
            return ServiceHelpers.isServiceRunning(serviceName);
        }
    }

}