#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeGenuineMonitorService: KillableService {

        protected override string serviceName { get; } = "AGMService";

        public override string name { get; } = "Adobe Genuine Monitor Service";

    }

}