#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AcroTray: KillableProcess {

    public override string name { get; } = "AcroTray";

    public override string processName { get; } = "acrotray";

    protected override IEnumerable<string> saviorProcesses { get; } = ["Acrobat"];

}