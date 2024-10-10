#nullable enable

using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses.Base;

public abstract class KillableService: KillableBase {

    protected abstract string serviceName { get; }
    protected virtual ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Disabled;

    public override bool shouldKill() => isServiceRunning(serviceName) && base.shouldKill();

    public override void kill() => stopService(serviceName, desiredServiceStartMode);

}