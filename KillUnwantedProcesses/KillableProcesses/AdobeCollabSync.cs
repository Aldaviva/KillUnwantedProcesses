using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class AdobeCollabSync: KillableProcess {

    public override string processName { get; } = "AdobeCollabSync";

    public override string name { get; } = "Adobe Collaboration Synchronizer";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning("Acrobat");
    }

}