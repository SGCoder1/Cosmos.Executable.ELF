#pragma once

/*
  Description:
    Contains all bridged function signatures defined in the COSMOS kit.

  Remarks:
    These function can only be called on ELF images run by Cosmos.Executable.ELF.
*/

extern void __cosmos_serial_write(const char* str);
extern void __cosmos_serial_write_hex_u32(unsigned int v);
extern void __cosmos_serial_write_hex_u64(unsigned long long v);
extern void __cosmos_serial_write_dec_u32(unsigned int v);
extern void __cosmos_serial_write_dec_u64(unsigned int v);
extern void __cosmos_memcpy(void* dst, const void* src, __SIZE_TYPE__ size);