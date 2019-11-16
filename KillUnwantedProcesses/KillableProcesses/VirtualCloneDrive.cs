using Microsoft.Win32;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VirtualCloneDrive: BaseKillableProcess {

        private const string PROCESS_NAME = "VCDDaemon";

        public override string name { get; } = "Virtual CloneDrive";

        public override bool shouldKill() {
            const string registryPath = @"HKEY_CURRENT_USER\Software\Elaborate Bytes\VirtualCloneDrive";
            int numberOfDrives = (int) (Registry.GetValue(registryPath, "NumberOfDrives", 0) ?? 0);

            return numberOfDrives == 0 && !isProcessRunning("VCDPrefs") && isProcessRunning(PROCESS_NAME);
        }

        public override void kill() {
            killProcess(PROCESS_NAME);
        }

    }

}