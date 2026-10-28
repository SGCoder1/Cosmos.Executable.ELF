using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    public sealed partial class ElfReader : IDisposable
    {
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

        public unsafe void Execute()
        {
            delegate* unmanaged[Cdecl]<int, byte**, int> ep = (delegate* unmanaged[Cdecl]<int, byte**, int>)(m_codeBlock + (nint)m_entryPointOffset);
            ep(0, null);
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
