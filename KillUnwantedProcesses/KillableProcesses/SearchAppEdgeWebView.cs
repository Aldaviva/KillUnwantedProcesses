#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unfucked.Windows;

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
        Process[] webviews = Process.GetProcessesByName("msedgewebview2");
        foreach (Process edgeWebView in webviews) {
            using Process? parent = edgeWebView.GetParentProcess();
            try {
                string? parentName = parent?.ProcessName;
                if (!"SearchApp".Equals(parentName, StringComparison.OrdinalIgnoreCase) &&
                    !"SearchHost".Equals(parentName, StringComparison.OrdinalIgnoreCase)) {
                    continue;
                }
            } catch (InvalidOperationException) {
                continue;
            }
            yield return edgeWebView;
        }
    }

}