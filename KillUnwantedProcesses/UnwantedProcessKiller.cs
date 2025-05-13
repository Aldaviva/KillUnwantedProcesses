#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KillUnwantedProcesses;

public static class UnwantedProcessKiller {

    public static int Main() {
        try {
            int killCount = killUnwantedProcesses();
            // MessageBox.Show($"Done, killed {killCount} {(killCount == 1 ? "process" : "processes")}.", nameof(KillUnwantedProcesses), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 0;
        } catch (Exception e) when (e is not OutOfMemoryException) {
            MessageBox.Show($"Uncaught exception: {e}", nameof(KillUnwantedProcesses), MessageBoxButtons.OK, MessageBoxIcon.Error);
            return 1;
        }
    }

    private static int killUnwantedProcesses() {
        HashSet<Killable> processesToCheck               = new(ProcessesToKill.PROCESSES);
        ISet<Killable>    processesKilledInLastIteration = new HashSet<Killable>();
        int               loops                          = 0;
        int               killCount                      = 0;

        do {
            processesKilledInLastIteration.Clear();

            foreach (Killable processToKill in processesToCheck.Where(process => process.shouldKill())) {
                Console.WriteLine($"Killing {processToKill.name}");
                processToKill.kill();
                killCount++;
                processesKilledInLastIteration.Add(processToKill); //don't recheck this program on the next loop
            }

            processesToCheck.RemoveWhere(processesKilledInLastIteration.Contains);

        } while (processesKilledInLastIteration.Count > 0 && ++loops < ProcessesToKill.PROCESSES.Count);

        return killCount;
    }

}