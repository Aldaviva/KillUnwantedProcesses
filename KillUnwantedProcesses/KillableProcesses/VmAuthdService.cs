using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmAuthdService: BaseKillableProcess {

        private const string SERVICE_NAME = "VMAuthdService";

        public override string name { get; } = "VMware Authorization Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}