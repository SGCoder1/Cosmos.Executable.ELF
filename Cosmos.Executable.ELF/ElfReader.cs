namespace Cosmos.Executable.ELF
{
    public sealed partial class ElfReader : IDisposable
    {
        private readonly BinaryReader m_reader;
        private readonly string? m_path;

        internal ElfReader(Stream stream)
        {
            m_reader = new(stream);
            if (stream is FileStream fs)
            {
                m_path = fs.Name;
            }

            Initialize();
        }

        public void Dispose()
        {

        }
    }
}
