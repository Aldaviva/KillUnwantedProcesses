using System;
using System.Collections.Generic;
using KillUnwantedProcesses.KillableProcesses;

namespace KillUnwantedProcesses {

    public class UnwantedProcessKiller {

        private readonly ICollection<KillableProcess> killableProcesses = new HashSet<KillableProcess>();

        public static void Main() {
            new UnwantedProcessKiller().KillUnwantedProcesses();
        }

        public UnwantedProcessKiller() {
            killableProcesses.Add(new AdobeCollabSync());
            killableProcesses.Add(new AdobeCreativeCloud());
            killableProcesses.Add(new LogitechGHub());
            killableProcesses.Add(new NvidiaControlPanel());
            killableProcesses.Add(new OfficeDocumentCache());
            killableProcesses.Add(new VirtualCloneDrive());
            killableProcesses.Add(new VisualStudio());
            killableProcesses.Add(new WindowsImageAcquisition());
        }

        public void KillUnwantedProcesses() {
            foreach (KillableProcess killableProcess in killableProcesses) {
                if (killableProcess.ShouldKill()) {
                    Console.WriteLine($"Killing {killableProcess.Name}");
                    killableProcess.Kill();
                }
            }
        }

    }

}