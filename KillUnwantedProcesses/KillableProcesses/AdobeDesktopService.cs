#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeDesktopService: KillableProcess {

    public override string processName { get; } = "Adobe Desktop Service";

    public override string name { get; } = "Adobe Desktop Service";

    public override bool shouldKill() {
        return !isProcessRunning("Creative Cloud") && base.shouldKill();
    }

}