#nullable enable

namespace KillUnwantedProcesses.KillableProcesses.Base {

    public abstract class KillableProcess: KillableBase {

        public abstract string processName { get; }

        protected virtual bool alsoKillChildren { get; } = true;

        public override bool shouldKill() {
            return isProcessRunning(processName);
        }

        public override void kill() {
            killProcess(processName, alsoKillChildren);
        }

    }

}