using Cosmos.Kernel.System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    public sealed partial class ElfReader : IDisposable
    {
        private bool m_executable;
        private bool m_switchEndianess;
        private readonly BinaryReader m_reader;
        private readonly string? m_path;
        private ulong m_entryPointOffset;
        
        // Use pointers because T[] might pressure the GC.
        // Marshal.AllocHGlobal() avoids GC allocation (probably)
        private nint m_codeBlock;
        private nint m_dataBlock;
        private nint m_bssBlock;

        public ElfReader(Stream stream)
        {
            m_reader = new(stream);
            if (stream is FileStream fs)
            {
                m_path = fs.Name;
            }
            
            Initialize();
        }

        public int Execute() => Execute([]);
        
        public unsafe int Execute(string[] args)
        {
            if (!m_executable)
            {
                ExceptionHelpers.ThrowNonExecutableImage();
                throw null!;
            }

            int passingArgc = args.Length;
            byte** passingArgv = (byte**)NativeMemory.Alloc((nuint)(passingArgc + 1), (nuint)sizeof(byte*));

            try
            {
                for (int i = 0; i < passingArgc; i++)
                {
                    passingArgv[i] = (byte*)Marshal.StringToHGlobalAnsi(args[i]);
                }

                // Zero out this index because it's not calloc'd
                passingArgv[passingArgc] = null;

                Log.WriteString($"[Cosmos.Executable.Elf] Executing image...\n");

                delegate* unmanaged[Cdecl]<int, byte**, int> ep = (delegate* unmanaged[Cdecl]<int, byte**, int>)(m_codeBlock + (nint)m_entryPointOffset);
                int result = ep(passingArgc, passingArgv);

                Log.WriteString($"[Cosmos.Executable.Elf] Executable returned {result}\n");
                return result;
            }
            finally
            {
                for (int i = 0; i < passingArgc; i++)
                {
                    Marshal.FreeHGlobal((nint)passingArgv[i]);
                }

                NativeMemory.Free(passingArgv);
            }
        }

        public void Dispose()
        {
            if (m_codeBlock != nint.Zero)
            {
                Marshal.FreeHGlobal(m_codeBlock);
            }
            
            if (m_dataBlock != nint.Zero)
            {
                Marshal.FreeHGlobal(m_dataBlock);
            }
            
            if (m_bssBlock != nint.Zero)
            {
                Marshal.FreeHGlobal(m_bssBlock);
            }

            m_reader.Dispose();
        }
    }
}
