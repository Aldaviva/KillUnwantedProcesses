using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class LogitechGHub: BaseKillableProcess {

        private const string ServiceName = "LGHUBUpdaterService";

        public override string Name { get; } = "Logitech G Hub";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName) && !IsProcessRunning("lghub");
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Manual);
        }

    }

}