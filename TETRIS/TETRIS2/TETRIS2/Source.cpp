#include "main.h"

#define WINDOWNAME TEXT("TETRIS")

HWND window;

DWORD WINAPI moveblockthread(LPVOID v) {
	while (true)
	{
		goDown();
		Sleep(1000);
	}
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wp, LPARAM lp) {

	DWORD dwID;

	switch (msg) {
	case WM_CREATE:
		spawn();
		CreateThread(NULL, 0, moveblockthread, (LPVOID)hWnd, 0, &dwID);
		return 0;
	case WM_KEYDOWN:
		switch (wp)
		{
		case VK_LEFT:
			break;
		case VK_DOWN:
			break;
		case VK_RIGHT:
			break;
		}
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	case WM_PAINT:
		Draw();
		return 0;
	}
	return DefWindowProc(hWnd, msg, wp, lp);
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	PSTR lpCmdLine, int nCmdShow) {
	HWND hWnd;
	MSG msg;
	WNDCLASS winc;

	winc.style = CS_HREDRAW | CS_VREDRAW;
	winc.lpfnWndProc = WndProc;
	winc.cbClsExtra = winc.cbWndExtra = 0;
	winc.hInstance = hInstance;
	winc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	winc.hCursor = LoadCursor(NULL, IDC_ARROW);
	winc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	winc.lpszMenuName = NULL;
	winc.lpszClassName = WINDOWNAME;

	RegisterClass(&winc);

	hWnd = CreateWindow(
		WINDOWNAME, WINDOWNAME,
		WS_OVERLAPPEDWINDOW | WS_VISIBLE,
		400, 400,
		600, 800,
		NULL, NULL, hInstance, NULL
		);

	window = hWnd;

	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}