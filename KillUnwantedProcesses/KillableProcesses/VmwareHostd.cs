using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmwareHostd: BaseKillableProcess {

        private const string SERVICE_NAME = "VMwareHostd";

        public override string name { get; } = "VMware Workstation Server";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}