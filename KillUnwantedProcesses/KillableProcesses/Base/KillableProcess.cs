#nullable enable

namespace KillUnwantedProcesses.KillableProcesses.Base; 

public abstract class KillableProcess: KillableBase {

    /// <summary>
    /// The file basename of the process to kill. The .EXE file extension is optional.
    /// </summary>
    public abstract string processName { get; }

    protected virtual bool alsoKillChildren { get; } = true;

    public override bool shouldKill() {
        return isProcessRunning(processName) && base.shouldKill();
    }

    public override void kill() {
        killProcess(processName, alsoKillChildren);
    }

}