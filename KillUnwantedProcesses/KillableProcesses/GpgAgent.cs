#nullable enable

using System.Collections.Generic;
using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class GpgAgent: KillableProcess {

    public override string name { get; } = "GPG Agent";

    public override string processName { get; } = "gpg-agent";

    protected override IEnumerable<string> saviorProcesses => new[] { "gpg", "pinentry", "git", "bash", "ConEmu", "ConEmuC64", "git-bash", "mintty", "GitExtensions", "devenv", "eclipse" };

}