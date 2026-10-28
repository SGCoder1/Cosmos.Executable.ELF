//
// Partially ported from https://github.com/torvalds/linux/blob/master/include/uapi/linux/elf.h
// Honorable Mention: https://gist.github.com/x0nu11byt3/bcb35c3de461e5fb66173071a2379779
// 
// Comments from the source are kept
//

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cosmos.Executable.ELF
{
    /* 32-bit ELF base types. */
    using Elf32_Addr = uint;
    using Elf32_Half = ushort;
    using Elf32_Off = uint;
    using Elf32_Word = uint;

    /* 64-bit ELF base types. */
    using Elf64_Addr = ulong;
    using Elf64_Half = ushort;
    using Elf64_Off = ulong;
    using Elf64_Word = uint;
    using Elf64_XWord = ulong;

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf32Header
    {
        public InlineArray16<byte> identification;              // e_ident
        public Elf32_Half elfType;                              // e_type
        public Elf32_Half machine;
        public Elf32_Word version;
        public Elf32_Addr entryPointRVA;                        // e_entry
        public Elf32_Off programHeaderOffset;                   // e_phoff
        public Elf32_Off sectionHeaderOffset;                   // e_shoff
        public Elf32_Word flags;
        public Elf32_Half headerSize;                           // e_ehsize
        public Elf32_Half programHeaderEntrySize;               // e_phentsize
        public Elf32_Half programHeaderCount;                   // e_phnum
        public Elf32_Half sectionHeaderEntrySize;               // e_shentsize
        public Elf32_Half sectionHeaderCount;                   // e_shnum
        public Elf32_Half sectionHeaderStringIndex;             // e_shstrndx
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf64Header
    {
        public InlineArray16<byte> identification;              // e_ident
        public Elf64_Half elfType;                              // e_type
        public Elf64_Half machine;
        public Elf64_Word version;
        public Elf64_Addr entryPointRVA;                        // e_entry
        public Elf64_Off programHeaderOffset;                   // e_phoff
        public Elf64_Off sectionHeaderOffset;                   // e_shoff
        public Elf64_Word flags;
        public Elf64_Half headerSize;                           // e_ehsize
        public Elf64_Half programHeaderEntrySize;               // e_phentsize
        public Elf64_Half programHeaderCount;                   // e_phnum
        public Elf64_Half sectionHeaderEntrySize;               // e_shentsize
        public Elf64_Half sectionHeaderCount;                   // e_shnum
        public Elf64_Half sectionHeaderStringIndex;             // e_shstrndx
    }

    //  
    // ELF Program Headers
    //   The actual loaded headers for each segment (.text, .data, .bss...)
    // 

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf32ProgramHeader
    {
        public Elf32_Word type;            // p_type
        public Elf32_Off fileOffset;       // p_offset
        public Elf32_Addr virtualAddr;     // p_vaddr
        public Elf32_Addr physicalAddr;    // p_paddr
        public Elf32_Word fileSize;        // p_filesz
        public Elf32_Word memorySize;      // p_memsz
        public Elf32_Word flags;           // p_flags
        public Elf32_Word alignment;       // p_align
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf64ProgramHeader
    {
        public Elf64_Word type;            // p_type
        public Elf64_Word flags;           // p_flags
        public Elf64_Off fileOffset;       // p_offset 
        public Elf64_Addr virtualAddr;     // p_vaddr 
        public Elf64_Addr physicalAddr;    // p_paddr 
        public Elf64_XWord fileSize;       // p_filesz 
        public Elf64_XWord memorySize;     // p_memsz 
        public Elf64_XWord alignment;      // p_align, file & memory 
    }


    //
    // ELF Section Headers
    //   Metadata for debuggers & not loaded into memory
    //

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf32SectionHeader
    {
        public Elf32_Word nameIndex;       // sh_name, string table 
        public Elf32_Word type;            // sh_type 
        public Elf32_Word flags;           // sh_flags 
        public Elf32_Addr execRVA;         // sh_addr 
        public Elf32_Off fileOffset;       // sh_offset 
        public Elf32_Word sizeInBytes;     // sh_size 
        public Elf32_Word link;            // sh_link 
        public Elf32_Word auxInfo;         // sh_info 
        public Elf32_Word addrAlignment;   // sh_addralign 
        public Elf32_Word tableEntrySize;  // sh_entsize 
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf64SectionHeader
    {
        public Elf64_Word nameIndex;       // sh_name, string table
        public Elf64_Word type;            // sh_type
        public Elf64_XWord flags;          // sh_flags
        public Elf64_Addr execRVA;         // sh_addr 
        public Elf64_Off fileOffset;       // sh_offset 
        public Elf64_XWord sizeInBytes;    // sh_size 
        public Elf64_Word link;            // sh_link 
        public Elf64_Word auxInfo;         // sh_info 
        public Elf64_XWord addrAlignment;  // sh_addralign 
        public Elf64_XWord tableEntrySize; // sh_entsize 
    }
}
