using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmnetDhcpService: BaseKillableProcess {

        private const string SERVICE_NAME = "VMnetDHCP";

        public override string name { get; } = "VMware DHCP Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}