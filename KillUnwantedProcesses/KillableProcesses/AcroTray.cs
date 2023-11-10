#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AcroTray: KillableProcess {

    public override string name { get; } = "AcroTray";

    public override string processName { get; } = "acrotray";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning("Acrobat");
    }

}