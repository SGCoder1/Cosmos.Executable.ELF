namespace Cosmos.Executable.ELF
{
    internal enum SectionHeaderType : byte
    {
        Unused,
        ProgramData,
        SymbolTable,
        StringTable,
        RelocationWithAddends,
        SymbolHashTable,
        DynamicLinkingInfo,
        Notes, // ???
        UninitializedData,
        Relocation, // Without addend variation
        SHLIB,      // Unsure how to word this
        DynamicSymbolTable, // Used by the dynamic linker
    }

    [Flags]
    internal enum SectionHeaderFlags : byte
    {
        Invalid,
        Writable,
        ExecAlloc,  // SHF_ALLOC
        Executable = 4,
    }
}
