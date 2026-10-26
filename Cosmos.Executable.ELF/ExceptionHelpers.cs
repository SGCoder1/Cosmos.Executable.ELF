using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    internal static class ExceptionHelpers
    {
        [DoesNotReturn]
        internal static void ThrowBadImage(string? path) => throw new BadImageFormatException("ELF image was invalid.", path);

        [DoesNotReturn]
        internal static void ThrowUnsupportedArchitecture(Architecture arch, bool becauseUnloadable)
        {
            if (becauseUnloadable)
            {
                throw new NotSupportedException($"Cannot run under the {arch}.");
            }

            throw new NotSupportedException($"ELF loader does not support architecutre '{arch}'.");
        }

        [DoesNotReturn]
        internal static void ThrowUnsupportedValues() => throw new NotSupportedException($"ELF loader does not support custom architectures or the GNU ABI.");
    }
}
