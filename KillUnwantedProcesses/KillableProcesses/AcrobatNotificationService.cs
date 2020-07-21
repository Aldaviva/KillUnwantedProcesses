﻿namespace KillUnwantedProcesses.KillableProcesses {

    public class AcrobatNotificationService: BaseKillableProcess {

        private const string APPX_PACKAGE_NAME = "AcrobatNotificationClient";

        public override string name { get; } = "Acrobat Notification Service";

        public override bool shouldKill() {
            return isUwpAppxPackageInstalled(APPX_PACKAGE_NAME);
        }

        public override void kill() {
            uninstallUwpAppxPackage(APPX_PACKAGE_NAME);
        }

    }

}