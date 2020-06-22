using System;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmAuthdService: BaseKillableProcess {

        private const string SERVICE_NAME = "VMAuthdService";

        public override string name { get; } = "VMware Authorization Service";

        /// <summary>
        /// On Windows 7, a VMware Workstation can start a guest when this service is disabled. On Windows 10, it cannot, and fails with an error message telling the user to enable this service.
        /// </summary>
        /// <returns></returns>
        public override bool shouldKill() {
            return Environment.OSVersion.Version.Major <= 7 && isServiceRunning(SERVICE_NAME) && !isProcessRunning("vmware-vmx");
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}