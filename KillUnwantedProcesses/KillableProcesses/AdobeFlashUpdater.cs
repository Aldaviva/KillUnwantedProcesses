using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeFlashUpdater: BaseKillableProcess {

        private const string ServiceName = "AdobeFlashPlayerUpdateSvc";

        public override string Name { get; } = "Adobe Flash Updater";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName);
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Disabled);
        }

    }

}