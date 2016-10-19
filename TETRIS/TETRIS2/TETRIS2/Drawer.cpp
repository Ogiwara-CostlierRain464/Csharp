#include "Drawer.h"

void CallWMPAINT() {
	InvalidateRect(window, NULL, TRUE);
}

//color flag!
Color flag;

bool isblock(int x, int y) {
	for (Block a : stopingblockstuck) {

		if (a.X == 0 ||  a.Y == 0) continue;

		if ((a.X * CELLSIZE) == x && (a.Y * CELLSIZE) == y) {
			flag = a.c;
			return true;
		}
	}

	for (Block b : movingblockstuck) {

		if (b.X == 0 || b.Y == 0) continue;

		if ((b.X * CELLSIZE) == x && (b.Y * CELLSIZE) == y) {
			flag = b.c;
			return true;
		}
	}

	return false;
}

Color getcolor(int x,int y) {
	return flag;
}

void Draw() {
	PAINTSTRUCT ps;
	HPEN hpen;
	HDC hdc = BeginPaint(window, &ps);
	hpen = CreatePen(PS_SOLID, 0, RGB(192, 192, 192));
	SelectObject(hdc, hpen);
	for (int i = 0; i <= (WIDTH * CELLSIZE); i += CELLSIZE) {
		for (int a = 0; a <= (HEIGHT * CELLSIZE); a += CELLSIZE) {
			if (isblock(i, a)) {
				SelectObject(hdc, CreateSolidBrush(RGB(flag.R,flag.G,flag.B)));
				Rectangle(hdc, i, a, i + CELLSIZE, a + CELLSIZE);
				SelectObject(hdc, GetStockObject(WHITE_BRUSH));
			}
			else {
				Rectangle(hdc, i, a, i + 20, a + 20);
			}
		}
	}
}