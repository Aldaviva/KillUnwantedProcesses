namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeCreativeCloud: BaseKillableProcess {

        private const string AdobeDesktopServiceProcessName = "Adobe Desktop Service";

        public override string Name { get; } = "Adobe Creative Cloud";

        public override bool ShouldKill() {
            return !IsProcessRunning("Creative Cloud") && IsProcessRunning(AdobeDesktopServiceProcessName);
        }

        public override void Kill() {
            KillProcess(AdobeDesktopServiceProcessName, true);
        }

    }

}