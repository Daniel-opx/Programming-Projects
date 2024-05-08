// UniversityVotingProject.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include<string.h>


#define HEADMAXVOTES 1
#define CMTMAXVOTES 2

#define HEAD 1
#define COMITEE 2

#define MULTIUNIVERSITYERR 1
#define REDUNDENTVOTEERR 2
#define DOUBLEVOTEERR 3

#define LENGTHOFID 9;

#define true (1)
#define false (0)

typedef char bool ;


typedef struct outputVote
{
	char idBuffer[10];
	int voteType;
}outputVote;

typedef struct outputVoteList
{
	int size;
	outputVote allOutputVotes[50];
}outputVoteList;

typedef struct CandidateNumToVotes
{
	int numOfCandidate;
	int numOfVotes;
}CandidateNumToVotes;


typedef struct {
	FILE* academyFile;
	int eof;
	char studentID[10];
	char studentName[31];
	int voteType;
	int candidate;
}vote;

typedef struct StaticIdNode
{
	char id[10];
}StaticIdNode;

typedef struct StaticIdList
{
	int size;
	StaticIdNode idArr[20];

}StaticIdList;

bool isStaticListContainsID(StaticIdList* idList, int numberOfElements, const char* targetId);


void CreateUniName(int num, char* strBuffer, char* shortFileName);

void extractStudentInfo(char* str, vote* votePtr);

void extractNumFromStr(char* dest, char* src, int* intPtr);

void structBubbleSort(CandidateNumToVotes structArr[], int size); // my implementation of bubble sort for the struct CandidateNumToVotes

void outputVoteBubbleSort(outputVote arr[], int size);

bool myIsDigit(char c);

