/*
  Description:
    The startup source file for all ELF images parsed by Cosmos.Executable.ELF.
*/

#include "cosmos.h"

extern int main(int argc, char* argv[]);

void _start() {
    int argc;
    char** argv;
    envargs(&argc, &argv);

    int result = main(argc, argv);
    exit(result);
    halt();
}