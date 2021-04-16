#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using KillUnwantedProcesses.KillableProcesses;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses {

    public class UnwantedProcessKiller {

        private readonly IReadOnlyCollection<Killable> killableProcesses = new HashSet<Killable> {
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
            new LogitechGHub(),
            new OfficeDocumentCache(),
            new VirtualCloneDrive(),
            new VisualStudio(),
            new VmAuthdService(),
            new VmnetDhcpService(),
            new VmUsbArbService(),
            new VmwareHostd(),
            new VmwareNatService(),
            new WindowsImageAcquisition()
        };

        public static void Main() {
            new UnwantedProcessKiller().killUnwantedProcesses();
        }

        private void killUnwantedProcesses() {
            var processesToCheck = new HashSet<Killable>(killableProcesses);
            var processesKilledInLastIteration = new HashSet<Killable>();
            int loops = 0;

            do {
                processesKilledInLastIteration.Clear();

                foreach (Killable processToKill in processesToCheck.Where(process => process.shouldKill())) {
                    Console.WriteLine($"Killing {processToKill.name}");
                    processToKill.kill();
                    processesKilledInLastIteration.Add(processToKill); //don't recheck this program on the next loop
                }

                processesToCheck.RemoveWhere(processesKilledInLastIteration.Contains);

            } while ((processesKilledInLastIteration.Count > 0) && (++loops < killableProcesses.Count));
        }

    }

}