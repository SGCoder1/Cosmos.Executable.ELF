#include "cosmos.internal.h"
#include "cosmos.loader.h"
#include "../include/cosmos.managed.h"

__attribute__((visibility("default"))) void __cosmos_exec_entry(void (*entryfn)()) {
    __cosmos_serial_write("[COSMOS_C_Int] Entered execution entry.\n");

    int argc = __env_argc();
    char* argv[argc + 1];       // NULL termination
    char* envp[1];              // NULL termination

    __cosmos_serial_write("[COSMOS_C_Int] Copying argv...\n");
    __cosmos_memcpy(argv, __env_argv(), sizeof(char*) * argc);

    __cosmos_serial_write("[COSMOS_C_Int] Calling function pointer...\n");
    __cosmos_serial_write("[COSMOS_C_Int] FnPtr address:");
    __cosmos_serial_write_hex_u64((unsigned long long)entryfn);
    __cosmos_serial_write("\n");

#ifdef __amd64
    __asm__ volatile (
    "jmp *%0"
    :
    : "r"(entryfn)
);
#else
    __asm__ volatile (
    "jmp *%0"
    :
    : "r"(entryfn)
#endif
}