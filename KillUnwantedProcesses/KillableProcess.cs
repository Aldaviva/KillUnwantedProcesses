namespace KillUnwantedProcesses {

    public abstract class KillableProcess {

        public abstract string Name { get; }

        public abstract bool ShouldKill();

        public abstract void Kill();

    }

}