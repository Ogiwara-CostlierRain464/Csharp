#include "Block.h"

vector<Block> stopingblockstuck;

vector<Block> movingblockstuck;

int getfirstx() {
	return WIDTH / 2;
}

int getfirsty() {
	return HEIGHT / 2;
}

bool isfilled(int x, int y) {
	if (x <= 0 || x >= WIDTH) return true;
	if (x <= 0 || x >= HEIGHT) return true;

	for (Block b : stopingblockstuck) {
		if (b.X == x && b.Y == y) {
			return true;
		}
	}

	return false;
}

void spawn() {
	if (isfilled(getfirstx(),getfirsty())) {
		MessageBox(window, TEXT("ゲームオーバー"), TEXT("Gameover"), MB_ICONERROR);
		PostQuitMessage(0);
		return;
	}

	Block s;
	s.X = getfirstx();
	s.Y = getfirsty();
	Color c;
	c.R = 200; c.G = 200; c.B = 200;
	s.c = c;
	movingblockstuck.push_back(s);
	CallWMPAINT();
}

void unactivemovingblock() {
	for (Block b : movingblockstuck) {
		stopingblockstuck.push_back(b);
	}
	movingblockstuck.clear();
}

void goDown() {
	bool result = true;

	for (Block b : movingblockstuck) {
		if (!isfilled(b.X,b.Y + 1)) {
			result = false;
		}
	}

	if (result == true) {
		for (Block b : movingblockstuck) {
			b.Y += 1;
			CallWMPAINT();
		}
	}
	else {
		unactivemovingblock();
		spawn();
	}
}

void goLeft() {
	bool result = true;

	for (Block b : movingblockstuck) {
		if (!isfilled(b.X - 1, b.Y)) {
			result = false;
		}
	}

	if (result == true) {
		for (Block b : movingblockstuck) {
			b.X -= 1;
			CallWMPAINT();
		}
	}
}

void goRight() {
	bool result = true;

	for (Block b : movingblockstuck) {
		if (!isfilled(b.X + 1, b.Y)) {
			result = false;
		}
	}

	if (result == true) {
		for (Block b : movingblockstuck) {
			b.X += 1;
			CallWMPAINT();
		}
	}
}
