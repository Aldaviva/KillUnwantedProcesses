using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeFlashUpdater: BaseKillableProcess {

        private const string SERVICE_NAME = "AdobeFlashPlayerUpdateSvc";

        public override string name { get; } = "Adobe Flash Updater";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}