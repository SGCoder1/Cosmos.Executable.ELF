using System.Buffers.Binary;
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

        internal static Elf32Header ReverseEndianess(this Elf32Header value)
        {
            Elf32Header result = value;
            result.elfType = BinaryPrimitives.ReverseEndianness(value.elfType);
            result.machine = BinaryPrimitives.ReverseEndianness(value.machine);
            result.version = BinaryPrimitives.ReverseEndianness(value.version);
            result.entryPointRVA = BinaryPrimitives.ReverseEndianness(value.entryPointRVA);
            result.programHeaderOffset = BinaryPrimitives.ReverseEndianness(value.programHeaderOffset);
            result.sectionHeaderOffset = BinaryPrimitives.ReverseEndianness(value.sectionHeaderOffset);
            result.flags = BinaryPrimitives.ReverseEndianness(value.flags);
            result.headerSize = BinaryPrimitives.ReverseEndianness(value.headerSize);
            result.programHeaderEntrySize = BinaryPrimitives.ReverseEndianness(value.programHeaderEntrySize);
            result.programHeaderCount = BinaryPrimitives.ReverseEndianness(value.programHeaderCount);
            result.sectionHeaderEntrySize = BinaryPrimitives.ReverseEndianness(value.sectionHeaderEntrySize);
            result.sectionHeaderCount = BinaryPrimitives.ReverseEndianness(value.sectionHeaderCount);
            result.sectionHeaderStringIndex = BinaryPrimitives.ReverseEndianness(value.sectionHeaderStringIndex);
            return result;
        }
        
        internal static Elf64Header ReverseEndianess(this Elf64Header value)
        {
            Elf64Header result = value;
            result.elfType = BinaryPrimitives.ReverseEndianness(value.elfType);
            result.machine = BinaryPrimitives.ReverseEndianness(value.machine);
            result.version = BinaryPrimitives.ReverseEndianness(value.version);
            result.entryPointRVA = BinaryPrimitives.ReverseEndianness(value.entryPointRVA);
            result.programHeaderOffset = BinaryPrimitives.ReverseEndianness(value.programHeaderOffset);
            result.sectionHeaderOffset = BinaryPrimitives.ReverseEndianness(value.sectionHeaderOffset);
            result.flags = BinaryPrimitives.ReverseEndianness(value.flags);
            result.headerSize = BinaryPrimitives.ReverseEndianness(value.headerSize);
            result.programHeaderEntrySize = BinaryPrimitives.ReverseEndianness(value.programHeaderEntrySize);
            result.programHeaderCount = BinaryPrimitives.ReverseEndianness(value.programHeaderCount);
            result.sectionHeaderEntrySize = BinaryPrimitives.ReverseEndianness(value.sectionHeaderEntrySize);
            result.sectionHeaderCount = BinaryPrimitives.ReverseEndianness(value.sectionHeaderCount);
            result.sectionHeaderStringIndex = BinaryPrimitives.ReverseEndianness(value.sectionHeaderStringIndex);
            return result;
        }
    }
}
