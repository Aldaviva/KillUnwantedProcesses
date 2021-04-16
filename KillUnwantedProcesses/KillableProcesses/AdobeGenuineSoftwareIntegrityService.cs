#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    public class AdobeGenuineSoftwareIntegrityService: KillableService {

        protected override string serviceName { get; } = "AGSService";

        public override string name { get; } = "Adobe Genuine Software Integrity Service";

    }

}