#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace KillUnwantedProcesses.Utilities {

    /// <summary>A utility class to determine a process parent.</summary>
    /// <remarks><a href="https://stackoverflow.com/a/3346055/979493">Source</a></remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct ParentProcessUtilities {

        // These members must match PROCESS_BASIC_INFORMATION
        private readonly IntPtr Reserved1;
        private readonly IntPtr PebBaseAddress;
        private readonly IntPtr Reserved2_0;
        private readonly IntPtr Reserved2_1;
        private readonly IntPtr UniqueProcessId;
        private readonly IntPtr InheritedFromUniqueProcessId;

        [DllImport("ntdll.dll")]
        private static extern int NtQueryInformationProcess(IntPtr                     processHandle,      int processInformationClass,
                                                            ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

        /// <summary>Gets the parent process of the current process.</summary>
        /// <returns>An instance of the Process class.</returns>
        public static Process? getParentProcess() {
            return getParentProcess(Process.GetCurrentProcess());
        }

        /// <summary>Gets the parent process of specified process.</summary>
        /// <param name="id">The process id.</param>
        /// <returns>An instance of the Process class.</returns>
        public static Process? getParentProcess(int id) {
            Process? process = Process.GetProcessById(id);
            return getParentProcess(process);
        }

        /// <summary>Gets the parent process of specified process.</summary>
        /// <param name="child">The child process.</param>
        /// <returns>An instance of the Process class.</returns>
        public static Process? getParentProcess(Process child) {
            return getParentProcess(child.Handle);
        }

        /// <summary>Gets the parent process of a specified process.</summary>
        /// <param name="handle">The process handle.</param>
        /// <returns>An instance of the Process class.</returns>
        private static Process? getParentProcess(IntPtr handle) {
            ParentProcessUtilities pbi    = new();
            int                    status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out int _);
            if (status != 0) {
                return null;
            }

            try {
                return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
            } catch (ArgumentException) {
                // not found
                return null;
            }
        }

        public static IEnumerable<Process> getDescendentProcesses(Process parent) {
            IList<Process> allProcesses = Process.GetProcesses().AsEnumerable().ToList();
            return getDescendentProcesses(parent, allProcesses)
                .ToList(); //eagerly find child processes, because once we start killing processes, their parent PIDs won't mean anything anymore
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local (Avoid double enumeration heuristic)
        private static IEnumerable<Process> getDescendentProcesses(Process parent, IList<Process> allProcesses) {
            return allProcesses.SelectMany(descendent => {
                bool isDescendentOfParent = false;
                try {
                    Process? descendentParent = getParentProcess(descendent);
                    isDescendentOfParent = descendentParent?.Id == parent.Id;
                } catch (Exception) {
                    //leave isDescendentOfParent false
                }

                return isDescendentOfParent
                    ? getDescendentProcesses(descendent, allProcesses).Prepend(descendent)
                    : Enumerable.Empty<Process>();
            });
        }

    }

}