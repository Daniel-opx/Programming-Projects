#include<stdio.h>
#include "userinput.h"
#include <ctype.h>



/*this dunction gets int from user and validates the input.
the first argument is cons string that will be printed, the second one is the for the input validation- it limits the 
number that the user can input, the third parameter for the function is the min
the return value of the function is the int that the user inputed*/
int get_integer(const char* str, int max, int min)
{
    int input = 0,
        rtn = 0;

    for (;;)

    {
        printf("%s", str);
        rtn = scanf_s("%i", &input);
        if (rtn == EOF) {   /* user generates manual EOF */
            fputs("(user canceled input.)\n", stderr);
            
        }
        else if (rtn == 0) {    /* matching failure */
            fputs(" error: invalid integer input.\n", stderr);
            empty_stdin();
        }
        else if (input < min || max < input) {  /* validate range */
            fprintf(stderr, "error: integer out of range [%d-%d]\n",min, max);
            empty_stdin();
        }
        else {  /* good input */
            empty_stdin();
            break;
        }
    }
    return input;

}


void empty_stdin(void)
{
    int c = getchar();

    while (c != '\n' && c != EOF)
        c = getchar();
}