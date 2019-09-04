using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCreativeCloudUpdater: BaseKillableProcess {

        private const string ServiceName = "AdobeUpdateService";

        public override string Name { get; } = "Adobe Creative Cloud Updater";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName);
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Disabled);
        }

    }

}