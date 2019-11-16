using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class NvidiaControlPanel: BaseKillableProcess {

        private const string SERVICE_NAME = "NVDisplay.ContainerLocalSystem";

        public override string name { get; } = "Nvidia Control Panel";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME) && !isProcessRunning("nvcplui");
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Manual);
        }

    }

}