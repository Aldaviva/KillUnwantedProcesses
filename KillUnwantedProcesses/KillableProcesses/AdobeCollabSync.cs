namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCollabSync: BaseKillableProcess {

        private const string PROCESS_NAME = "AdobeCollabSync";

        public override string name { get; } = "Adobe Collaboration Synchronizer";

        public override bool shouldKill() {
            return !isProcessRunning("Acrobat") && isProcessRunning(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME, true);
        }

    }

}