using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace KillUnwantedProcesses {

    /// <summary>
    /// A utility class to determine a process parent.
    /// </summary>
    /// <remarks><a href="https://stackoverflow.com/a/3346055/979493">Source</a></remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct ParentProcessUtilities {

        // These members must match PROCESS_BASIC_INFORMATION
        internal IntPtr Reserved1;
        internal IntPtr PebBaseAddress;
        internal IntPtr Reserved2_0;
        internal IntPtr Reserved2_1;
        internal IntPtr UniqueProcessId;
        internal IntPtr InheritedFromUniqueProcessId;

        [DllImport("ntdll.dll")]
        private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass,
            ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

        /// <summary>
        /// Gets the parent process of the current process.
        /// </summary>
        /// <returns>An instance of the Process class.</returns>
        public static Process GetParentProcess() {
            return GetParentProcess(Process.GetCurrentProcess());
        }

        /// <summary>
        /// Gets the parent process of specified process.
        /// </summary>
        /// <param name="id">The process id.</param>
        /// <returns>An instance of the Process class.</returns>
        public static Process GetParentProcess(int id) {
            Process process = Process.GetProcessById(id);
            return GetParentProcess(process);
        }

        /// <summary>
        /// Gets the parent process of specified process.
        /// </summary>
        /// <param name="child">The child process.</param>
        /// <returns>An instance of the Process class.</returns>
        public static Process GetParentProcess(Process child) {
            return GetParentProcess(child.Handle);
        }

        /// <summary>
        /// Gets the parent process of a specified process.
        /// </summary>
        /// <param name="handle">The process handle.</param>
        /// <returns>An instance of the Process class.</returns>
        public static Process GetParentProcess(IntPtr handle) {
            var pbi = new ParentProcessUtilities();
            int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out int _);
            if (status != 0)
                throw new Win32Exception(status);

            try {
                return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
            } catch (ArgumentException) {
                // not found
                return null;
            }
        }

        public static IEnumerable<Process> GetDescendentProcesses(Process parent) {
            IList<Process> allProcesses = Process.GetProcesses().AsEnumerable().ToList();
            return GetDescendentProcesses(parent, allProcesses).ToList(); //eagerly find child processes, because once we start killing processes, their parent PIDs won't mean anything anymore
        }

        private static IEnumerable<Process> GetDescendentProcesses(Process parent, IList<Process> allProcesses) {
            return allProcesses.SelectMany(descendent => {
                bool isDescendentOfParent = false;
                try {
                    Process descendentParent = GetParentProcess(descendent);
                    isDescendentOfParent = descendentParent?.Id == parent.Id;
                } catch (Exception) {
                    //leave descendentParent null
                }

                if (isDescendentOfParent) {
                    return GetDescendentProcesses(descendent, allProcesses).Prepend(descendent);
                } else {
                    return Enumerable.Empty<Process>();
                }
            });
        }

    }

}