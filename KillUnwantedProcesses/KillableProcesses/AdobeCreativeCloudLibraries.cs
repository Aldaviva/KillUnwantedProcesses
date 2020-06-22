﻿    namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCreativeCloudLibraries: BaseKillableProcess {

        private const string PROCESS_NAME = "CCLibrary";

        public override string name { get; } = "Adobe Creative Cloud Libraries";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && !isProcessRunning(AdobeDesktopService.ADOBE_DESKTOP_SERVICE_PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME, true);
        }

    }

}