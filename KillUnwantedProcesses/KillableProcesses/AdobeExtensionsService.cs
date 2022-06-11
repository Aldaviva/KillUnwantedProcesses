#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeExtensionsService: KillableProcess {

    public override string name { get; } = "Adobe Extensions Service";

    public override string processName { get; } = "AdobeExtensionsService.exe";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
    }

}