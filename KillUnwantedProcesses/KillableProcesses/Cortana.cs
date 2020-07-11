#nullable enable

namespace KillUnwantedProcesses.KillableProcesses {

    public class Cortana: BaseKillableProcess {

        private const string PROCESS_NAME = "Cortana";

        public override string name { get; } = "Cortana";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && isProcessSuspended(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}