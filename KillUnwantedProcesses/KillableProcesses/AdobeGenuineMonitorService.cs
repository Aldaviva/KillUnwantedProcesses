#nullable enable

using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeGenuineMonitorService: BaseKillableProcess {

        private const string SERVICE_NAME = "AGMService";

        public override string name { get; } = "Adobe Genuine Monitor Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}