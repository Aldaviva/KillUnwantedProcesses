namespace KillUnwantedProcesses.KillableProcesses {

    public abstract class KillableProcess {

        public abstract string name { get; }

        public abstract bool shouldKill();

        public abstract void kill();

    }

}