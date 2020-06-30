namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCoreSync: BaseKillableProcess {

        private const string PROCESS_NAME = "CoreSync";

        public override string name { get; } = "Adobe Sync";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && !isProcessRunning(AdobeDesktopService.ADOBE_DESKTOP_SERVICE_PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME, true);
        }

    }

}