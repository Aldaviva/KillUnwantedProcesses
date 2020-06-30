#nullable enable

using System;
using System.Collections.Generic;
using KillUnwantedProcesses.KillableProcesses;

namespace KillUnwantedProcesses {

    public class UnwantedProcessKiller {

        private const int MAX_LOOPS = 32;

        private readonly IReadOnlyCollection<KillableProcess> killableProcesses = new HashSet<KillableProcess> {
            new AcroTray(),
            new AdobeAcrobatUpdater(),
            new AdobeCollabSync(),
            new AdobeCoreSync(),
            new AdobeCreativeCloudExperience(),
            new AdobeCreativeCloudLibraries(),
            new AdobeCreativeCloudUpdater(),
            new AdobeDesktopService(),
            new AdobeFlashUpdater(),
            new AdobeGenuineSoftwareIntegrityService(),
            new AdobeNotificationClient(),
            new DotNetRuntimeOptimizationService(),
            new LogitechGHub(),
            new NvidiaControlPanel(),
            new OfficeDocumentCache(),
            new SystemSettings(),
            new VirtualCloneDrive(),
            new VisualStudio(),
            new VmAuthdService(),
            new VmnetDhcpService(),
            new VmUsbArbService(),
            new VmwareHostd(),
            new VmwareNatService(),
            new WindowsImageAcquisition(),
            new WindowsStore()
        };

        public static void Main() {
            new UnwantedProcessKiller().killUnwantedProcesses();
        }

        private void killUnwantedProcesses() {
            var processesToCheck = new HashSet<KillableProcess>(killableProcesses);
            var processesKilledInLastIteration = new HashSet<KillableProcess>();
            int loops = 0;

            do {
                processesKilledInLastIteration.Clear();

                foreach (KillableProcess processToKill in processesToCheck) {
                    if (processToKill.shouldKill()) {
                        Console.WriteLine($"Killing {processToKill.name}");
                        processToKill.kill();
                        processesKilledInLastIteration.Add(processToKill); //don't recheck this program on the next loop
                    }
                }

                processesToCheck.RemoveWhere(processesKilledInLastIteration.Contains);

            } while (processesKilledInLastIteration.Count > 0 && ++loops < MAX_LOOPS);
        }

    }

}