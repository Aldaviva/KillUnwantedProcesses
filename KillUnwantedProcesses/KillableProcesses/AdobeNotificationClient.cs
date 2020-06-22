namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeNotificationClient: BaseKillableProcess {

        private const string PROCESS_NAME = "AdobeNotificationClient.exe";

        public override string name { get; } = "Adobe Notification Client";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}