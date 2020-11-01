using System.ServiceProcess;

#nullable enable

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeUpdateService: BaseKillableProcess {

        private const string SERVICE_NAME = "AdobeUpdateService";

        public override string name { get; } = "Adobe Update Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME) && !isProcessRunning(AdobeDesktopService.ADOBE_DESKTOP_SERVICE_PROCESS_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}