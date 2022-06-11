#nullable enable

using System.ServiceProcess;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class AutodeskLicensingService: KillableService {

    protected override string serviceName { get; } = "AdskLicensingService";

    public override string name => "Autodesk Licensing Service";

    public override bool shouldKill() {
        return base.shouldKill() && !isProcessRunning("Inventor") && !isProcessRunning("InventorOEM");
    }

    /*
     * Manual mode will make Inventor 2021 take a little longer to start than automatic, since it has to use AdskLicensingInstHelper.exe to start this service, but it does happen
     * successfully without user interaction. The additional delay is about 12 seconds.
     *
     * To improve this further, you could instead launch a batch script or other program to start the licensing service and then Inventor:
     *
     *     @echo off
     *     sc start adsklicensingservice
     *     start "" "InventorOEM.exe %*"
     */
    protected override ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Manual;

}