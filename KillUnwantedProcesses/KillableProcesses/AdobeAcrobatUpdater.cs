using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeAcrobatUpdater: BaseKillableProcess {

        private const string SERVICE_NAME = "AdobeARMservice";

        public override string name { get; } = "Adobe Acrobat Updater";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}