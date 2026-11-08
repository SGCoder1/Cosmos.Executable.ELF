#pragma once

// NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE
// 
// Developers must implement each method defined below in their COSMOS operating system.
// Linking will fail if any of them are missing.
//
// NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE

/*
  Description:
    The COSMOS standard library header for ELF images loaded by Cosmos.Executable.ELF

  Remarks:
    This header file defines the specification for the ELF images.
*/

typedef struct __cosmos_file_ { unsigned long long unused; } FILE;

FILE* stdin;
FILE* stderr;
// FILE* stdout      Justification: putc, write and wrline already write to stdout


// ================================================================================
// Functions declarations
//=================================================================================

extern void envargs(int* argc, char*** argv);

extern void exit(int code);

extern void fclose(FILE* stream);

extern void fflush(FILE* stream);

extern FILE* fopen(const char* path, const char* mode);

extern void fread(FILE* stream, void* buf, unsigned long long len);

extern void halt();

extern void putc(char c);

extern void write(const char* str);

extern void wrline(const char* str);