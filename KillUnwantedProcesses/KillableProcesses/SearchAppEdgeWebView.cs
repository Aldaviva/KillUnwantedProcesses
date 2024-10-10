#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using KillUnwantedProcesses.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KillUnwantedProcesses.KillableProcesses;

public class SearchAppEdgeWebView: KillableBase {

    public override string name { get; } = "MSEdgeWebView2 children of SearchApp";

    public override void kill() {
        foreach (Process edgeWebView in getProcessesToKill()) {
            using (edgeWebView) {
                killProcess(edgeWebView, true);
            }
        }
    }

    public override bool shouldKill() {
        using Process? any = getProcessesToKill().FirstOrDefault();
        return any != null;
    }

    private static IEnumerable<Process> getProcessesToKill() {
        foreach (Process edgeWebView in Process.GetProcessesByName("msedgewebview2")) {
            using Process? parent = ParentProcessUtilities.getParentProcess(edgeWebView);
            if ("SearchApp".Equals(parent?.ProcessName, StringComparison.OrdinalIgnoreCase)) {
                yield return edgeWebView;
            }
        }
    }

}