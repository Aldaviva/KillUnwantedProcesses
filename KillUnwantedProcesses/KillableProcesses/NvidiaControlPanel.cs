#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses;

public class NvidiaControlPanel: KillableService {

    protected override string serviceName { get; } = "NVDisplay.ContainerLocalSystem";

    public override string name { get; } = "Nvidia Control Panel";

    protected override ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Manual;

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning("nvcplui");
    }

}