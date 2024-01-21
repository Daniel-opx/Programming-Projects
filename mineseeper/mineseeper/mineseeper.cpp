// ConsoleApplication1.cpp : This file contains the 'main' function. Program execution begins and ends there.


#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <time.h>
#include <stdlib.h>
#include <ctype.h>
#include <math.h>
#include "userinput.h"
#include <time.h>

#include "CoordinateList.h"

#define BOARDSIZE 10
#define MINE 9

//#define DEBUG_MINEFIELD

typedef struct
{
	int minecount;
	char mine;
	bool clicked;
}cell;


int numofrecursion = 0;

//determinstic grid functions
void resetField(cell array[][BOARDSIZE]);
void printBoard(cell array[][BOARDSIZE]);
void setBoard(cell array[][BOARDSIZE]);
void mineCountSetter(cell array[][BOARDSIZE]);
void userChoice(void);
bool isMine(int i, int j, cell array[][BOARDSIZE]);
void revealBoard(cell array[][BOARDSIZE]);
int minecounter(int i, int j, cell array[][BOARDSIZE]);
void popAdjacentCells(int i, int j, cell array[][BOARDSIZE]);
int randRange(int minInclusive, int maxExclusive);



cell mineField[BOARDSIZE][BOARDSIZE];

//dynamically created grid functions
void DprintBoard(cell** array, int size); //done
void DsetBoard(cell** array, int size);//done
void DmineCountSetter(cell** array, int size);//done
int Dminecounter(int i, int j, cell** array,int size);//done
void DuserChoice(int size, cell** array); //done
bool DisMine(int i, int j, cell** array);//done
void DrevealBoard(cell** array, int size);//done
void DpopAdjacentCells(int i, int j, cell** array,int size); //done
cell** initialDyanmicallyGrid(int size);//done
void freeMineGrid(cell** mineGrid,int size);


int yaxischoice, xaxischoice;




int main(void)
{
	time_t t;
	srand((unsigned)time(&t));

	int size = get_integer("chhose the length and the width of your grid: ", 10, 5);
	cell** Dgrid=initialDyanmicallyGrid(size);

	
	DuserChoice(size,Dgrid);
	DsetBoard(Dgrid, size);
	DmineCountSetter(Dgrid, size);
	DprintBoard(Dgrid, size);
	

	
	

	while (true)
	{
		DuserChoice(size, Dgrid);
		DprintBoard(Dgrid, size);
		printf("\n");
		if (DisMine(yaxischoice, xaxischoice, Dgrid))
		{
			DrevealBoard(Dgrid,size);
			printf("you lose , there is a mine in [%i][%i]\n", xaxischoice, yaxischoice);
			break;
		}

	}
	freeMineGrid(Dgrid, size);

	printf("the num of recursive is %d\n", numofrecursion);


	
	
	

	

	return 2;


	
	
	
	


	/*resetField(mineField);
	setBoard(mineField);
	mineCountSetter(mineField);
	printBoard(mineField);
	while (true)
	{
		userChoice();
		printBoard(mineField);
		printf("\n");
		if (isMine(yaxischoice, xaxischoice, mineField))
		{
			revealBoard(mineField);
			printf("you lose , there is a mine in [%i][%i]",yaxischoice,xaxischoice );
			break;
		}

	}

	return 0;*/
}







//pre set board function
void printBoard(cell array[][BOARDSIZE])
{
	for (int i = 0; i < BOARDSIZE; i++)
	{
		for (int j = 0; j < BOARDSIZE; j++)
		{
#ifndef DEBUG_MINEFIELD
			if (array[i][j].clicked == false)
			{
				printf("%c", array[i][j].mine);
				printf(" ");
			}
			else  
			{
				if (array[i][j].minecount == MINE)
				{
					printf("*");
					printf(" ");
				}
				else if (array[i][j].minecount != MINE)
				{
					printf("%i", array[i][j].minecount);
					printf(" ");
				}
			}
#else
			if (array[i][j].minecount == MINE)
			{
				printf("**");
				printf(" ");
			}
			else if (array[i][j].minecount != MINE)
			{
				printf("%i%C", array[i][j].minecount, array[i][j].clicked ? 'O' : 'C');
				printf(" ");
			}
#endif
		}
		printf("\n");
	}
}

void resetField(cell array[][BOARDSIZE])
{
	for (int i = 0; i < BOARDSIZE; i++)
	{
		for (int j = 0; j < BOARDSIZE; j++)
		{
			array[i][j].minecount = 0;
			array[i][j].clicked = false;
			array[i][j].mine = 254;
		}
	}

}
int randRange(int minInclusive, int maxExclusive)
{
	return (rand() % (maxExclusive - minInclusive)) + minInclusive;
}

