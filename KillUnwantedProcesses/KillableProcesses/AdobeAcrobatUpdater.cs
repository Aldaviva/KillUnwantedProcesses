using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeAcrobatUpdater: BaseKillableProcess {

        private const string ServiceName = "AdobeARMservice";

        public override string Name { get; } = "Adobe Acrobat Updater";

        public override bool ShouldKill() {
            return IsServiceRunning(ServiceName);
        }

        public override void Kill() {
            StopService(ServiceName, ServiceStartMode.Disabled);
        }

    }

}