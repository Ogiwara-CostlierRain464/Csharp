#pragma once
#include <windows.h>
#include <vector>
#include "Block.h"
#include "Color.h"
#include "Drawer.h"
typedef struct Color Color;

using namespace std;

#define CELLSIZE 40//��̃}�X�̑傫��

#define WIDTH 10
#define HEIGHT 15

extern HWND window;//at source.cpp