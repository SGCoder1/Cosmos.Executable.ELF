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

            switch (identification.align)
            {
                case ElfDataAlignment.MostSignificant: m_switchEndianess = BitConverter.IsLittleEndian; break;
                case ElfDataAlignment.LeastSignificant: m_switchEndianess = !BitConverter.IsLittleEndian; break;
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
                ExceptionHelpers.ThrowUnloadbleArchitecture((ElfArchitecture)header.machine);
                throw null!;
            }

            // TODO: Implement the 32-bit side for the entire header.
        }

        private unsafe void LoadAs64Bit()
        {
            m_reader.BaseStream.Position = 0;
            Elf64Header header = m_reader.Read<Elf64Header>();
            if (m_switchEndianess)
            {
                header.ReverseEndianess();
            }

            if (!IsCompatible(GetCurrentArchitecture(), (ElfArchitecture)header.machine))
            {
                ExceptionHelpers.ThrowUnloadbleArchitecture((ElfArchitecture)header.machine);
                throw null!;
            }

            int programHeaderLength = header.programHeaderCount;
            if (programHeaderLength == 0 && header.entryPointRVA != 0)
            {
                ExceptionHelpers.ThrowBadImage(m_path);
                throw null!;
            }
            
            m_executable = (ImageType)header.elfType == ImageType.Executable;

            // TODO: Implement the 64-bit side for the entire header.

            Elf64ProgramHeader[] progs = new Elf64ProgramHeader[programHeaderLength];

            m_reader.BaseStream.Position = (long)header.programHeaderOffset;
            for (int i = 0; i < programHeaderLength; i++)
            {
                // Buffer first before parsing. We're going to be seeking again.
                progs[i] = m_reader.Read<Elf64ProgramHeader>();
            }

            for (int i = 0; i < programHeaderLength; i++)
            {
                Elf64ProgramHeader progHeader = progs[i];
                m_reader.BaseStream.Position = (long)progHeader.fileOffset;

                ProgramHeaderFlags flags = (ProgramHeaderFlags)progHeader.flags;
                ProgramHeaderType type = (ProgramHeaderType)progHeader.type;

                if (flags == ProgramHeaderFlags.Invalid)
                {
                    ExceptionHelpers.ThrowBadImage(m_path);
                    throw null!;
                }

                if (type == ProgramHeaderType.Loadable && (flags & ProgramHeaderFlags.Executable) != 0)
                {
                    // Found the .text section :)
                    byte[] codeBytesToCopy = m_reader.ReadBytes((int)progHeader.fileSize);
                    byte* codeBlock = (byte*)NativeMemory.Alloc((nuint)progHeader.memorySize);

                    int index = codeBytesToCopy.IndexOfAnyExcept((byte)0);
                    if (index < 0)
                    {
                        throw new InvalidOperationException("Bytes for the .text section should not be all zeros.");
                    }

                    fixed (byte* pCopyBytes = codeBytesToCopy)
                    {
                        NativeMemory.Copy(pCopyBytes, codeBlock, (nuint)codeBytesToCopy.Length);
                    }

                    m_code.block = codeBlock;
                    m_code.rva = progHeader.virtualAddr;
                    m_code.length = progHeader.memorySize;
                    m_executable = true;
                }
            }

            m_entryPointOffset = header.entryPointRVA;
        }
    }
}
