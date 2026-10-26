using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    partial class ElfReader
    {
        private void Initialize()
        {
            ReadOnlySpan<byte> expectedMagic = "\u007fELF"u8;

            ElfIdentification identification = m_reader.Read<ElfIdentification>();
            if (identification.magic != MemoryMarshal.AsRef<uint>(expectedMagic) ||
                identification.arch == 0 ||
                identification.align == 0 ||
                identification.version is 0 or ElfVersion.Unknown ||
                identification.padding != 0)
            {
                ExceptionHelpers.ThrowBadImage(m_path);
                throw null!;  // somehow, this hints the JIT that we are not returning
            }

            if (identification.arch == ElfArchitectureClass.ModeCustomBit ||
                identification.abi == ElfAbi.GnuLinux)
            {
                ExceptionHelpers.ThrowUnsupportedValues();
                throw null!;
            }

            if (identification.arch == ElfArchitectureClass.Mode32Bit)
            {
                LoadAs32Bit();
                return;
            }

            LoadAs64Bit();
            return;
        }

        private void LoadAs32Bit()
        {
            m_reader.BaseStream.Position = 0;
            Elf32Header header = m_reader.Read<Elf32Header>();

            if (!IsCompatible(GetCurrentArchitecture(), (ElfArchitecture)header.machine))
            {
                ExceptionHelpers.ThrowUnsupportedArchitecture(RuntimeInformation.OSArchitecture, true);
                throw null!;
            }

            // TODO: Implement the 32-bit side for the entire header.
        }

        private void LoadAs64Bit()
        {
            m_reader.BaseStream.Position = 0;
            Elf64Header header = m_reader.Read<Elf64Header>();

            if (!IsCompatible(GetCurrentArchitecture(), (ElfArchitecture)header.machine))
            {
                ExceptionHelpers.ThrowUnsupportedArchitecture(RuntimeInformation.OSArchitecture, true);
                throw null!;
            }

            // TODO: Implement the 64-bit side for the entire header.
            m_reader.BaseStream.Position = header.programHeaderOffset;
            for (int i = 0; i < header.programHeaderCount; i++)
            {
                Elf32ProgramHeader progHeader = m_reader.Read<Elf32ProgramHeader>();
                
            }
        }

        private static ElfArchitecture GetCurrentArchitecture()
        {
            Architecture arch = RuntimeInformation.OSArchitecture;
            switch (arch)
            {
                case Architecture.X86: return ElfArchitecture.x86;
                case Architecture.X64: return ElfArchitecture.x64;
                case Architecture.Arm: return ElfArchitecture.Arm32;
                case Architecture.Arm64: return ElfArchitecture.Arm64;
                default: ExceptionHelpers.ThrowUnsupportedArchitecture(arch, false); throw null!;
            }
        }

        private static bool IsCompatible(ElfArchitecture current, ElfArchitecture archToRun)
        {
            if (current == ElfArchitecture.x86_64)
            {
                return archToRun is ElfArchitecture.x86 or ElfArchitecture.x64 or ElfArchitecture.x86_64;
            }

            return current == archToRun;
        }
    }
}
