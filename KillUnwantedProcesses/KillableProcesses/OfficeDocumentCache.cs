namespace KillUnwantedProcesses.KillableProcesses {

    public class OfficeDocumentCache: BaseKillableProcess {

        private const string PROCESS_NAME = "MSOSYNC";

        public override string name { get; } = "Office Document Cache";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) &&
                   !isProcessRunning("EXCEL") &&
                   !isProcessRunning("WINWORD") &&
                   !isProcessRunning("ONENOTE") &&
                   !isProcessRunning("POWERPNT.EXE") &&
                   !isProcessRunning("OUTLOOK.EXE");
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}