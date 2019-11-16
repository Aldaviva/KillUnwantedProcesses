namespace KillUnwantedProcesses.KillableProcesses {

    public class AcroTray: BaseKillableProcess {

        private const string PROCESS_NAME = "acrotray";

        public override string name { get; } = "AcroTray";

        public override bool shouldKill() {
            return isProcessRunning(PROCESS_NAME) && !isProcessRunning("Acrobat");
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}