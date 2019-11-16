﻿using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCreativeCloudUpdater: BaseKillableProcess {

        private const string SERVICE_NAME = "AdobeUpdateService";

        public override string name { get; } = "Adobe Creative Cloud Updater";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME);
        }

        public override void kill() {
            stopService(SERVICE_NAME, ServiceStartMode.Disabled);
        }

    }

}