#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeExtensionsService: KillableProcess {

    public override string name { get; } = "Adobe Extensions Service";

    public override string processName { get; } = "AdobeExtensionsService.exe";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}