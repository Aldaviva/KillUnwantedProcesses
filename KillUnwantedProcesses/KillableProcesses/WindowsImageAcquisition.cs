using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class WindowsImageAcquisition: BaseKillableProcess {

        private const string ServiceName = "stisvc";

        public override string Name { get; } = "Windows Image Acquisition";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName);
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Manual);
        }

    }

}