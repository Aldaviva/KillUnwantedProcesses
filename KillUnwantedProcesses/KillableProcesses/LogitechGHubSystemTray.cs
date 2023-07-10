#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class LogitechGHubSystemTray: KillableProcess {

    public override string name { get; } = "Logitech G Hub System Tray";

    public override string processName { get; } = "lghub_system_tray.exe";

    protected override bool alsoKillChildren => false;

}