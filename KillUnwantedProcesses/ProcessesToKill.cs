#nullable enable

using KillUnwantedProcesses.KillableProcesses;
using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses;

public static class ProcessesToKill {

    public static readonly IReadOnlyCollection<Killable> PROCESSES = new HashSet<Killable> {
        new AcrobatNotificationService(),
        new AcroTray(),
        new AdobeAcrobatUpdater(),
        new AdobeCollabSync(),
        new AdobeCoreSync(),
        new AdobeCrashProcessor(),
        new AdobeCreativeCloudExperience(),
        new AdobeCreativeCloudLibraries(),
        new AdobeDesktopService(),
        new AdobeExtensionsService(),
        new AdobeGenuineMonitorService(),
        new AdobeGenuineSoftwareIntegrityService(),
        new AdobeNotificationClient(),
        new AdobeUpdateService(),
        new AutodeskAccessServiceHost(),
        new AutodeskLicensingService(),
        new FlexNetLicensingService(),
        new LogitechGHubSystemTray(),
        new NvidiaControlPanel(),
        new OfficeDocumentCache(),
        new VirtualCloneDrive(),
        new VisualStudioCompiler(),
        new VmAuthdService(),
        new VmnetDhcpService(),
        new VmwareHostd(),
        new VmwareNatService()
    };

}