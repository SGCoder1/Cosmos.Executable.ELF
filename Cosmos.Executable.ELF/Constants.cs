/*
 * Every macro defined in the following headers will be defined here:
 *  [elf.h]: https://github.com/torvalds/linux/blob/master/include/uapi/linux/elf.h
 * 
 * 
 */
namespace Cosmos.Executable.ELF
{
    internal static class Constants
    {
        // elf.h

        /* These constants define the permissions on sections in the program header, p_flags. */
        internal const byte ProgramFlagsRead = 0x4;
        internal const byte ProgramFlagsWrite = 0x2;
        internal const byte ProgramFlagsExecute = 0x1;
    }
}
