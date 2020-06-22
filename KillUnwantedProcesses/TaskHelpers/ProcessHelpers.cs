#nullable enable

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using KillUnwantedProcesses.Utilities;

namespace KillUnwantedProcesses.TaskHelpers {

    internal static class ProcessHelpers {

        public static void killProcess(string processName, bool alsoKillChildren = false) {
            processName = stripExeSuffix(processName);
            Process[] processesToKill = Process.GetProcessesByName(processName);
            foreach (Process processToKill in processesToKill) {
                using Process kill = processToKill;
                if (alsoKillChildren) {
                    foreach (Process descendentToKill in ParentProcessUtilities.getDescendentProcesses(processToKill)) {
                        try {
                            killProcess(descendentToKill);
                        } catch (Exception) {
                            //probably already closed or can't be killed
                        }
                    }
                }

                try {
                    killProcess(processToKill);
                } catch (Exception) {
                    //probably already closed or can't be killed
                }
            }
        }

        private static void killProcess(Process process) {
            Console.WriteLine($"Killing {process.ProcessName} ({process.Id})");
            process.Kill();
        }

        public static bool isProcessRunning(string processName) {
            processName = stripExeSuffix(processName);
            using Process? process = Process.GetProcessesByName(processName).FirstOrDefault();
            return process != null;
        }

        private static string stripExeSuffix(string processName) {
            return Regex.Replace(processName, @"\.exe$", string.Empty, RegexOptions.IgnoreCase);
        }

        public static bool isProcessSuspended(string processName) {
            processName = stripExeSuffix(processName);
            Process? process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process != null) {
                NtStatus returnCode = NtQueryInformationProcess(process.Handle, ProcessInfoClass.PROCESS_BASIC_INFORMATION,
                    out ProcessExtendedBasicInformation processBasicInformation, Marshal.SizeOf<ProcessExtendedBasicInformation>(),
                    out int resultSize);

                if (returnCode == NtStatus.SUCCESS) {
                    return (processBasicInformation.flags & ProcessExtendedBasicInformation.Flags.IS_FROZEN) != 0;
                }
            }

            return false;
        }

        #region pinvoke

        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern NtStatus NtQueryInformationProcess(IntPtr hProcess, ProcessInfoClass processInfoClass,
            out ProcessExtendedBasicInformation processExtendedBasicInformation, int inputSize, out int resultSize);

        internal struct ProcessExtendedBasicInformation {

            public UIntPtr size;
            public ProcessBasicInformation basicInfo;
            public Flags flags;

            [Flags]
            public enum Flags: uint {

                IS_PROTECTED_PROCESS = 1 << 0,
                IS_WOW64_PROCESS = 1 << 1,
                IS_PROCESS_DELETING = 1 << 2,
                IS_CROSS_SESSION_CREATE = 1 << 3,
                IS_FROZEN = 1 << 4,
                IS_BACKGROUND = 1 << 5,
                IS_STRONGLY_NAMED = 1 << 6,
                IS_SECURE_PROCESS = 1 << 7,
                IS_SUBSYSTEM_PROCESS = 1 << 8,
                SPARE_BITS = 1 << 9

            }

        }

        internal struct ProcessBasicInformation {

            public NtStatus exitStatus;
            public IntPtr pebBaseAddress;
            public UIntPtr affinityMask;
            public int basePriority;
            public UIntPtr uniqueProcessId;
            public UIntPtr inheritedFromUniqueProcessId;

        }

        internal enum NtStatus: uint {

