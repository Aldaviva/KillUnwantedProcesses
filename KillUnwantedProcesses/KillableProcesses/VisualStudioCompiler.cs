using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class VisualStudioCompiler: KillableProcess {

    public override string processName { get; } = "VBCSCompiler";

    public override string name { get; } = "Visual Studio Compiler";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning("devenv");
    }

}