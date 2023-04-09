using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell;

Console.WriteLine("Starting");
Console.WriteLine("Is Phone Link installed: " + isUwpAppxPackageInstalled("Microsoft.YourPhone"));

bool isUwpAppxPackageInstalled(string packageName) {
    InitialSessionState
        iss = InitialSessionState
            .CreateDefault(); //CreateDefault works when published, but crashes when published single-file, because some code tries to Path.Combine(Assembly.Location) which is null or empty
    // InitialSessionState iss = InitialSessionState.CreateDefault(); // CreateDefault2 works in Debug & Release, but spins forever when published
    iss.ExecutionPolicy = ExecutionPolicy.RemoteSigned;
    Console.WriteLine("Created session");
    //
    using Runspace runspace = RunspaceFactory.CreateRunspace(iss);
    Console.WriteLine("Created runspace");
    runspace.Open();
    // Console.WriteLine("Opened runspace version " + runspace.Version);

    PowerShell import = PowerShell.Create(runspace);
    import.AddCommand("Import-Module")
        .AddParameter("Name", "Appx")
        .AddParameter("UseWindowsPowerShell");
    import.Invoke();
    Console.WriteLine("Imported module");

    PowerShell pipeline = PowerShell.Create(runspace);
    pipeline.AddCommand("Get-AppxPackage")
        .AddParameter("Name", packageName);
    Collection<PSObject> results = pipeline.Invoke();
    return results.Count != 0;
}