using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    internal static class StreamHelpers
    {
        internal const int StackBufferThresholdBytes = 256;

        internal static T Read<T>(this BinaryReader reader) where T : unmanaged
        {
            int length = Unsafe.SizeOf<T>();
            Span<byte> buffer = length < StackBufferThresholdBytes ? stackalloc byte[length] : new byte[length];

            reader.Read(buffer);
            return MemoryMarshal.Read<T>(buffer);
        }
    }
}
