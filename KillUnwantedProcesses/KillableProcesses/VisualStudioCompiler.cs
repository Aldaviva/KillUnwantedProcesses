#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class VisualStudioCompiler: KillableProcess {

    public override string processName { get; } = "VBCSCompiler";

    public override string name { get; } = "Visual Studio Compiler";

    protected override IEnumerable<string> saviorProcesses { get; } = ["devenv"];

}