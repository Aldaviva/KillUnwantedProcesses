﻿using KillUnwantedProcesses.KillableProcesses.Base;

#nullable enable

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeUpdateService: KillableService {

        protected override string serviceName { get; } = "AdobeUpdateService";

        public override string name { get; } = "Adobe Update Service";

        public override bool shouldKill() {
            return base.shouldKill() && !isProcessRunning(new AdobeDesktopService().processName);
        }
        

    }

}