int main()
{
	int headArr[10] = { 0 };
	int committeeArr[10] = { 0 };
	vote allVotes[100];
	FILE* candidatesArr[10]; // file pointers for the voted claculation
	//in this files we will write 2 columns:
	//student id and voteType
	FILE* errFile;
	char filename[13], minID[10], shortFileName[13];
	int i, imin, cHead;
	int maxHead = 0, countMaxHead = 0, maxMem1 = 0, countMaxMem1 = 0, maxMem2 = 0, countMaxMem2 = 0, votesCount = 0, countHead = 0, countCmt = 0;
	int  cmt1 = -1, cmt2 = -1, head = -1;
	int universityCounter = 0;



	printf("Voting universities:\n=====================\n");
	// open all votes files for read access


	int indexHolder = 0; // this varibsle will save the index that the arrays are currently in - the numOfuni array and the DyanmicVoteArr array3
	for (int currUniNumber = 0; currUniNumber < 100; currUniNumber++)
	{

		CreateUniName(currUniNumber, filename,shortFileName);

		allVotes[indexHolder].academyFile = fopen(filename, "r");

		if (allVotes[indexHolder].academyFile == NULL)
		{
			continue;
		}
		else
		{
			printf("%s\n", shortFileName); //print the name as requested in the asignment requirements
			indexHolder++;  universityCounter++;
		}

	}

	vote* DyanmicVoteArr = (vote*)malloc(universityCounter * sizeof(vote)); // dynamic array of votes struct as requested in the assignment
	if (DyanmicVoteArr == NULL) //handling the casse of memory allocation failure
	{
		printf("memory allocation for DynamicVoteArr pointer failed\n");
		return 1;
	}


	for (int i = 0; i < universityCounter; i++)
	{
		DyanmicVoteArr[i].academyFile = allVotes[i].academyFile; //assigning all the votes struct that contain the vote txt files of the unis from the input files
	}


	/*open all candidates files for write access*/

	for (int i = 0; i < 10; i++)
	{
		snprintf(filename, 6, "%d.txt", i);
		candidatesArr[i] = fopen(filename, "w");

		if (candidatesArr[i] == NULL) //hamdles failure in the file opening
		{
			//close the pointrs that set till now
			free(DyanmicVoteArr);
			int j = 0;
			while (j < 10 || j < i)
			{
				fclose(candidatesArr[j]); // close every file that opened till now
				j++;
			}
			printf("candiate file number %d failed to open\n", i);
			return 1;
		}
	}


	// open error file
	errFile = fopen("canceled.txt", "w");

	if (errFile == NULL)
	{
		free(DyanmicVoteArr);

		for (int i = 0; i < 10; i++)
		{
			fclose(candidatesArr[i]);
		}
		printf("caceled file failed to opne\n");
		return 1;
	}






	int errorType;
	bool isFirst;
	int numOfCanceledFileLines = 0;

	int candidateCmtVotesCountsMembers[10];
	for (int i = 0; i < 10; i++) candidateCmtVotesCountsMembers[i] = 0; //reset the array
	int candidateHeadVotesCounts[10];
	for (int i = 0; i < 10; i++) candidateHeadVotesCounts[i] = 0; //reset the array

	StaticIdList staticCanceledList;
	staticCanceledList.size = 0;


	int numOfLinesInCandiateOutputFile[10];
	for (int i = 0; i < 10; i++)
	{
		numOfLinesInCandiateOutputFile[i] = 0;
	}

	StaticIdList* StaticListArr = malloc(universityCounter * sizeof(StaticIdList));
	if (StaticListArr == NULL)
	{
		free(DyanmicVoteArr);
		for (int i = 0; i < 10; i++)
		{
			fclose(candidatesArr[i]);
		}
		fclose(errFile);
		printf("memory allocation for StaticListArr ptr failed\n");
		return 1;
	}
	for (int i = 0; i < universityCounter; i++) //reset the size property so they dont get garbage value
	{
		StaticListArr[i].size = 0;
	}
	
	// read first line from each academy vote file
	for (int i = 0; i < universityCounter; i++)
	{
		vote* currVote = &DyanmicVoteArr[i];
		isFirst = true;
		char buffer[128];
		bool isVoteCancelled = false;
		while (fgets(buffer, 128, DyanmicVoteArr[i].academyFile) != NULL)
		{
			extractStudentInfo(buffer, &DyanmicVoteArr[i]);
			if (!isFirst)
			{
				if (strcmp(minID, currVote->studentID) == 0)
				{
					isVoteCancelled = false;
					if (currVote->voteType == COMITEE)
					{
						if ((cmt1 != -1 && cmt2 != -1) || countCmt == 2) // handles redundent vote
						{
							errorType = REDUNDENTVOTEERR;
							isVoteCancelled = true;
						}
						else if (currVote->candidate == cmt1 || currVote->candidate == head || currVote->candidate == cmt2) // handles double vote
						{
							errorType = DOUBLEVOTEERR;
							isVoteCancelled = true;
						}

						//todo:
						// need to handle the first cancel situation-sme person in different uni
						if (isVoteCancelled)
						{
							if (!isStaticListContainsID(&staticCanceledList, staticCanceledList.size,currVote->studentID))
							{
								numOfCanceledFileLines++;
								fprintf(errFile, "%s%d\n", currVote->studentID, errorType);

								strcpy(staticCanceledList.idArr[staticCanceledList.size].id, currVote->studentID);
								staticCanceledList.size++;

								
							}
							continue;
						}

						//assign the candidate to the corresponding cmt member variable
						if (cmt1 != -1)
						{
							cmt2 = currVote->candidate;
							candidateCmtVotesCountsMembers[currVote->candidate]++;
							countCmt++;
							
							numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
						}
						else
						{
							cmt1 = currVote->candidate;
							candidateCmtVotesCountsMembers[currVote->candidate]++;
							countCmt++;

							numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
						}
						fprintf(candidatesArr[currVote->candidate], "%s%d\n", currVote->studentID, currVote->voteType);

					}
					else if (currVote->voteType == HEAD)
					{
						if (head != -1 || countHead == 1)
						{
							errorType = REDUNDENTVOTEERR;
							isVoteCancelled = true;
						}
						else if (currVote->candidate == cmt1 || currVote->candidate == head || currVote->candidate == cmt2)
						{
							errorType = DOUBLEVOTEERR;
							isVoteCancelled = true;
						}


						if (isVoteCancelled)
						{
							if (!isStaticListContainsID(&staticCanceledList, staticCanceledList.size, currVote->studentID))
							{
								numOfCanceledFileLines++;
								fprintf(errFile, "%s%d\n", currVote->studentID, errorType);
								strcpy(staticCanceledList.idArr[staticCanceledList.size].id, currVote->studentID);
								staticCanceledList.size++;

								
							}
							continue;
						}
						head = currVote->candidate;
						candidateHeadVotesCounts[currVote->candidate]++;
						numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
						countHead++;
						fprintf(candidatesArr[currVote->candidate], "%s%d\n", currVote->studentID, currVote->voteType);
					}
				}
				else
				{
					isVoteCancelled = false;
					for (int numOfList = 0; numOfList < i; numOfList++)
					{
						if (isStaticListContainsID(&StaticListArr[numOfList], StaticListArr[numOfList].size, currVote->studentID))
						{
							errorType = MULTIUNIVERSITYERR;
							isVoteCancelled = true;
							break;
						}
					}
					if (isVoteCancelled)
					{
						if (!isStaticListContainsID(&staticCanceledList, staticCanceledList.size, currVote->studentID))
						{
							numOfCanceledFileLines++;
							fprintf(errFile, "%s%d\n", currVote->studentID, errorType);
							strcpy(staticCanceledList.idArr[staticCanceledList.size].id, currVote->studentID);
							staticCanceledList.size++;

							
						}
						continue;
					}


					cmt1 = -1, cmt2 = -1, head = -1, countCmt = 0, countHead = 0; // reset the varisbles that keep the state
					if (currVote->voteType == COMITEE)
					{
						cmt1 = currVote->candidate;
						candidateCmtVotesCountsMembers[currVote->candidate]++;
						countCmt++;

						numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature


					}
					else if (currVote->voteType == HEAD)
					{
						head = currVote->candidate;
						candidateHeadVotesCounts[currVote->candidate]++;
						countHead++;

						numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
					}
					int previousIndex = numOfLinesInCandiateOutputFile[currVote->candidate] - 1;
					strcpy(StaticListArr[i].idArr[StaticListArr[i].size].id, currVote->studentID);
					StaticListArr[i].size++;
					////===============================================//
					//printf("just printed to the %d place of the %d list id: %s\n", StaticListArr[i].size - 1, i, StaticListArr[i].idArr[StaticListArr[i].size - 1].id);

					////==============================================//


					
					
					fprintf(candidatesArr[currVote->candidate], "%s%d\n", currVote->studentID, currVote->voteType);
				}
			}
			else
			{
				cmt1 = -1, cmt2 = -1, head = -1, countCmt = 0, countHead = 0; // reset the varisbles that keep the state
				isVoteCancelled = false;
				for (int numOfList = 0; numOfList < i; numOfList++)
				{
					if (isStaticListContainsID(&StaticListArr[numOfList], StaticListArr[numOfList].size, currVote->studentID))
					{
						errorType = MULTIUNIVERSITYERR;
						isVoteCancelled = true;
						break;
					}
				}
				if (isVoteCancelled)
				{
					if (!isStaticListContainsID(&staticCanceledList, staticCanceledList.size, currVote->studentID))
					{
						numOfCanceledFileLines++;
						fprintf(errFile, "%s%d\n", currVote->studentID, errorType);
						strcpy(staticCanceledList.idArr[staticCanceledList.size].id, currVote->studentID);
						staticCanceledList.size++;

						
					}
					continue;
				}
				if (currVote->voteType == COMITEE)
				{
					cmt1 = currVote->candidate;
					candidateCmtVotesCountsMembers[currVote->candidate]++;
					countCmt++;

					numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
				}
				else if (currVote->voteType == HEAD)
				{
					head = currVote->candidate;
					candidateHeadVotesCounts[currVote->candidate]++;
					countHead++;

					numOfLinesInCandiateOutputFile[currVote->candidate]++; // new feature
				}
				int previousIndex = numOfLinesInCandiateOutputFile[currVote->candidate] - 1;
				strcpy(StaticListArr[i].idArr[StaticListArr[i].size].id, currVote->studentID);
				StaticListArr[i].size++;
				////===============================================//
				//printf("just printed to the %d place of the %d list id: %s\n", StaticListArr[i].size -1, i, StaticListArr[i].idArr[StaticListArr[i].size - 1].id);

				////==============================================//


				
				
				fprintf(candidatesArr[currVote->candidate], "%s%d\n", currVote->studentID, currVote->voteType);

			}

			/*printf("the id of the student is %s\n", minID);*/
			/*printf("the id of the structstudnt is %s\n", DyanmicVoteArr[i].studentID);
			printf("the student chose cndidate number %d \n", DyanmicVoteArr[i].candidate);
			printf("the student chose vote number %d \n", DyanmicVoteArr[i].voteType);*/

			strcpy(minID, DyanmicVoteArr[i].studentID);
			/*printf("%s\n", buffer);*/
			isFirst = false;
		}
		/*printf("finished the %dth file\n=============================\n", i);*/
	}

	/*printf("MEM1 cmt votes:==============================================================================================\n");
	for (int i = 0; i < 10; i++)
	{
		printf("candidate #%d got %d votes\n", i, candidateCmtVotesCountsMembers[i]);
	}
	printf("head votes:==============================================================================================\n");
	for (int i = 0; i < 10; i++)
	{
		printf("candidate #%d got %d votes\n", i, candidateHeadVotesCounts[i]);
	}*/


	/*printIds(&list);
	printf("now printing the list in accending order\n");
	for (int i = 0; i < universityCounter; i++)
	{
		printIds(&listArr[i]);
		printf("done with %d list =============\n", i);
	}*/


	// main loop :
	//      1. read votes resulte of current student
	//      2. check if the vote is legal
	//          if the vote is legal => update votes array
	//          else => write an error line to errfile
	//......

	// count results for head
	//......

	// count results for member1
	//......

	// count results for member2
	//......
	for (int i = 0; i < 10; i++) 
	{
		 
		snprintf(filename, 6, "%d.txt", i);
		fclose(candidatesArr[i]);
		candidatesArr[i] = fopen(filename, "r");

		if (candidatesArr[i] == NULL)
		{
			free(DyanmicVoteArr);
			free(StaticListArr);
			fclose(errFile);
			for (int j = 0; j < 10; j++)
			{
				fclose(candidatesArr[j]);
			}
			printf("coudlnt open file with name of : %s", filename);
			return 1;
		}


		fseek(candidatesArr[i], 0, SEEK_SET); //set the "cursor" of the reader to the begginig of the file

		char buffer[128];
		char idBuffer[10];
		int voteType;

		outputVoteList temp;
		temp.size = numOfLinesInCandiateOutputFile[i];
		int indexTrackerWhileLoop = 0;

		while (fgets(buffer, 128, candidatesArr[i]) != NULL) // read each line to a buffer
		{
			/*printf("the buffer is: %s", buffer);*/
			extractNumFromStr(idBuffer, buffer, &voteType);

			strcpy(temp.allOutputVotes[indexTrackerWhileLoop].idBuffer, idBuffer);
			temp.allOutputVotes[indexTrackerWhileLoop].idBuffer[9] = '\0';
			temp.allOutputVotes[indexTrackerWhileLoop].voteType = voteType;
			indexTrackerWhileLoop++;

			/*printf("the id is %s adn the vote type is:%d\n", idBuffer,voteType);*/
			if (isStaticListContainsID(&staticCanceledList, staticCanceledList.size, idBuffer))
			{
				if (voteType == HEAD)
				{
					candidateHeadVotesCounts[i]--;
				}
				else if (voteType == COMITEE)
				{
					candidateCmtVotesCountsMembers[i]--;
				}
			}

		}
		snprintf(filename, 6, "%d.txt", i);
		candidatesArr[i] = fopen(filename, "w");
		outputVoteBubbleSort(temp.allOutputVotes, temp.size);
		for (int j = 0; j < temp.size; j++)
		{
			if (!isStaticListContainsID(&staticCanceledList, staticCanceledList.size, temp.allOutputVotes[j].idBuffer))
			{
				fprintf(candidatesArr[i], "%s%d\n", temp.allOutputVotes[j].idBuffer, temp.allOutputVotes[j].voteType);
				/*printf("printing in %d.txt : %s%d\n", i, temp.allOutputVotes[j].idBuffer, temp.allOutputVotes[j].voteType);*/
			}


		}

		/*printf("going to next page\n\n");*/

	}
	fclose(errFile);
	errFile = fopen("canceled.txt", "r");




	outputVote* dynamicOutputArr = malloc(numOfCanceledFileLines * sizeof(outputVote));
	//todo - handle failure in malloc or file openinf
	if (errFile == NULL || dynamicOutputArr == NULL)
	{
		free(DyanmicVoteArr);
		free(StaticListArr);
		for (int j = 0; j < 10; j++)
		{
			fclose(candidatesArr[j]);
		}
		
		
		if ((errFile == NULL))
		{
			free(dynamicOutputArr);
			printf("failed to open canceled file fow reading ");
		}
		else if (dynamicOutputArr == NULL)
		{
			fclose(errFile);
			printf("memory allocation for dynamicOutputArr pointer failed ");
		}
		return 1;
	}

	char buffer[128];
	outputVote outputBuffer;
	for (int currIndex = 0; currIndex < numOfCanceledFileLines; currIndex++)
	{
		if (fgets(buffer, 128, errFile) != NULL)
		{
			extractNumFromStr(dynamicOutputArr[currIndex].idBuffer, buffer, &dynamicOutputArr[currIndex].voteType);
			/*printf("just got the followng details: id is %s || Vote type is %d\n", dynamicOutputArr[currIndex].idBuffer, dynamicOutputArr[currIndex].voteType);*/
			
		}
	}
	outputVoteBubbleSort(dynamicOutputArr, numOfCanceledFileLines);
	errFile = fopen("canceled.txt", "w");
	if (errFile == NULL)
	{
		free(DyanmicVoteArr);
		free(dynamicOutputArr);
		
		for (int j = 0; j < 10; j++)
		{
			fclose(candidatesArr[j]);
		}
		
		printf("failed to open canceled file for writing");
		return 1;
	}
	for (int i = 0; i < numOfCanceledFileLines; i++)
	{
		outputVote temp = dynamicOutputArr[i];
		fprintf(errFile, "%s%d\n", temp.idBuffer, temp.voteType);
	}

	/*printf("MEM1 cmt votes:==============================================================================================\n");
	for (int i = 0; i < 10; i++)
	{
		printf("candidate #%d got %d votes\n", i, candidateCmtVotesCountsMembers[i]);
	}
	printf("head votes:==============================================================================================\n");
	for (int i = 0; i < 10; i++)
	{
		printf("candidate #%d got %d votes\n", i, candidateHeadVotesCounts[i]);
	}*/



	

	CandidateNumToVotes tupleArr[10]; // array to store each candidaet and its corresponding num of votes
	printf("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n");
	printf("@                      Elections results                    @\n");
	printf("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n");

	printf("\nHEAD\n");
	printf("====\n");
	for (int i = 0; i < 10; i++)
	{
		tupleArr[i].numOfCandidate = i;
		tupleArr[i].numOfVotes = candidateHeadVotesCounts[i];
		/*printf("%d:%d\n", tupleArr[i].numOfCandidate, tupleArr[i].numOfVotes);*/
	}
	structBubbleSort(tupleArr, 10);


	/*printf("\nSorted array by descending order of numOfVotes:\n");*/
	/*for (int i = 0; i < 10; i++) {
		printf("Candidate %d: %d votes\n", tupleArr[i].numOfCandidate, tupleArr[i].numOfVotes);
	}*/


	if (tupleArr[0].numOfVotes == tupleArr[1].numOfVotes)
	{
		maxHead = tupleArr[0].numOfVotes;
		printf("there are more than one elected for head:\n");
		for (int i = 0; i < 10; i++)
		{
			if (tupleArr[i].numOfVotes == maxHead)
			{
				printf("Candidate #%d with %d votes\n", tupleArr[i].numOfCandidate, tupleArr[i].numOfVotes);
			}
		}

	}
	else if (tupleArr[0].numOfVotes > tupleArr[1].numOfVotes)
	{
		printf("there is only one elected for head:\n");
		printf("candidate #%d with %d votes.\n", tupleArr[0].numOfCandidate, tupleArr[0].numOfVotes);

	}
	else if (tupleArr[0].numOfVotes == 0)
	{
		printf("there is no one elected for head");
	}



	printf("\nMEMBER1\n");
	printf("=======\n");
	printf("\n");

	CandidateNumToVotes tupleArrForMem1[10];
	for (int i = 0; i < 10; i++)
	{
		tupleArrForMem1[i].numOfCandidate = i;
		tupleArrForMem1[i].numOfVotes = candidateCmtVotesCountsMembers[i];
		/*printf("%d:%d\n", tupleArrForMem1[i].numOfCandidate, tupleArrForMem1[i].numOfVotes);*/
	}
	/*printf("\nSorted array by descending order of numOfVotes:\n");
	for (int i = 0; i < 10; i++) {
		printf("Candidate %d: %d votes\n", tupleArrForMem1[i].numOfCandidate, tupleArrForMem1[i].numOfVotes);
	}*/
	structBubbleSort(tupleArrForMem1, 10);
	/*printf("\nSorted array by descending order of numOfVotes:\n");
	for (int i = 0; i < 10; i++) {
		printf("Candidate %d: %d votes\n", tupleArrForMem1[i].numOfCandidate, tupleArrForMem1[i].numOfVotes);
	}*/


	if (tupleArrForMem1[0].numOfVotes == 0)
	{
		printf("there is no one elected for member1.\n");
	}
	else if (tupleArrForMem1[0].numOfVotes == tupleArrForMem1[1].numOfVotes)
	{
		printf("there are more than one elected for member1:\n");
		maxMem1 = tupleArrForMem1[0].numOfVotes;
		for (int i = 0; i < 10; i++)
		{
			if (tupleArrForMem1[i].numOfVotes == maxMem1)
			{
				printf("Candidate #%d with %d votes.\n", tupleArrForMem1[i].numOfCandidate, tupleArrForMem1[i].numOfVotes);
			}
		}
	}
	else if (tupleArrForMem1[0].numOfVotes > tupleArrForMem1[1].numOfVotes)
	{
		countMaxMem1 = 1;
		maxMem1 = tupleArrForMem1[0].numOfVotes;
		printf("there is only one elected for member1:\nCandidate #%d with %d votes.", tupleArrForMem1[0].numOfCandidate, tupleArrForMem1[0].numOfVotes);
	}




	if (countMaxMem1 == 1) {
		printf("\n");
		printf("\nMEMBER2\n");
		printf("=======\n");
		printf("\n");

		CandidateNumToVotes tupleArrForMem2[10];
		for (int i = 0; i < 10; i++)
		{
			tupleArrForMem2[i].numOfCandidate = i;
			tupleArrForMem2[i].numOfVotes = candidateCmtVotesCountsMembers[i];
			/*printf("%d:%d\n", tupleArrForMem2[i].numOfCandidate, tupleArrForMem2[i].numOfVotes);*/
		}
		/*printf("\nSorted array by descending order of numOfVotes:\n");
		for (int i = 0; i < 10; i++) {
			printf("Candidate %d: %d votes\n", tupleArrForMem2[i].numOfCandidate, tupleArrForMem2[i].numOfVotes);
		}*/
		structBubbleSort(tupleArrForMem2, 10);
		/*printf("\nSorted array by descending order of numOfVotes:\n");
		for (int i = 0; i < 10; i++) {
			printf("Candidate %d: %d votes\n", tupleArrForMem2[i].numOfCandidate, tupleArrForMem2[i].numOfVotes);
		}*/
		maxMem2 = tupleArrForMem2[1].numOfVotes;
		if (maxMem2 == 0)
		{
			printf("there is no one elected for member2.\n");
		}
		else if (tupleArrForMem2[1].numOfVotes == tupleArrForMem2[2].numOfVotes)
		{
			printf("there are more than one elected for member2:\n");
			for (int i = 1; i < 10; i++)
			{
				if (tupleArrForMem2[i].numOfVotes == maxMem2)
				{
					printf("Candidate #%d with %d votes.\n", tupleArrForMem2[i].numOfCandidate, tupleArrForMem2[i].numOfVotes);
				}
			}
		}
		else if (tupleArrForMem2[1].numOfVotes > tupleArrForMem2[2].numOfVotes)
		{
			countMaxMem1 = 1;
			maxMem1 = tupleArrForMem1[0].numOfVotes;
			printf("there is only one elected for member1:\nCandidate #%d with %d votes.", tupleArrForMem1[0].numOfCandidate, tupleArrForMem1[0].numOfVotes);
		}
	}


	////===============================================//
	//printf("\n============================================================\n");
	//for (int i = 0; i < 10; i++)
	//{
	//	printf("the num of line in candidate file number %d: is %d lines\n", i, numOfLinesInCandiateOutputFile[i]);
	//}
	//printf("\n============================================================\n");
	////==========================================//


	free(DyanmicVoteArr);
	for (int i = 0; i < 10; i++)
	{
		fclose(candidatesArr[i]);
	}
	fclose(errFile);
	free(StaticListArr);
	free(dynamicOutputArr);

}

