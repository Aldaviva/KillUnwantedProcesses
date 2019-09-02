namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCollabSync: BaseKillableProcess {

        private const string ProcessName = "AdobeCollabSync";

        public override string Name { get; } = "Adobe Collaboration Synchronizer";

        public override bool ShouldKill() {
            return !IsProcessRunning("Acrobat") && IsProcessRunning(ProcessName);
        }

        public override void Kill() {
            KillProcess(ProcessName, true);
        }

    }

}