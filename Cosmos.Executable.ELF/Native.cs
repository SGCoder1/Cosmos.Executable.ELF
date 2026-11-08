using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Cosmos.Executable.ELF
{
    internal static class Native
    {
        [UnmanagedCallersOnly(EntryPoint = "__env_argc", CallConvs = [typeof(CallConvCdecl)])]
        private static int GetArgCount()
        {
            if (ElfReader.ExecutingReader is null)
            {
                throw new InvalidOperationException("Executing image should not be null.");
            }

            return ElfReader.ExecutingReader.ArgCount;
        }

        [UnmanagedCallersOnly(EntryPoint = "__env_argv", CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe char** GetArgsRaw()
        {
            if (ElfReader.ExecutingReader is null)
            {
                throw new InvalidOperationException("Executing image should not be null.");
            }

            return ElfReader.ExecutingReader.ArgValues;
        }
    }
}
