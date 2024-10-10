#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeNotificationClient: KillableBase {

    private const string APPX_PACKAGE_NAME = "AdobeNotificationClient";

    public override string name { get; } = "Adobe Notification Client";

    public override bool shouldKill() => isUwpAppxPackageInstalled(APPX_PACKAGE_NAME);

    public override void kill() => uninstallUwpAppxPackage(APPX_PACKAGE_NAME);

}