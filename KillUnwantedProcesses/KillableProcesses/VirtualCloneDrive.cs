using Microsoft.Win32;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VirtualCloneDrive: BaseKillableProcess {

        private const string ProcessName = "VCDDaemon";

        public override string Name { get; } = "Virtual CloneDrive";

        public override bool ShouldKill() {
            const string registryPath = @"HKEY_CURRENT_USER\Software\Elaborate Bytes\VirtualCloneDrive";
            int numberOfDrives = (int) (Registry.GetValue(registryPath, "NumberOfDrives", 0) ?? 0);

            return numberOfDrives == 0 && !IsProcessRunning("VCDPrefs") && IsProcessRunning(ProcessName);
        }

        public override void Kill() {
            KillProcess(ProcessName);
        }

    }

}