void structBubbleSort(CandidateNumToVotes structArr[], int size)
{
	for (int i = 0; i < size - 1; i++)
	{
		for (int j = 0; j < (size - i - 1); j++)
		{
			if (structArr[j].numOfVotes < structArr[j + 1].numOfVotes) {
				CandidateNumToVotes temp = structArr[j];
				structArr[j] = structArr[j + 1];
				structArr[j + 1] = temp;
			}
			if (structArr[j].numOfVotes == structArr[j + 1].numOfVotes) // handles a situation in which the there is a tie between 2 members
				// the the sort goes by accending order of the candiate number
			{
				if (structArr[j].numOfCandidate > structArr[j + 1].numOfCandidate)
				{
					CandidateNumToVotes temp = structArr[j];
					structArr[j] = structArr[j + 1];
					structArr[j + 1] = temp;
				}
			}
		}
	}
}


bool myIsDigit(char c)
{
	return c >= '0' && c <= '9';
}

void CreateUniName(int num, char* strBuffer,char* shortFileName)
{


	bool greatertThanTen = num >= 10;
	if (!greatertThanTen)
	{
		//snprintf - function that stores a string a buffer, the arguments in this function allow to use format args (place holder and such)
		snprintf(strBuffer, 12, "univ0%d.txt", num);
		sprintf(shortFileName, "univ0%d",num);
	}
	else
	{
		snprintf(strBuffer, 12, "univ%d.txt", num);
		sprintf(shortFileName, "univ%d", num);
	}

}

