using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class LogitechGHub: BaseKillableProcess {

        private const string SERVICE_NAME = "LGHUBUpdaterService";

        public override string name { get; } = "Logitech G Hub";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME) && !isProcessRunning("lghub");
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Manual);
        }

    }

}