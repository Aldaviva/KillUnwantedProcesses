#nullable enable

using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeGenuineSoftwareIntegrityService: BaseKillableProcess {

        private const string SERVICE_NAME = "AGSService";

        public override string name { get; } = "Adobe Genuine Software Integrity Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}