using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class WindowsImageAcquisition: BaseKillableProcess {

        private const string SERVICE_NAME = "stisvc";

        public override string name { get; } = "Windows Image Acquisition";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Manual);
        }

    }

}