#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    /// <summary>
    ///     Disabling this service sometimes causes the Creative Cloud apps tab to not load with the error message "Page Unavailable. We can't load this content, Please try again later."
    ///     It's not 100% reproducible, possibly due to long-running timeouts from genuine software checks. If this keeps happening, this service's start type may need to remain Automatic.
    /// </summary>
    public class AdobeGenuineMonitorService: KillableService {

        protected override string serviceName { get; } = "AGMService";

        public override string name { get; } = "Adobe Genuine Monitor Service";

    }

}