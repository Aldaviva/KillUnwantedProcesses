using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmwareNatService: BaseKillableProcess {

        private const string SERVICE_NAME = "VMware NAT Service";

        public override string name { get; } = "VMware NAT Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}