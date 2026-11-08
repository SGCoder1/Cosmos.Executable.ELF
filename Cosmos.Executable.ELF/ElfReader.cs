using Cosmos.Kernel.System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    public sealed unsafe partial class ElfReader : IDisposable
    {
        private bool m_executable;
        private bool m_switchEndianess;
        private readonly BinaryReader m_reader;
        private readonly string? m_path;
        private ulong m_entryPointOffset;
        private static ElfReader? sm_exec;

        // Stored argc and argv
        private char** m_lastArgv;
        private int m_lastArgc;

        // Use pointers because T[] might pressure the GC.
        // TODO: Use NativeMemory.Alloc()
        private LoadedSection m_code;
        private LoadedSection m_data;
        private LoadedSection m_bss;

        public static ElfReader? ExecutingReader => sm_exec;

        public int ArgCount => m_lastArgc;

        public char** ArgValues
        {
            get
            {
                if (m_lastArgv == null)
                {
                    throw new InvalidOperationException("No loaded arg value.");
                }

                return m_lastArgv;
            }
        }

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

        public int Execute(string[] args)
        {
            if (!m_executable)
            {
                ExceptionHelpers.ThrowNonExecutableImage();
                throw null!;
            }

            sm_exec = this;
            if (m_lastArgv != null)
            {
                NativeMemory.Free(m_lastArgv);
            }

            m_lastArgc = args.Length;
            m_lastArgv = (char**)NativeMemory.Alloc((nuint)(args.Length + 1), (nuint)sizeof(nint));

            Log.WriteString($"[Cosmos.Executable.Elf] Executing image...\n");
            InvokeEntryPoint();
            Log.WriteString($"[Cosmos.Executable.Elf] Executable returned {0}\n");

            NativeMemory.Free(m_lastArgv);
            return 0;
        }

        public void Dispose()
        {
            if (m_code.block != null)
            {
                NativeMemory.Free(m_code.block);
            }

            if (m_data.block != null)
            {
                NativeMemory.Free(m_data.block);
            }

            if (m_bss.block != null)
            {
                NativeMemory.Free(m_bss.block);
            }

            m_reader.Dispose();
        }
    }
}

internal unsafe struct LoadedSection
{
    internal byte* block;
    internal ulong length;
    internal ulong rva;
}