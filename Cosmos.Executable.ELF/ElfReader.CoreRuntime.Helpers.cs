using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    partial class ElfReader
    {
        private static ElfArchitecture GetCurrentArchitecture()
        {
            Architecture arch = RuntimeInformation.OSArchitecture;
            switch (arch)
            {
                case Architecture.X86: return ElfArchitecture.x86;
                case Architecture.X64: return ElfArchitecture.x64;
                case Architecture.Arm: return ElfArchitecture.Arm32;
                case Architecture.Arm64: return ElfArchitecture.Arm64;
                default: ExceptionHelpers.ThrowUnsupportedArchitecture(arch); throw null!;
            }
        }

        private static bool IsCompatible(ElfArchitecture current, ElfArchitecture archToRun)
        {
            if (current == ElfArchitecture.x86_64)
            {
                return archToRun is ElfArchitecture.x86 or ElfArchitecture.x64 or ElfArchitecture.x86_64;
            }

            if (current is ElfArchitecture.x64 or ElfArchitecture.x86)
            {
                return archToRun == ElfArchitecture.x86_64 || current == archToRun;
            }

            return current == archToRun;
        }
    }
}
