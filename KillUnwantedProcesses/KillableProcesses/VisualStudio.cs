namespace KillUnwantedProcesses.KillableProcesses {

    public class VisualStudio: BaseKillableProcess {

        private const string PROCESS_NAME = "VBCSCompiler";

        public override string name { get; } = "Visual Studio Compiler";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && !isProcessRunning("devenv");
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}