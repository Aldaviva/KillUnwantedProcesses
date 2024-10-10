#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using Microsoft.Win32;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class VirtualCloneDrive: KillableProcess {

    private const string REGISTRY_PATH = @"HKEY_CURRENT_USER\Software\Elaborate Bytes\VirtualCloneDrive";

    public override string processName { get; } = "VCDDaemon";

    public override string name { get; } = "Virtual CloneDrive";

    protected override IEnumerable<string> saviorProcesses { get; } = ["VCDPrefs"];

    public override bool shouldKill() {
        int numberOfDrives = Registry.GetValue(REGISTRY_PATH, "NumberOfDrives", 0) as int? ?? 0;
        return numberOfDrives == 0 && base.shouldKill();
    }

}