using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    // Interesting how the first 16 bytes are an entire structure

    [StructLayout(LayoutKind.Sequential)]
    internal struct ElfIdentification
    {
        internal uint magic; // EI_MAG
        internal ElfArchitectureClass arch; // EI_CLASS
        internal ElfDataAlignment align;
        internal ElfVersion version;
        internal ElfAbi abi;
        internal ulong padding; // This MUST be 0
    }

    internal enum ElfArchitectureClass : byte
    {
        None,
        Mode32Bit,
        Mode64Bit,
        ModeCustomBit,  // ELFCLASSNUM (unsure if that's used)
    }

    internal enum ElfDataAlignment : byte
    {
        None,
        LeastSignificant,
        MostSignificant,
    }

    internal enum ElfVersion : byte
    {
        None,
        Current,
        Unknown,   // EV_NUM
    }

    internal enum ElfAbi : byte
    {
        SystemV,  // popularly seen as ELFOSABI_NONE, but has a value
        GnuLinux, // GNU extensions, keep if supporting it
    }

    internal enum ImageType : ushort
    {
        None,
        RelocatableObject,
        Executable,
        DynamicLibrary,
        CoreDump,
    }

    internal enum ElfArchitecture
    {
        Invalid,
        x86 = 2,
        MIPS = 8,
        Arm32 = 40,
        x64 = 50,
        x86_64 = 62,
        Arm64 = 183,
    }
}
