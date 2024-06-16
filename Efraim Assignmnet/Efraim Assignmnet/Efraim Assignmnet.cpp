// Efraim Assignmnet.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h> 
#include <time.h> 
#include <string.h>

typedef struct stringList {
	char** wordList;
	int Count;
}stringList;

typedef struct
{
	char name[30];
	int Id;
	char Party[20];
	int TimeInChair;

}ParlimentMember;

#define MUX_1B 1
#define MUX_1A (MUX_1B << 1)

#define MUX_2B 2
#define MUX_2A (MUX_2B << 1)

#define N 101

#define NUMBER_OF_MEMBERS 120

void empty_stdin();
void Exercise1();
void Exercise2();
void Exercise3();
void Exercise4();

int generateRandom(int lower, int upper)
{
	int i, num;
	for (i = 0; i < 5; i++) {
		num = (rand() %
			(upper - lower + 1)) + lower;
	}
	return num;
}
int readInt()
{
	int num;
	scanf_s("%d", &num);
	empty_stdin();
	return num;
}
char readChar()
{
	char c;
	scanf_s("%c", &c);
	empty_stdin();
	return c;
}
char* readString(char* c)
{
	fgets(c, N, stdin);
	return c;
}
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
			fprintf(stderr, "error: integer out of range [%d-%d]\n", min, max);
			empty_stdin();
		}
		else {  /* good input */
			empty_stdin();
			break;
		}
	}
	return input;

}
int get_integer_without_max(const char* str, int min)
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
		else if (input < min) {  /* validate range */
			fprintf(stderr, "error: integer lower than minimum [%d]\n", min);
			empty_stdin();
		}
		else {  /* good input */
			empty_stdin();
			break;
		}
	}
	return input;

}

int main()
{
	while (true)
	{
		int gameChoice = get_integer("What is your choice:\n1 - Exercise 1\n2 - Exercise 2\n3 - Exercise 3\n4 - Exercise 4\n5 - Exit from program\n", 5, 1);

		switch (gameChoice)
		{
		case 1:
			Exercise1();
			break;
		case 2:
			Exercise2();
			break;
		case 3:
			Exercise3();
			break;
		case 4:
			Exercise4();
			break;
		case 5:
			return 0;
		}
	}
	

	
}


void Exercise1()
{
	int capital = get_integer_without_max("please enetr capital: ", 1);
	printf("you started with %d shekels\n", capital);
	int option, bet;
	bool isGameRunning = true;
	bool isWinning;

	do
	{

		printf("What is your option(between 1-3) and what is your bet? ");
		option = readInt();
		bet = readInt();

		srand(time(0));
		int diceSum = generateRandom(2, 12);

		isWinning = false;

		switch (option)
		{
		case 1:
			isWinning = diceSum > 7;
			break;
		case 2:
			isWinning = diceSum < 7;
			break;
		case 3:
			isWinning = diceSum == 7;
			break;
		}
		bet *= isWinning ? 1 : -1;
		capital += bet;
		if (option == 3 && isWinning) capital += 2 * bet;


		printf("your capital now is %d\n", capital);

		if (capital <= 0) {
			printf("Thank you. Good bye");
			break;
		}

		bool isValidInput = true;

		do {
			printf("do you want to continue ? (y/n)");
			char userInput = readChar();

			switch (userInput)
			{
			case 'n':
				printf("closing geame");
				isGameRunning = false;
				break;
			case 'y':

				break;
			default:
				isValidInput = false;
				printf("error- choose proper answer 'y' or 'n'\n");
				break;
			}

		} while (!isValidInput);



	} while (isGameRunning);


} // complete



