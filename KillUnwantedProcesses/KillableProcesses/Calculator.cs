#nullable enable

namespace KillUnwantedProcesses.KillableProcesses {

    public class Calculator: BaseKillableProcess {

        private const string PROCESS_NAME = "Calculator";

        public override string name { get; } = "Calculator";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && isProcessSuspended(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}