void setBoard(cell array[][BOARDSIZE])
{
	PCoordinateList list = initializeList();
	int min = (int)(pow(BOARDSIZE, 2) * 1 / 10), max = (int)(pow(BOARDSIZE, 2) * 2 / 10);
	int numberOfMines = randRange(min, max);
	for (int i = 0; i < BOARDSIZE; i++)
	{
		for (int j = 0; j < BOARDSIZE; j++)
		{
			add(list, i, j);
		}

	}

	for (int i = 0; i < numberOfMines; i++)
	{
		Coordinate co = getCoordinateAt(list, (rand() % getSize(list)));
		array[co.x][co.y].minecount = 9;
	}
	printf("you have %i mines in your borad \n",numberOfMines);
	freeList(list);

}

int minecounter(int i, int j, cell array[][BOARDSIZE])
{
	int minecounter = 0;
	for (int lengthOffset = -1; lengthOffset <= 1; lengthOffset++)
	{
		for (int widthOffset = -1; widthOffset <= 1; widthOffset++)
		{
			if (lengthOffset == 0 && widthOffset == 0)
			{
				continue;
			}

			if (i + lengthOffset >= 0 && i + lengthOffset < BOARDSIZE &&
				j + widthOffset >= 0 && j + widthOffset < BOARDSIZE)
			{
				if (array[i + lengthOffset][j + widthOffset].minecount == MINE)
				{
					minecounter++;
				}
			}
		}
	}
	return minecounter;

}

void mineCountSetter(cell array[][BOARDSIZE])
{
	for (int i = 0; i < BOARDSIZE; i++)
	{
		for (int j = 0; j < BOARDSIZE; j++)
		{
			if (array[i][j].minecount != MINE)
			{
				array[i][j].minecount = minecounter(i, j, array);
			}

		}
	}
}

void popAdjacentCells(int i, int j, cell array[][BOARDSIZE])
{
	cell* targetCell = &array[i][j];
	if (i < 0 || i >= BOARDSIZE || j < 0 || j >= BOARDSIZE || targetCell->clicked == true)
	{
		return;
	}
	targetCell->clicked = true;
	if (targetCell->minecount > 0)
	{
		return;
	}
	
	
	for (int lengthOffset = -1; lengthOffset <= 1; lengthOffset++)
	{
		for (int widthOffset = -1; widthOffset <= 1; widthOffset++)
		{
			int iWithOffset = i + lengthOffset,
				jWithOffset = j + widthOffset;

			popAdjacentCells(iWithOffset, jWithOffset, array);
		}
	}


}

void userChoice(void)
{
	static bool isFirstTime = true;
	yaxischoice = xaxischoice = -1;

	do
	{
		
		printf("select x cordinate between 1 and %i: ", BOARDSIZE);
		scanf_s("%i", &xaxischoice);
		printf("select y cordinate between 1 and %i: ", BOARDSIZE);
		scanf_s("%i", &yaxischoice);
	} while (yaxischoice > BOARDSIZE || yaxischoice < 1 || xaxischoice > BOARDSIZE || xaxischoice < 1 || (isFirstTime && mineField[yaxischoice - 1][xaxischoice - 1].minecount == MINE));

	mineField[yaxischoice - 1][xaxischoice - 1].clicked = true;
	popAdjacentCells(yaxischoice - 1, xaxischoice - 1, mineField);

	if (isFirstTime) {
		printf("you chose to start at %i,%i, now lets start the game!\n", xaxischoice, yaxischoice);
	}

	isFirstTime = false;

}

bool isMine(int i, int j, cell array[][BOARDSIZE])
{
	return(array[i - 1][j - 1].minecount == MINE);
}

void revealBoard(cell array[][BOARDSIZE])
{
	for (int i = 0; i < BOARDSIZE; i++)
	{
		for (int j = 0; j < BOARDSIZE; j++)
		{
			array[i][j].clicked = true;
		}

	}
	printBoard(mineField);

}







//dynamically set board functions
void DpopAdjacentCells(int i, int j, cell** array, int size)
{
	cell* targetCell = &array[i][j];
	if (i < 0 || i >= size || j < 0 || j >= size || targetCell->clicked == true )
	{
		return;
	}
	targetCell->clicked = true;
	//printf("[%d][%d] %d\n", xaxischoice, yaxischoice, targetCell->minecount);// for debuging
	if (targetCell->minecount > 0)
	{
		return;
	}

	numofrecursion++;
	for (int lengthOffset = -1; lengthOffset <= 1; lengthOffset++)
	{
		for (int widthOffset = -1; widthOffset <= 1; widthOffset++)
		{
			int iWithOffset = i + lengthOffset,
				jWithOffset = j + widthOffset;
			if (i + lengthOffset >= 0 && i + lengthOffset < size &&
				j + widthOffset >= 0 && j + widthOffset < size)
			{
				//printf("now the cell [%d][%d] entering the recursion\n", jWithOffset, iWithOffset);//for deubuging
				DpopAdjacentCells(iWithOffset, jWithOffset, array, size);
			}
			
		}
	}
}