int MUX(int I[], int S[], int M)
{
	/*
	for (int i = 0; i < M; i++)
	{
		index = index | S[i] << i;
	}*/
	int index = 0;
	int multiplier = 1;
	for (int i = 0; i < M; i++)
	{
		index += S[i] * (multiplier);
		multiplier *= 2;
	}
	return I[index];

}
void Exercise2()
{
	int I1[MUX_1A], S1[MUX_1B], I2[MUX_2A], S2[MUX_2B];

	printf("choose numbers for first mux\n");
	for (int i = 0; i < MUX_1B; i++)
	{
		char prompt[50];
		snprintf(prompt, 50, "choose number for the %dth selector - 0/1: \n", i);
		S1[i] = get_integer(prompt, 1, 0);
	}
	for (int i = 0; i < MUX_1A; i++)
	{
		char prompt[50];
		snprintf(prompt, 50, "choose number for the %dth Input - 0/1: \n", i);
		I1[i] = get_integer(prompt, 1, 0);
	}

	printf("choose numbers for second mux\n");
	for (int i = 0; i < MUX_2B; i++)
	{
		char prompt[50];
		snprintf(prompt, 50, "choose number for the %dth selector - 0/1: \n", i);
		S2[i] = get_integer(prompt, 1, 0);
	}
	for (int i = 0; i < MUX_2A; i++)
	{
		char prompt[50];
		snprintf(prompt, 50, "choose number for the %dth Input - 0/1: \n", i);
		I2[i] = get_integer(prompt, 1, 0);
	}
	int mux1Res = MUX(I1, S1, MUX_1B);
	int mux2Res = MUX(I2, S2, MUX_2B);

	printf("the resault of this muxes is: %d \n", mux1Res | mux2Res);
}


void CHECK_LETTER(char* str, char letter)
{
	const char s[2] = " ";
	char* token;
	int wordCounter = 1;


	/* get the first token */
	token = strtok(str, s);

	/* walk through other tokens */
	while (token != NULL) {
		/*printf(" %s\n", token);*/
		int letterCounter = 0;

		char* letterSearchPtr = token;

		for (; *letterSearchPtr != '\0'; letterSearchPtr++)
		{
			if (*letterSearchPtr == letter) letterCounter++;
		}
		printf("Word num %d contains letter %c %d times\n", wordCounter, letter, letterCounter);
		wordCounter++;
		token = strtok(NULL, s);
	}
}
void Exercise3()
{
	char str[N];
	printf("please write your sentence , length must now exceed %d chars and then write a letter to count \n", N);
	readString(str);

	
	printf("choose your letter");
	char letter = readChar();

	CHECK_LETTER(str, letter);
}



int ParlimentMembersComparere(const void* a, const void* b)
{
	ParlimentMember* member1 = (ParlimentMember*)a;
	ParlimentMember* member2 = (ParlimentMember*)b;

	return member1->Id - member2->Id;

}
void Exercise4()
{
	ParlimentMember kneset[NUMBER_OF_MEMBERS];

	for (int i = 0; i < NUMBER_OF_MEMBERS; i++)
	{
		printf("enter name: ");
		readString(kneset[i].name);

		printf("enter id: ");
		kneset[i].Id = readInt();

		printf("enter Party : ");
		readString(kneset[i].Party);

		printf("enter time on chair: ");
		kneset[i].TimeInChair = readInt();
	}

	qsort(kneset, NUMBER_OF_MEMBERS, sizeof(kneset[0]), ParlimentMembersComparere);

	ParlimentMember* nautymember = NULL;
	ParlimentMember* nautymemberDuplicate = NULL;

	for (int i = 0; i < NUMBER_OF_MEMBERS - 1; i++)
	{
		if (kneset[i].Id == kneset[i + 1].Id && strcmp(kneset[i].Party, kneset[i + 1].Party) != 0)
		{
			nautymember = &kneset[i];
			nautymemberDuplicate = &kneset[i + 1];
			break;
		}
	}
	printf("name: %s\nId:%d \nparty1:%s\n party2:%s ", nautymember->name, nautymember->Id,
		nautymember->Party, nautymemberDuplicate->Party);
}





void empty_stdin()
{
	int c = getchar();

	while (c != '\n' && c != EOF)
		c = getchar();
}


