#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using Microsoft.Win32;

namespace KillUnwantedProcesses.KillableProcesses;

public class VirtualCloneDrive: KillableProcess {

    public override string processName { get; } = "VCDDaemon";

    public override string name { get; } = "Virtual CloneDrive";

    public override bool shouldKill() {
        const string REGISTRY_PATH  = @"HKEY_CURRENT_USER\Software\Elaborate Bytes\VirtualCloneDrive";
        int          numberOfDrives = (int) (Registry.GetValue(REGISTRY_PATH, "NumberOfDrives", 0) ?? 0);

        return numberOfDrives == 0 && !isProcessRunning("VCDPrefs") && base.shouldKill();
    }

}