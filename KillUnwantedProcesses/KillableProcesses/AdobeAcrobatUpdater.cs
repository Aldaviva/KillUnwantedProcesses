#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeAcrobatUpdater: KillableService {

    public override string name { get; } = "Adobe Acrobat Updater";

    protected override string serviceName { get; } = "AdobeARMservice";

}