            // Success
            SUCCESS = 0x00000000,
            WAIT0 = 0x00000000,
            WAIT1 = 0x00000001,
            WAIT2 = 0x00000002,
            WAIT3 = 0x00000003,
            WAIT63 = 0x0000003f,
            ABANDONED = 0x00000080,
            ABANDONED_WAIT0 = 0x00000080,
            ABANDONED_WAIT1 = 0x00000081,
            ABANDONED_WAIT2 = 0x00000082,
            ABANDONED_WAIT3 = 0x00000083,
            ABANDONED_WAIT63 = 0x000000bf,
            USER_APC = 0x000000c0,
            KERNEL_APC = 0x00000100,
            ALERTED = 0x00000101,
            TIMEOUT = 0x00000102,
            PENDING = 0x00000103,
            REPARSE = 0x00000104,
            MORE_ENTRIES = 0x00000105,
            NOT_ALL_ASSIGNED = 0x00000106,
            SOME_NOT_MAPPED = 0x00000107,
            OP_LOCK_BREAK_IN_PROGRESS = 0x00000108,
            VOLUME_MOUNTED = 0x00000109,
            RX_ACT_COMMITTED = 0x0000010a,
            NOTIFY_CLEANUP = 0x0000010b,
            NOTIFY_ENUM_DIR = 0x0000010c,
            NO_QUOTAS_FOR_ACCOUNT = 0x0000010d,
            PRIMARY_TRANSPORT_CONNECT_FAILED = 0x0000010e,
            PAGE_FAULT_TRANSITION = 0x00000110,
            PAGE_FAULT_DEMAND_ZERO = 0x00000111,
            PAGE_FAULT_COPY_ON_WRITE = 0x00000112,
            PAGE_FAULT_GUARD_PAGE = 0x00000113,
            PAGE_FAULT_PAGING_FILE = 0x00000114,
            CRASH_DUMP = 0x00000116,
            REPARSE_OBJECT = 0x00000118,
            NOTHING_TO_TERMINATE = 0x00000122,
            PROCESS_NOT_IN_JOB = 0x00000123,
            PROCESS_IN_JOB = 0x00000124,
            PROCESS_CLONED = 0x00000129,
            FILE_LOCKED_WITH_ONLY_READERS = 0x0000012a,
            FILE_LOCKED_WITH_WRITERS = 0x0000012b,

            // Informational
            INFORMATIONAL = 0x40000000,
            OBJECT_NAME_EXISTS = 0x40000000,
            THREAD_WAS_SUSPENDED = 0x40000001,
            WORKING_SET_LIMIT_RANGE = 0x40000002,
            IMAGE_NOT_AT_BASE = 0x40000003,
            REGISTRY_RECOVERED = 0x40000009,

            // Warning
            WARNING = 0x80000000,
            GUARD_PAGE_VIOLATION = 0x80000001,
            DATATYPE_MISALIGNMENT = 0x80000002,
            BREAKPOINT = 0x80000003,
            SINGLE_STEP = 0x80000004,
            BUFFER_OVERFLOW = 0x80000005,
            NO_MORE_FILES = 0x80000006,
            HANDLES_CLOSED = 0x8000000a,
            PARTIAL_COPY = 0x8000000d,
            DEVICE_BUSY = 0x80000011,
            INVALID_EA_NAME = 0x80000013,
            EA_LIST_INCONSISTENT = 0x80000014,
            NO_MORE_ENTRIES = 0x8000001a,
            LONG_JUMP = 0x80000026,
            DLL_MIGHT_BE_INSECURE = 0x8000002b,

