using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class AdobeFlashUpdater: KillableService {

    public override string name { get; } = "Adobe Flash Updater";

    protected override string serviceName { get; } = "AdobeFlashPlayerUpdateSvc";

}