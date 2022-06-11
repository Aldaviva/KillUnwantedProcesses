#nullable enable

using System.Collections.Generic;
using System.ServiceProcess;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AutodeskAccessServiceHost: KillableService {

    public override string name { get; } = "Autodesk Access Service Host";

    protected override string serviceName { get; } = "Autodesk Access Service Host";

    protected override ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Manual;

    protected override IEnumerable<string> saviorProcesses { get; } = new[] { "Inventor.exe" };

}