cell** initialDyanmicallyGrid(int size)
{
	cell** grid = (cell**)malloc(sizeof(cell*) * size);
	if (grid == NULL)
	{
		fprintf(stderr, "Memory allocation failed for grid\n");
		exit(EXIT_FAILURE);
	}
	for (int i = 0; i < size; i++)
	{
		{
			grid[i] = (cell*)malloc(sizeof(cell) * size); //memory allocation
		}
	}

	for (int i = 0; i < size; i++) //reset field function
	{
		for (int j = 0; j < size; j++)
		{
			grid[i][j].minecount = 0;
			grid[i][j].clicked = false;
			grid[i][j].mine = 254;
		}
	}
	return grid;
}

void DprintBoard(cell** array, int size)
{
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
#ifndef DEBUG_MINEFIELD
			if (array[i][j].clicked == false)
			{
				printf("%c", array[i][j].mine);
				printf(" ");
			}
			else
			{
				if (array[i][j].minecount == MINE)
				{
					printf("*");
					printf(" ");
				}
				else if (array[i][j].minecount != MINE)
				{
					printf("%i", array[i][j].minecount);
					printf(" ");
				}
			}
#else
			if (array[i][j].minecount == MINE)
			{
				printf("**");
				printf(" ");
			}
			else if (array[i][j].minecount != MINE)
			{
				printf("%i%C", array[i][j].minecount, array[i][j].clicked ? 'O' : 'C');
				printf(" ");
			}
#endif
		}
		printf("\n");
	}
}

void DuserChoice(int size, cell** array)
{
	static bool isFirstTime = true;
	char formatString[50];  // Adjust the size as needed
	snprintf(formatString, sizeof(formatString), "select x coordinate between 1 and %d: ", size);
	/*int snprintf(char *str, size_t size, const char *format, …);
	*str : is a buffer.
	size : is the maximum number of bytes (characters) that will be written to the buffer.
	format : C string that contains a format string that follows the same specifications as format in printf
… : the optional ( …) arguments are just the string formats like (“%d”, myint) as seen in printf.*/


	char formatString2[50];  // Adjust the size as needed
	snprintf(formatString2, sizeof(formatString), "select y coordinate between 1 and %d: ", size);
	do
	{
		xaxischoice = get_integer(formatString, size, 1);
		yaxischoice = get_integer(formatString2, size, 1);
		
	} while ((isFirstTime && mineField[yaxischoice - 1][xaxischoice - 1].minecount == MINE));
	int realxaxischoice = xaxischoice - 1, realyaxixchoice = yaxischoice - 1;
	
	
	if (!isFirstTime)
	{
		DpopAdjacentCells(realyaxixchoice, realxaxischoice, array, size);
	}
		
	
	

	if (isFirstTime) {
		array[realyaxixchoice][realxaxischoice].clicked = true;
		printf("you chose to start at %i,%i, now lets start the game!\n", xaxischoice, yaxischoice);
	}

	isFirstTime = false;

}


void DsetBoard(cell** array,int size)
{
	PCoordinateList list = initializeList();
	int min = (int)(pow(size, 2) * 1 / 10), max = (int)(pow(size, 2) * 2 / 10);
	int numberOfMines = randRange(min, max);
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			add(list, i, j);
		}

	}
	int realxaxischoice = xaxischoice - 1, realyaxixchoice = yaxischoice - 1;
	
	
	int numOfJumps = (size * realyaxixchoice) + realxaxischoice;
	getCoordinateAt(list, numOfJumps);
	
	for (int i = 0; i < numberOfMines; i++)
	{
		Coordinate co = getCoordinateAt(list, (rand() % getSize(list)));
		array[co.x][co.y].minecount = 9;
	}
	printf("you have %i mines in your borad \n", numberOfMines);
	freeList(list);

}

void DmineCountSetter(cell** array,int size)
{
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			if (array[i][j].minecount != MINE)
			{
				array[i][j].minecount = Dminecounter(i, j, array,size);
			}

		}
	}
}
int Dminecounter(int i, int j, cell** array,int size)
{
	int minecounter = 0;
	for (int lengthOffset = -1; lengthOffset <= 1; lengthOffset++)
	{
		for (int widthOffset = -1; widthOffset <= 1; widthOffset++)
		{
			if (lengthOffset == 0 && widthOffset == 0)
			{
				continue;
			}

			if (i + lengthOffset >= 0 && i + lengthOffset < size &&
				j + widthOffset >= 0 && j + widthOffset < size)
			{
				if (array[i + lengthOffset][j + widthOffset].minecount == MINE)
				{
					minecounter++;
				}
			}
		}
	}
	return minecounter;

}

void DrevealBoard(cell** array,int size)
{
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			array[i][j].clicked = true;
		}
	}
	DprintBoard(array,size);
}


bool DisMine(int i, int j, cell** array)
{
	return(array[i - 1][j - 1].minecount == MINE);
}

void freeMineGrid(cell** mineGrid, int size)
{
	for (int i = 0; i < size; i++)
	{
		free(mineGrid[i]);
	}
	free(mineGrid);
}