#nullable enable

namespace KillUnwantedProcesses.KillableProcesses.Base {

    public interface Killable {

        string name { get; }

        bool shouldKill();

        void kill();

    }

}