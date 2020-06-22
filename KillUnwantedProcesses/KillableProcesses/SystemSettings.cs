namespace KillUnwantedProcesses.KillableProcesses {

    public class SystemSettings: BaseKillableProcess {

        private const string PROCESS_NAME = "SystemSettings";

        public override string name { get; } = "System Settings";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && isProcessSuspended(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}