#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses {

    public static class UnwantedProcessKiller {

        public static void Main() {
            killUnwantedProcesses();
        }

        private static void killUnwantedProcesses() {
            HashSet<Killable> processesToCheck               = new(ProcessesToKill.PROCESSES);
            HashSet<Killable> processesKilledInLastIteration = new();
            int               loops                          = 0;

            do {
                processesKilledInLastIteration.Clear();

                foreach (Killable processToKill in processesToCheck.Where(process => process.shouldKill())) {
                    Console.WriteLine($"Killing {processToKill.name}");
                    processToKill.kill();
                    processesKilledInLastIteration.Add(processToKill); //don't recheck this program on the next loop
                }

                processesToCheck.RemoveWhere(processesKilledInLastIteration.Contains);

            } while (processesKilledInLastIteration.Count > 0 && ++loops < ProcessesToKill.PROCESSES.Count);
        }

    }

}