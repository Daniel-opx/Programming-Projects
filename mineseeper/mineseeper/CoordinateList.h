#pragma once

#include "Coordinate.h"

typedef struct _CoordinateNode {
	Coordinate coordinate;
	struct _CoordinateNode* next;
	struct _CoordinateNode* prev;
} CoordinateNode;

typedef CoordinateNode* PCoordinateNode;

typedef struct {
	int size;
	PCoordinateNode head;
} CoordinateList;

typedef CoordinateList* PCoordinateList;

PCoordinateList initializeList();
void add(PCoordinateList list, int x, int y);
void freeList(PCoordinateList list);
Coordinate getCoordinateAt(PCoordinateList list, int i);
int getSize(PCoordinateList list);


