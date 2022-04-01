using System.Collections.Generic;
using KillUnwantedProcesses.KillableProcesses;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses {

    public static class ProcessesToKill {

        public static readonly IReadOnlyCollection<Killable> PROCESSES = new HashSet<Killable> {
            new AcrobatNotificationService(),
            new AcroTray(),
            new AdobeAcrobatUpdater(),
            new AdobeCollabSync(),
            new AdobeCoreSync(),
            new AdobeCreativeCloudExperience(),
            new AdobeCreativeCloudLibraries(),
            new AdobeDesktopService(),
            new AdobeFlashUpdater(),
            new AdobeGenuineMonitorService(),
            new AdobeGenuineSoftwareIntegrityService(),
            new AdobeNotificationClient(),
            new AdobeUpdateService(),
            new AutodeskLicensingService(),
            new DotNetRuntimeOptimizationService(),
            new FlexNetLicensingService(),
            new GpgAgent(),
            // new LogitechGHub(),
            new NvidiaControlPanel(),
            new OfficeDocumentCache(),
            new VirtualCloneDrive(),
            new VisualStudioCompiler(),
            new VmAuthdService(),
            new VmnetDhcpService(),
            new VmUsbArbService(),
            new VmwareHostd(),
            new VmwareNatService(),
            // new WindowsImageAcquisition()
        };

    }

}