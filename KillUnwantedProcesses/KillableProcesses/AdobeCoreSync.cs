using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCoreSync: KillableProcess {

        public override string processName { get; } = "CoreSync";

        public override string name { get; } = "Adobe Sync";

        public override bool shouldKill() {
            return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
        }

    }

}