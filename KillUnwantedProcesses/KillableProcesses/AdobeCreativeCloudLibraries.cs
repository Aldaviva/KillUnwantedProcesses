using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class AdobeCreativeCloudLibraries: KillableProcess {

    public override string processName { get; } = "CCLibrary";

    public override string name { get; } = "Adobe Creative Cloud Libraries";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
    }

}