            // Error
            ERROR = 0xc0000000,
            UNSUCCESSFUL = 0xc0000001,
            NOT_IMPLEMENTED = 0xc0000002,
            INVALID_INFO_CLASS = 0xc0000003,
            INFO_LENGTH_MISMATCH = 0xc0000004,
            ACCESS_VIOLATION = 0xc0000005,
            IN_PAGE_ERROR = 0xc0000006,
            PAGEFILE_QUOTA = 0xc0000007,
            INVALID_HANDLE = 0xc0000008,
            BAD_INITIAL_STACK = 0xc0000009,
            BAD_INITIAL_PC = 0xc000000a,
            INVALID_CID = 0xc000000b,
            TIMER_NOT_CANCELED = 0xc000000c,
            INVALID_PARAMETER = 0xc000000d,
            NO_SUCH_DEVICE = 0xc000000e,
            NO_SUCH_FILE = 0xc000000f,
            INVALID_DEVICE_REQUEST = 0xc0000010,
            END_OF_FILE = 0xc0000011,
            WRONG_VOLUME = 0xc0000012,
            NO_MEDIA_IN_DEVICE = 0xc0000013,
            NO_MEMORY = 0xc0000017,
            NOT_MAPPED_VIEW = 0xc0000019,
            UNABLE_TO_FREE_VM = 0xc000001a,
            UNABLE_TO_DELETE_SECTION = 0xc000001b,
            ILLEGAL_INSTRUCTION = 0xc000001d,
            ALREADY_COMMITTED = 0xc0000021,
            ACCESS_DENIED = 0xc0000022,
            BUFFER_TOO_SMALL = 0xc0000023,
            OBJECT_TYPE_MISMATCH = 0xc0000024,
            NON_CONTINUABLE_EXCEPTION = 0xc0000025,
            BAD_STACK = 0xc0000028,
            NOT_LOCKED = 0xc000002a,
            NOT_COMMITTED = 0xc000002d,
            INVALID_PARAMETER_MIX = 0xc0000030,
            OBJECT_NAME_INVALID = 0xc0000033,
            OBJECT_NAME_NOT_FOUND = 0xc0000034,
            OBJECT_NAME_COLLISION = 0xc0000035,
            OBJECT_PATH_INVALID = 0xc0000039,
            OBJECT_PATH_NOT_FOUND = 0xc000003a,
            OBJECT_PATH_SYNTAX_BAD = 0xc000003b,
            DATA_OVERRUN = 0xc000003c,
            DATA_LATE = 0xc000003d,
            DATA_ERROR = 0xc000003e,
            CRC_ERROR = 0xc000003f,
            SECTION_TOO_BIG = 0xc0000040,
            PORT_CONNECTION_REFUSED = 0xc0000041,
            INVALID_PORT_HANDLE = 0xc0000042,
            SHARING_VIOLATION = 0xc0000043,
            QUOTA_EXCEEDED = 0xc0000044,
            INVALID_PAGE_PROTECTION = 0xc0000045,
            MUTANT_NOT_OWNED = 0xc0000046,
            SEMAPHORE_LIMIT_EXCEEDED = 0xc0000047,
            PORT_ALREADY_SET = 0xc0000048,
            SECTION_NOT_IMAGE = 0xc0000049,
            SUSPEND_COUNT_EXCEEDED = 0xc000004a,
            THREAD_IS_TERMINATING = 0xc000004b,
            BAD_WORKING_SET_LIMIT = 0xc000004c,
            INCOMPATIBLE_FILE_MAP = 0xc000004d,
            SECTION_PROTECTION = 0xc000004e,
            EAS_NOT_SUPPORTED = 0xc000004f,
            EA_TOO_LARGE = 0xc0000050,
            NON_EXISTENT_EA_ENTRY = 0xc0000051,
            NO_EAS_ON_FILE = 0xc0000052,
            EA_CORRUPT_ERROR = 0xc0000053,
            FILE_LOCK_CONFLICT = 0xc0000054,
            LOCK_NOT_GRANTED = 0xc0000055,
            DELETE_PENDING = 0xc0000056,
            CTL_FILE_NOT_SUPPORTED = 0xc0000057,
            UNKNOWN_REVISION = 0xc0000058,
            REVISION_MISMATCH = 0xc0000059,
            INVALID_OWNER = 0xc000005a,
            INVALID_PRIMARY_GROUP = 0xc000005b,
            NO_IMPERSONATION_TOKEN = 0xc000005c,
            CANT_DISABLE_MANDATORY = 0xc000005d,
            NO_LOGON_SERVERS = 0xc000005e,
            NO_SUCH_LOGON_SESSION = 0xc000005f,
            NO_SUCH_PRIVILEGE = 0xc0000060,
            PRIVILEGE_NOT_HELD = 0xc0000061,
            INVALID_ACCOUNT_NAME = 0xc0000062,
            USER_EXISTS = 0xc0000063,
            NO_SUCH_USER = 0xc0000064,
            GROUP_EXISTS = 0xc0000065,
            NO_SUCH_GROUP = 0xc0000066,
            MEMBER_IN_GROUP = 0xc0000067,
            MEMBER_NOT_IN_GROUP = 0xc0000068,
            LAST_ADMIN = 0xc0000069,
            WRONG_PASSWORD = 0xc000006a,
            ILL_FORMED_PASSWORD = 0xc000006b,
            PASSWORD_RESTRICTION = 0xc000006c,
            LOGON_FAILURE = 0xc000006d,
            ACCOUNT_RESTRICTION = 0xc000006e,
            INVALID_LOGON_HOURS = 0xc000006f,
            INVALID_WORKSTATION = 0xc0000070,
            PASSWORD_EXPIRED = 0xc0000071,
            ACCOUNT_DISABLED = 0xc0000072,
            NONE_MAPPED = 0xc0000073,
            TOO_MANY_LUIDS_REQUESTED = 0xc0000074,
            LUIDS_EXHAUSTED = 0xc0000075,
            INVALID_SUB_AUTHORITY = 0xc0000076,
            INVALID_ACL = 0xc0000077,
            INVALID_SID = 0xc0000078,
            INVALID_SECURITY_DESCR = 0xc0000079,
            PROCEDURE_NOT_FOUND = 0xc000007a,
            INVALID_IMAGE_FORMAT = 0xc000007b,
            NO_TOKEN = 0xc000007c,
            BAD_INHERITANCE_ACL = 0xc000007d,
            RANGE_NOT_LOCKED = 0xc000007e,
            DISK_FULL = 0xc000007f,
            SERVER_DISABLED = 0xc0000080,
            SERVER_NOT_DISABLED = 0xc0000081,
            TOO_MANY_GUIDS_REQUESTED = 0xc0000082,
            GUIDS_EXHAUSTED = 0xc0000083,
            INVALID_ID_AUTHORITY = 0xc0000084,
            AGENTS_EXHAUSTED = 0xc0000085,
            INVALID_VOLUME_LABEL = 0xc0000086,
            SECTION_NOT_EXTENDED = 0xc0000087,
            NOT_MAPPED_DATA = 0xc0000088,
            RESOURCE_DATA_NOT_FOUND = 0xc0000089,
            RESOURCE_TYPE_NOT_FOUND = 0xc000008a,
            RESOURCE_NAME_NOT_FOUND = 0xc000008b,
            ARRAY_BOUNDS_EXCEEDED = 0xc000008c,
            FLOAT_DENORMAL_OPERAND = 0xc000008d,
            FLOAT_DIVIDE_BY_ZERO = 0xc000008e,
            FLOAT_INEXACT_RESULT = 0xc000008f,
            FLOAT_INVALID_OPERATION = 0xc0000090,
            FLOAT_OVERFLOW = 0xc0000091,
            FLOAT_STACK_CHECK = 0xc0000092,
            FLOAT_UNDERFLOW = 0xc0000093,
            INTEGER_DIVIDE_BY_ZERO = 0xc0000094,
            INTEGER_OVERFLOW = 0xc0000095,
            PRIVILEGED_INSTRUCTION = 0xc0000096,
            TOO_MANY_PAGING_FILES = 0xc0000097,
            FILE_INVALID = 0xc0000098,
            INSTANCE_NOT_AVAILABLE = 0xc00000ab,
            PIPE_NOT_AVAILABLE = 0xc00000ac,
            INVALID_PIPE_STATE = 0xc00000ad,
            PIPE_BUSY = 0xc00000ae,
            ILLEGAL_FUNCTION = 0xc00000af,
            PIPE_DISCONNECTED = 0xc00000b0,
            PIPE_CLOSING = 0xc00000b1,
            PIPE_CONNECTED = 0xc00000b2,
            PIPE_LISTENING = 0xc00000b3,
            INVALID_READ_MODE = 0xc00000b4,
            IO_TIMEOUT = 0xc00000b5,
            FILE_FORCED_CLOSED = 0xc00000b6,
            PROFILING_NOT_STARTED = 0xc00000b7,
            PROFILING_NOT_STOPPED = 0xc00000b8,
            NOT_SAME_DEVICE = 0xc00000d4,
            FILE_RENAMED = 0xc00000d5,
            CANT_WAIT = 0xc00000d8,
            PIPE_EMPTY = 0xc00000d9,
            CANT_TERMINATE_SELF = 0xc00000db,
            INTERNAL_ERROR = 0xc00000e5,
            INVALID_PARAMETER1 = 0xc00000ef,
            INVALID_PARAMETER2 = 0xc00000f0,
            INVALID_PARAMETER3 = 0xc00000f1,
            INVALID_PARAMETER4 = 0xc00000f2,
            INVALID_PARAMETER5 = 0xc00000f3,
            INVALID_PARAMETER6 = 0xc00000f4,
            INVALID_PARAMETER7 = 0xc00000f5,
            INVALID_PARAMETER8 = 0xc00000f6,
            INVALID_PARAMETER9 = 0xc00000f7,
            INVALID_PARAMETER10 = 0xc00000f8,
            INVALID_PARAMETER11 = 0xc00000f9,
            INVALID_PARAMETER12 = 0xc00000fa,
            MAPPED_FILE_SIZE_ZERO = 0xc000011e,
            TOO_MANY_OPENED_FILES = 0xc000011f,
            CANCELLED = 0xc0000120,
            CANNOT_DELETE = 0xc0000121,
            INVALID_COMPUTER_NAME = 0xc0000122,
            FILE_DELETED = 0xc0000123,
            SPECIAL_ACCOUNT = 0xc0000124,
            SPECIAL_GROUP = 0xc0000125,
            SPECIAL_USER = 0xc0000126,
            MEMBERS_PRIMARY_GROUP = 0xc0000127,
            FILE_CLOSED = 0xc0000128,
            TOO_MANY_THREADS = 0xc0000129,
            THREAD_NOT_IN_PROCESS = 0xc000012a,
            TOKEN_ALREADY_IN_USE = 0xc000012b,
            PAGEFILE_QUOTA_EXCEEDED = 0xc000012c,
            COMMITMENT_LIMIT = 0xc000012d,
            INVALID_IMAGE_LE_FORMAT = 0xc000012e,
            INVALID_IMAGE_NOT_MZ = 0xc000012f,
            INVALID_IMAGE_PROTECT = 0xc0000130,
            INVALID_IMAGE_WIN16 = 0xc0000131,
            LOGON_SERVER = 0xc0000132,
            DIFFERENCE_AT_DC = 0xc0000133,
            SYNCHRONIZATION_REQUIRED = 0xc0000134,
            DLL_NOT_FOUND = 0xc0000135,
            IO_PRIVILEGE_FAILED = 0xc0000137,
            ORDINAL_NOT_FOUND = 0xc0000138,
            ENTRY_POINT_NOT_FOUND = 0xc0000139,
            CONTROL_C_EXIT = 0xc000013a,
            PORT_NOT_SET = 0xc0000353,
            DEBUGGER_INACTIVE = 0xc0000354,
            CALLBACK_BYPASS = 0xc0000503,
            PORT_CLOSED = 0xc0000700,
            MESSAGE_LOST = 0xc0000701,
            INVALID_MESSAGE = 0xc0000702,
            REQUEST_CANCELED = 0xc0000703,
            RECURSIVE_DISPATCH = 0xc0000704,
            LPC_RECEIVE_BUFFER_EXPECTED = 0xc0000705,
            LPC_INVALID_CONNECTION_USAGE = 0xc0000706,
            LPC_REQUESTS_NOT_ALLOWED = 0xc0000707,
            RESOURCE_IN_USE = 0xc0000708,
            PROCESS_IS_PROTECTED = 0xc0000712,
            VOLUME_DIRTY = 0xc0000806,
            FILE_CHECKED_OUT = 0xc0000901,
            CHECK_OUT_REQUIRED = 0xc0000902,
            BAD_FILE_TYPE = 0xc0000903,
            FILE_TOO_LARGE = 0xc0000904,
            FORMS_AUTH_REQUIRED = 0xc0000905,
            VIRUS_INFECTED = 0xc0000906,
            VIRUS_DELETED = 0xc0000907,
            TRANSACTIONAL_CONFLICT = 0xc0190001,
            INVALID_TRANSACTION = 0xc0190002,
            TRANSACTION_NOT_ACTIVE = 0xc0190003,
            TM_INITIALIZATION_FAILED = 0xc0190004,
            RM_NOT_ACTIVE = 0xc0190005,
            RM_METADATA_CORRUPT = 0xc0190006,
            TRANSACTION_NOT_JOINED = 0xc0190007,
            DIRECTORY_NOT_RM = 0xc0190008,
            COULD_NOT_RESIZE_LOG = 0xc0190009,
            TRANSACTIONS_UNSUPPORTED_REMOTE = 0xc019000a,
            LOG_RESIZE_INVALID_SIZE = 0xc019000b,
            REMOTE_FILE_VERSION_MISMATCH = 0xc019000c,
            CRM_PROTOCOL_ALREADY_EXISTS = 0xc019000f,
            TRANSACTION_PROPAGATION_FAILED = 0xc0190010,
            CRM_PROTOCOL_NOT_FOUND = 0xc0190011,
            TRANSACTION_SUPERIOR_EXISTS = 0xc0190012,
            TRANSACTION_REQUEST_NOT_VALID = 0xc0190013,
            TRANSACTION_NOT_REQUESTED = 0xc0190014,
            TRANSACTION_ALREADY_ABORTED = 0xc0190015,
            TRANSACTION_ALREADY_COMMITTED = 0xc0190016,
            TRANSACTION_INVALID_MARSHALL_BUFFER = 0xc0190017,
            CURRENT_TRANSACTION_NOT_VALID = 0xc0190018,
            LOG_GROWTH_FAILED = 0xc0190019,
            OBJECT_NO_LONGER_EXISTS = 0xc0190021,
            STREAM_MINIVERSION_NOT_FOUND = 0xc0190022,
            STREAM_MINIVERSION_NOT_VALID = 0xc0190023,
            MINIVERSION_INACCESSIBLE_FROM_SPECIFIED_TRANSACTION = 0xc0190024,
            CANT_OPEN_MINIVERSION_WITH_MODIFY_INTENT = 0xc0190025,
            CANT_CREATE_MORE_STREAM_MINIVERSIONS = 0xc0190026,
            HANDLE_NO_LONGER_VALID = 0xc0190028,
            NO_TXF_METADATA = 0xc0190029,
            LOG_CORRUPTION_DETECTED = 0xc0190030,
            CANT_RECOVER_WITH_HANDLE_OPEN = 0xc0190031,
            RM_DISCONNECTED = 0xc0190032,
            ENLISTMENT_NOT_SUPERIOR = 0xc0190033,
            RECOVERY_NOT_NEEDED = 0xc0190034,
            RM_ALREADY_STARTED = 0xc0190035,
            FILE_IDENTITY_NOT_PERSISTENT = 0xc0190036,
            CANT_BREAK_TRANSACTIONAL_DEPENDENCY = 0xc0190037,
            CANT_CROSS_RM_BOUNDARY = 0xc0190038,
            TXF_DIR_NOT_EMPTY = 0xc0190039,
            INDOUBT_TRANSACTIONS_EXIST = 0xc019003a,
            TM_VOLATILE = 0xc019003b,
            ROLLBACK_TIMER_EXPIRED = 0xc019003c,
            TXF_ATTRIBUTE_CORRUPT = 0xc019003d,
            EFS_NOT_ALLOWED_IN_TRANSACTION = 0xc019003e,
            TRANSACTIONAL_OPEN_NOT_ALLOWED = 0xc019003f,
            TRANSACTED_MAPPING_UNSUPPORTED_REMOTE = 0xc0190040,
            TXF_METADATA_ALREADY_PRESENT = 0xc0190041,
            TRANSACTION_SCOPE_CALLBACKS_NOT_SET = 0xc0190042,
            TRANSACTION_REQUIRED_PROMOTION = 0xc0190043,
            CANNOT_EXECUTE_FILE_IN_TRANSACTION = 0xc0190044,
            TRANSACTIONS_NOT_FROZEN = 0xc0190045,

