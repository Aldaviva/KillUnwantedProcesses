using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses {

    public class VmnetDhcpService: KillableService {

        protected override string serviceName { get; }= "VMnetDHCP";

        public override string name { get; } = "VMware DHCP Service";

    }

}