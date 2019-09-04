namespace KillUnwantedProcesses.KillableProcesses {

    public class OfficeDocumentCache: BaseKillableProcess {

        private const string ProcessName = "MSOSYNC";

        public override string Name { get; } = "Office Document Cache";

        public override bool ShouldKill() {
            return IsProcessRunning(ProcessName) && !IsProcessRunning("EXCEL") && !IsProcessRunning("WINWORD") &&
                   !IsProcessRunning("ONENOTE");
        }

        public override void Kill() {
            KillProcess(ProcessName);
        }

    }

}