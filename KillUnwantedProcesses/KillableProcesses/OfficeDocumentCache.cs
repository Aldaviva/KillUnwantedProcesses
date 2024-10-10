#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class OfficeDocumentCache: KillableProcess {

    public override string processName { get; } = "MSOSYNC";

    public override string name { get; } = "Office Document Cache";

    protected override IEnumerable<string> saviorProcesses { get; } = ["EXCEL", "WINWORD", "ONENOTE", "POWERPNT", "OUTLOOK"];

}