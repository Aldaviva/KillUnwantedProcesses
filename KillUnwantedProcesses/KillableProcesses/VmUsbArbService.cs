using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmUsbArbService: BaseKillableProcess {

        private const string SERVICE_NAME = "VMUSBArbService";

        public override string name { get; } = "VMware USB Arbitration Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}