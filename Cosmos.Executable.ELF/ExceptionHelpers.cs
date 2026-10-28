using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    internal static class ExceptionHelpers
    {
        [DoesNotReturn]
        internal static void ThrowBadImage(string? path) => throw new BadImageFormatException("ELF image was invalid.", path);

        [DoesNotReturn]
        internal static void ThrowNonExecutableImage() => throw new BadImageFormatException("ELF image is not executable.");

        [DoesNotReturn]
        internal static void ThrowUnloadbleArchitecture(ElfArchitecture arch) => throw new NotSupportedException($"Cannot run under {arch}.");

        [DoesNotReturn]
        internal static void ThrowUnsupportedArchitecture(Architecture arch) => throw new NotSupportedException($"ELF loader does not support architecutre '{arch}'.");

        [DoesNotReturn]
        internal static void ThrowUnsupportedValues() => throw new NotSupportedException($"ELF loader does not support custom architectures or the GNU ABI.");
    }
}
