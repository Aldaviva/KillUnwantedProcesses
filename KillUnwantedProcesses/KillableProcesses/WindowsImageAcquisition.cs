#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class WindowsImageAcquisition: KillableService {

    protected override string serviceName { get; } = "stisvc";

    public override string name { get; } = "Windows Image Acquisition";

}