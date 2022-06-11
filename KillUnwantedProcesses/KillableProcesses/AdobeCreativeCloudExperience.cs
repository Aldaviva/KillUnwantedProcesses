#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class AdobeCreativeCloudExperience: KillableProcess {

    public override string processName { get; } = "CCXProcess";

    public override string name { get; } = "Adobe Creative Cloud Experience";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
    }

}