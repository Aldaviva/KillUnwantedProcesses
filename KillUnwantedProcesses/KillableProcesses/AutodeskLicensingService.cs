﻿#nullable enable

using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AutodeskLicensingService: BaseKillableProcess {

        private const string SERVICE_NAME = "AdskLicensingService";

        public override string name => "Autodesk Licensing Service";

        public override bool shouldKill() {
            return isServiceRunning(SERVICE_NAME) && !isProcessRunning("Inventor") && !isProcessRunning("InventorOEM");
        }

        public override void kill() {
            /*
             * Manual mode will make Inventor take a little longer to start than automatic, since it has to use AdskLicensingInstHelper.exe to start this service, but it does happen
             * successfully without user interaction. The additional delay is about 12 seconds.
             *
             * To improve this further, you could instead launch a batch script or other program to start the licensing service and then Inventor:
             *
             *     @echo off
             *     sc start adsklicensingservice
             *     start "" "InventorOEM.exe %*"
             */
            stopService(SERVICE_NAME, ServiceStartMode.Manual);
        }

    }

}