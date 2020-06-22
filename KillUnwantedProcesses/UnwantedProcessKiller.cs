using System;
using System.Collections.Generic;
using KillUnwantedProcesses.KillableProcesses;

namespace KillUnwantedProcesses {

    public class UnwantedProcessKiller {

        private readonly ICollection<KillableProcess> killableProcesses = new HashSet<KillableProcess>();

        public static void Main() {
            new UnwantedProcessKiller().killUnwantedProcesses();
        }

        private UnwantedProcessKiller() {
            killableProcesses.Add(new AcroTray());
            killableProcesses.Add(new AdobeAcrobatUpdater());
            killableProcesses.Add(new AdobeCollabSync());
            killableProcesses.Add(new AdobeCreativeCloudExperience());
            killableProcesses.Add(new AdobeCreativeCloudLibraries());
            killableProcesses.Add(new AdobeCreativeCloudUpdater());
            killableProcesses.Add(new AdobeDesktopService());
            killableProcesses.Add(new AdobeFlashUpdater());
            killableProcesses.Add(new AdobeNotificationClient());
            killableProcesses.Add(new DotNetRuntimeOptimizationService());
            killableProcesses.Add(new LogitechGHub());
            killableProcesses.Add(new NvidiaControlPanel());
            killableProcesses.Add(new OfficeDocumentCache());
            killableProcesses.Add(new SystemSettings());
            killableProcesses.Add(new VirtualCloneDrive());
            killableProcesses.Add(new VisualStudio());
            killableProcesses.Add(new VmAuthdService());
            killableProcesses.Add(new VmnetDhcpService());
            killableProcesses.Add(new VmUsbArbService());
            killableProcesses.Add(new VmwareHostd());
            killableProcesses.Add(new VmwareNatService());
            killableProcesses.Add(new WindowsImageAcquisition());
            killableProcesses.Add(new WindowsStore());
        }

        private void killUnwantedProcesses() {
            foreach (KillableProcess processToKill in killableProcesses) {
                if (processToKill.shouldKill()) {
                    Console.WriteLine($"Killing {processToKill.name}");
                    processToKill.kill();
                }
            }
        }

    }

}