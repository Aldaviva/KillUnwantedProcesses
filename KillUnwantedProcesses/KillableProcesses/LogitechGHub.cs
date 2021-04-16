using System.ServiceProcess;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    public class LogitechGHub: KillableService {

        protected override string serviceName { get; } = "LGHUBUpdaterService";

        public override string name { get; } = "Logitech G Hub";

        protected override ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Manual;

        public override bool shouldKill() {
            return base.shouldKill() && !isProcessRunning("lghub");
        }

    }

}