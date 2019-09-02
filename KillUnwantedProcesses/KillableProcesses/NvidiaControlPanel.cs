using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class NvidiaControlPanel: BaseKillableProcess {

        private const string ServiceName = "NVDisplay.ContainerLocalSystem";

        public override string Name { get; } = "Nvidia Control Panel";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName) && !IsProcessRunning("nvcplui");
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Manual);
        }

    }

}