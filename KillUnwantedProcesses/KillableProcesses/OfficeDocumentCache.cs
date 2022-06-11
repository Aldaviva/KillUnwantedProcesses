using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class OfficeDocumentCache: KillableProcess {

    public override string processName { get; } = "MSOSYNC";

    public override string name { get; } = "Office Document Cache";

    public override bool shouldKill() {
        return base.shouldKill() &&
            !isProcessRunning("EXCEL") &&
            !isProcessRunning("WINWORD") &&
            !isProcessRunning("ONENOTE") &&
            !isProcessRunning("POWERPNT.EXE") &&
            !isProcessRunning("OUTLOOK.EXE");
    }

}