namespace KillUnwantedProcesses.KillableProcesses {

    public class VisualStudio: BaseKillableProcess {

        private const string ProcessName = "VBCSCompiler";

        public override string Name { get; } = "Visual Studio Compiler";

        public override bool ShouldKill() {
            return IsProcessRunning(ProcessName) && !IsProcessRunning("devenv");
        }

        public override void Kill() {
            KillProcess(ProcessName);
        }

    }

}