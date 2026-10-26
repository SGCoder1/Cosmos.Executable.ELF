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
    using Elf64_Off = uint;
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
        public Elf32_Off sectionHeaderOffser;                   // e_shoff
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
        public Elf64_Half headersSize;                          // e_ehsize
        public Elf64_Half programHeaderEntrySize;               // e_phentsize
        public Elf64_Half programHeaderCount;                   // e_phnum
        public Elf64_Half sectionHeaderEntrySize;               // e_shentsize
        public Elf64_Half sectionHeaderCount;                   // e_shnum
        public Elf64_Half sectionHeaderStringIndex;             // e_shstrndx
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Elf32ProgramHeader
    {
        public Elf32_Word p_type;
        public Elf32_Off p_offset;
        public Elf32_Addr p_vaddr;
        public Elf32_Addr p_paddr;
        public Elf32_Word p_filesz;
        public Elf32_Word p_memsz;
        public Elf32_Word p_flags;
        public Elf32_Word p_align;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Elf64ProgramHeader
    {
        public Elf64_Word p_type;
        public Elf64_Word p_flags;
        public Elf64_Off p_offset;         /* Segment file offset */
        public Elf64_Addr p_vaddr;         /* Segment virtual address */
        public Elf64_Addr p_paddr;         /* Segment physical address */
        public Elf64_XWord p_filesz;       /* Segment size in file */
        public Elf64_XWord p_memsz;        /* Segment size in memory */
        public Elf64_XWord p_align;        /* Segment alignment, file & memory */
    }

    public struct Elf32SectionHeader
    {
        public Elf32_Word sh_name;         /* Section name (string tbl index) */
        public Elf32_Word sh_type;         /* Section type */
        public Elf32_Word sh_flags;        /* Section flags */
        public Elf32_Addr sh_addr;         /* Section virtual addr at execution */
        public Elf32_Off sh_offset;        /* Section file offset */
        public Elf32_Word sh_size;         /* Section size in bytes */
        public Elf32_Word sh_link;         /* Link to another section */
        public Elf32_Word sh_info;         /* Additional section information */
        public Elf32_Word sh_addralign;    /* Section alignment */
        public Elf32_Word sh_entsize;      /* Entry size if section holds table */
    }

    public struct Elf64SectionHeader
    {
        public Elf64_Word sh_name;         /* Section name (string tbl index) */
        public Elf64_Word sh_type;         /* Section type */
        public Elf64_XWord sh_flags;       /* Section flags */
        public Elf64_Addr sh_addr;         /* Section virtual addr at execution */
        public Elf64_Off sh_offset;        /* Section file offset */
        public Elf64_XWord sh_size;        /* Section size in bytes */
        public Elf64_Word sh_link;         /* Link to another section */
        public Elf64_Word sh_info;         /* Additional section information */
        public Elf64_XWord sh_addralign;   /* Section alignment */
        public Elf64_XWord sh_entsize;     /* Entry size if section holds table */
    }
}
