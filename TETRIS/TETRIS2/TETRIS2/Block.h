#pragma once
#include "main.h"
#include <vector>

using namespace std;

struct Block {
	int X;
	int Y;
	Color c;
};

typedef struct Block Block;

extern vector<Block> stopingblockstuck;

extern vector<Block> movingblockstuck;

void spawn();

void goDown();

void goLeft();

void goRight();