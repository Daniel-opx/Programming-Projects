#include "CoordinateList.h"
#include <stdlib.h>

static void freeNode(PCoordinateNode node)
{
    if (node == NULL)
    {
        return;
    }
    freeNode(node->next);
    free(node);
}

PCoordinateList initializeList()
{
    CoordinateList* list = (CoordinateList*)calloc(1, sizeof(CoordinateList));
    return list;
}

void add(PCoordinateList list, int x, int y)
{
    PCoordinateNode newNode = (PCoordinateNode)malloc(sizeof(CoordinateNode));
    newNode->coordinate.x = x;
    newNode->coordinate.y = y;
    newNode->next = list->head;
    newNode->prev = NULL;
    if (list->head != NULL)
    {
        list->head->prev = newNode;
    }
    list->head = newNode;
    list->size++;

    
}

void freeList(PCoordinateList list)
{
    freeNode(list->head);
    free(list);
}

static void removeNode(PCoordinateList list, PCoordinateNode node)
{
    if (node->prev) {
        node->prev->next = node->next;
    }
    // first node
    else 
    {
        list->head = node->next;
    }

    if (node->next) {
        node->next->prev = node->prev;
    }
    list->size--;
    free(node);
        
   
}

// get cell at index i and delete it.
Coordinate getCoordinateAt(PCoordinateList list, int i)
{
    PCoordinateNode cursor = list->head;
    for (int j = 0; j < i && cursor; j++)
    {
       cursor = cursor->next;
    }

    if (!cursor)
    {
        Coordinate invalid;
        invalid.x = -1;
        invalid.y = -1;
        return invalid;
    }

    Coordinate newcoordinate = cursor->coordinate; 
    removeNode(list, cursor);

    return newcoordinate;
}

int getSize(PCoordinateList list) 
{
    return list->size;
}