            MAXIMUM_NT_STATUS = 0xffffffff

        }

        internal enum ProcessInfoClass {

            PROCESS_BASIC_INFORMATION = 0x00,
            PROCESS_QUOTA_LIMITS = 0x01,
            PROCESS_IO_COUNTERS = 0x02,
            PROCESS_VM_COUNTERS = 0x03,
            PROCESS_TIMES = 0x04,
            PROCESS_BASE_PRIORITY = 0x05,
            PROCESS_RAISE_PRIORITY = 0x06,
            PROCESS_DEBUG_PORT = 0x07,
            PROCESS_EXCEPTION_PORT = 0x08,
            PROCESS_ACCESS_TOKEN = 0x09,
            PROCESS_LDT_INFORMATION = 0x0A,
            PROCESS_LDT_SIZE = 0x0B,
            PROCESS_DEFAULT_HARD_ERROR_MODE = 0x0C,
            PROCESS_IO_PORT_HANDLERS = 0x0D,
            PROCESS_POOLED_USAGE_AND_LIMITS = 0x0E,
            PROCESS_WORKING_SET_WATCH = 0x0F,
            PROCESS_USER_MODE_IOPL = 0x10,
            PROCESS_ENABLE_ALIGNMENT_FAULT_FIXUP = 0x11,
            PROCESS_PRIORITY_CLASS = 0x12,
            PROCESS_WX86_INFORMATION = 0x13,
            PROCESS_HANDLE_COUNT = 0x14,
            PROCESS_AFFINITY_MASK = 0x15,
            PROCESS_PRIORITY_BOOST = 0x16,
            PROCESS_DEVICE_MAP = 0x17,
            PROCESS_SESSION_INFORMATION = 0x18,
            PROCESS_FOREGROUND_INFORMATION = 0x19,
            PROCESS_WOW64_INFORMATION = 0x1A,
            PROCESS_IMAGE_FILE_NAME = 0x1B,
            PROCESS_LUID_DEVICE_MAPS_ENABLED = 0x1C,
            PROCESS_BREAK_ON_TERMINATION = 0x1D,
            PROCESS_DEBUG_OBJECT_HANDLE = 0x1E,
            PROCESS_DEBUG_FLAGS = 0x1F,
            PROCESS_HANDLE_TRACING = 0x20,
            PROCESS_IO_PRIORITY = 0x21,
            PROCESS_EXECUTE_FLAGS = 0x22,
            PROCESS_RESOURCE_MANAGEMENT = 0x23,
            PROCESS_COOKIE = 0x24,
            PROCESS_IMAGE_INFORMATION = 0x25,
            PROCESS_CYCLE_TIME = 0x26,
            PROCESS_PAGE_PRIORITY = 0x27,
            PROCESS_INSTRUMENTATION_CALLBACK = 0x28,
            PROCESS_THREAD_STACK_ALLOCATION = 0x29,
            PROCESS_WORKING_SET_WATCH_EX = 0x2A,
            PROCESS_IMAGE_FILE_NAME_WIN32 = 0x2B,
            PROCESS_IMAGE_FILE_MAPPING = 0x2C,
            PROCESS_AFFINITY_UPDATE_MODE = 0x2D,
            PROCESS_MEMORY_ALLOCATION_MODE = 0x2E,
            PROCESS_GROUP_INFORMATION = 0x2F,
            PROCESS_TOKEN_VIRTUALIZATION_ENABLED = 0x30,
            PROCESS_CONSOLE_HOST_PROCESS = 0x31,
            PROCESS_WINDOW_INFORMATION = 0x32,
            PROCESS_HANDLE_INFORMATION = 0x33,
            PROCESS_MITIGATION_POLICY = 0x34,
            PROCESS_DYNAMIC_FUNCTION_TABLE_INFORMATION = 0x35,
            PROCESS_HANDLE_CHECKING_MODE = 0x36,
            PROCESS_KEEP_ALIVE_COUNT = 0x37,
            PROCESS_REVOKE_FILE_HANDLES = 0x38,
            PROCESS_WORKING_SET_CONTROL = 0x39,
            PROCESS_HANDLE_TABLE = 0x3A,
            PROCESS_CHECK_STACK_EXTENTS_MODE = 0x3B,
            PROCESS_COMMAND_LINE_INFORMATION = 0x3C,
            PROCESS_PROTECTION_INFORMATION = 0x3D,
            PROCESS_MEMORY_EXHAUSTION = 0x3E,
            PROCESS_FAULT_INFORMATION = 0x3F,
            PROCESS_TELEMETRY_ID_INFORMATION = 0x40,
            PROCESS_COMMIT_RELEASE_INFORMATION = 0x41,
            PROCESS_DEFAULT_CPU_SETS_INFORMATION = 0x42,
            PROCESS_ALLOWED_CPU_SETS_INFORMATION = 0x43,
            PROCESS_SUBSYSTEM_PROCESS = 0x44,
            PROCESS_JOB_MEMORY_INFORMATION = 0x45,
            PROCESS_IN_PRIVATE = 0x46,
            PROCESS_RAISE_UM_EXCEPTION_ON_INVALID_HANDLE_CLOSE = 0x47,
            PROCESS_IUM_CHALLENGE_RESPONSE = 0x48,
            PROCESS_CHILD_PROCESS_INFORMATION = 0x49,
            PROCESS_HIGH_GRAPHICS_PRIORITY_INFORMATION = 0x4A,
            PROCESS_SUBSYSTEM_INFORMATION = 0x4B,
            PROCESS_ENERGY_VALUES = 0x4C,
            PROCESS_ACTIVITY_THROTTLE_STATE = 0x4D,
            PROCESS_ACTIVITY_THROTTLE_POLICY = 0x4E,
            PROCESS_WIN32_K_SYSCALL_FILTER_INFORMATION = 0x4F,
            PROCESS_DISABLE_SYSTEM_ALLOWED_CPU_SETS = 0x50,
            PROCESS_WAKE_INFORMATION = 0x51,
            PROCESS_ENERGY_TRACKING_STATE = 0x52,
            PROCESS_MANAGE_WRITES_TO_EXECUTABLE_MEMORY = 0x53,
            PROCESS_CAPTURE_TRUSTLET_LIVE_DUMP = 0x54,
            PROCESS_TELEMETRY_COVERAGE = 0x55,
            PROCESS_ENCLAVE_INFORMATION = 0x56,
            PROCESS_ENABLE_READ_WRITE_VM_LOGGING = 0x57,
            PROCESS_UPTIME_INFORMATION = 0x58,
            PROCESS_IMAGE_SECTION = 0x59,
            PROCESS_DEBUG_AUTH_INFORMATION = 0x5A,
            PROCESS_SYSTEM_RESOURCE_MANAGEMENT = 0x5B,
            PROCESS_SEQUENCE_NUMBER = 0x5C,
            PROCESS_LOADER_DETOUR = 0x5D,
            PROCESS_SECURITY_DOMAIN_INFORMATION = 0x5E,
            PROCESS_COMBINE_SECURITY_DOMAINS_INFORMATION = 0x5F,
            PROCESS_ENABLE_LOGGING = 0x60,
            PROCESS_LEAP_SECOND_INFORMATION = 0x61,
            PROCESS_FIBER_SHADOW_STACK_ALLOCATION = 0x62,
            PROCESS_FREE_FIBER_SHADOW_STACK_ALLOCATION = 0x63,
            MAX_PROCESS_INFO_CLASS = 0x64

        };

        #endregion
    }

}