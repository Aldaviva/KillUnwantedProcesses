namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeDesktopService: BaseKillableProcess {

        internal const string ADOBE_DESKTOP_SERVICE_PROCESS_NAME = "Adobe Desktop Service";

        public override string name { get; } = "Adobe Desktop Service";

        public override bool shouldKill() {
            return !isProcessRunning("Creative Cloud") && isProcessRunning(ADOBE_DESKTOP_SERVICE_PROCESS_NAME);
        }

        public override void kill() {
            killProcess(ADOBE_DESKTOP_SERVICE_PROCESS_NAME, true);
        }

    }

}