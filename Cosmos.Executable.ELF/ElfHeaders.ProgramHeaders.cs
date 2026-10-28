namespace Cosmos.Executable.ELF
{
    internal enum ProgramHeaderType
    {
        Unused,
        Loadable,
        Dyanmic,
        Interpreter,
        Notes, // Auxiliary info
        SHLIB,
        ProgramHeader, // for the table itself (interesting)
        ThreadLocalStorage,
        NumOfDefTypes, // PT_NUM (unsure if this is used)


        //
        // The following enum values are GNU-specific
        //

        GnuExceptionFrameHeader = 0x6474e550,
        GnuStackExec = 0x6474e551, // PT_GNU_STACK
        GnuReadonlyReloc = 0x6474e552 // PT_GNU_RELRO (readonly after relocation)
    }

    [Flags]
    internal enum ProgramHeaderFlags
    {
        Invalid,
        Executable,
        Writable = 2,
        Readable = 4,
    }
}
