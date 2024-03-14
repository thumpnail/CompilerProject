#include <stdio.h>
#include <stdlib.h>
#include "nano.h"

typedef struct {
    char* value;
    int length;
}TSTR;

int main() {
    str_t test = as_str("hello world");
    return test.length;
}