void extractStudentInfo(char* str, vote* votePtr) {
	int i = 0;
	int counter = 0;
	int destIndex = 0;
	while (str[i] && counter < 9) {
		if (isdigit(str[i])) {
			votePtr->studentID[destIndex++] = str[i];
			counter++;
		}
		i++;
	}
	votePtr->studentID[destIndex] = '\0';
	votePtr->voteType = (char)str[i] - '0';
	i++;
	votePtr->candidate = (char)str[i] - '0';
}

void extractNumFromStr(char* dest, char* src, int* intPtr)
{
	int i = 0, counter = 0, destIndex = 0;
	while (src[i] && counter < 9)
	{
		if (isdigit(src[i]))
		{
			counter++;
			dest[destIndex++] = src[i];
		}
		i++;
		dest[destIndex] = '\0';
		*(intPtr) = (char)src[i] - '0';
	}
}

void outputVoteBubbleSort(outputVote arr[], int size)
{
	char* currPtr; char* nextPtr;

	for (int i = 0; i < size - 1; i++)
	{
		for (int j = 0; j < (size - i - 1); j++)
		{
			bool isCurrBigger = false; bool isNextBigger = false;
			int comparerResault = 0;
			outputVote curr = arr[j]; outputVote next = arr[j + 1];
			currPtr = curr.idBuffer; nextPtr = next.idBuffer;
			int counter = 0;
			while (counter < 9)
			{
				if (*currPtr == *nextPtr)
				{
					counter++; currPtr++; nextPtr++;
					continue;
				}
				else
				{
					comparerResault = *nextPtr - *currPtr;
					break;
				}
			}
			if (comparerResault < 0)
			{
				outputVote temp = arr[j];
				arr[j] = arr[j + 1];
				arr[j + 1] = temp;
			}
		}
	}
}

bool isStaticListContainsID(StaticIdList* idList, int numberOfElements, const char* targetId)
{
	for (int i = 0; i < numberOfElements; i++)
	{
		if (strcmp(idList->idArr[i].id, targetId) == 0)return true;
	}
	return false;
}