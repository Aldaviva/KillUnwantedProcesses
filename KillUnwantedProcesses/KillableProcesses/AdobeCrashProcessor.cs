#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCrashProcessor: KillableProcess {

    public override string processName { get; } = "Adobe Crash Processor";

    public override string name { get; } = "Adobe Crash Processor";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
    }

}