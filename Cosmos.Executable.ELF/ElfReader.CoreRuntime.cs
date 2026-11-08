using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    partial class ElfReader
    {
        [LibraryImport("*", EntryPoint = "__cosmos_exec_entry")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        private unsafe static partial void InvokeEntryPointInternal(delegate* unmanaged[Cdecl]<void> fnptr);

        private unsafe void InvokeEntryPoint()
        {
            ulong calculatedOffset = m_entryPointOffset - m_code.rva;
            delegate* unmanaged[Cdecl]<void> fn = (delegate* unmanaged[Cdecl]<void>)(m_code.block + calculatedOffset);
            InvokeEntryPointInternal(fn);
        }
    }
}
