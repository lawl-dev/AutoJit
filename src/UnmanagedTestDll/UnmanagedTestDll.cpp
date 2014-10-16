// UnmanagedTestDll.cpp : Definiert die exportierten Funktionen für die DLL-Anwendung.
//

#include <string.h>
#include "Extern.h"
#include "stdafx.h"




extern "C"
{
	__declspec(dllexport) bool TestFuncA(BYTE a, BOOLEAN b, SHORT c){
		return a == 2 && b > 0 && c == 1;
	}
	__declspec(dllexport) bool  TestFuncB(USHORT a, WORD b, INT c){
		return a == 3 && b == 4 && c == 5;
	}

	__declspec(dllexport) bool  TestFuncC(LONG a, BOOL b, UINT c){
		return a == 6 && b && c == 7;
	}
	__declspec(dllexport) bool  TestFuncD(ULONG a, DWORD b, INT64 c){
		return a == 8 && b == 9 && c == 10;
	}
	__declspec(dllexport) bool  TestFuncE(UINT64 a, void* b, HWND c){
		LPDWORD pid = NULL;
		GetWindowThreadProcessId(c, pid);
		return a == 11 && *((int *)b) == 12 && pid > 0;
	}
	__declspec(dllexport) bool TestFuncF(HANDLE a, FLOAT b, double c){
		return a > 0 && b == 0.1f && c == 0.1;
	}
	__declspec(dllexport) bool  TestFuncG(INT_PTR a, LONG_PTR b, LRESULT c){
		return a == 13 && b == 14 && c == 15;
	}
	__declspec(dllexport) bool  TestFuncH(LPARAM a, UINT_PTR b, ULONG_PTR c){
		return a == 16 && b == 17 && c == 18;
	}
	__declspec(dllexport) bool  TestFuncI(DWORD_PTR a, WPARAM b, LPCSTR c){
		return a == 19 && b == 20 && c == "0";
	}
	__declspec(dllexport) 	bool  TestFuncJ(LPCWSTR a, LPVOID b){
		wchar_t* ext = L"LOL";
		return wcsstr(a, ext) && *((int *)b) == 21;
	}
	__declspec(dllexport) bool  TestFuncK(HRESULT a, LONGLONG b, LARGE_INTEGER c){
		return a == 22 && b == 23 && c.HighPart == 24 && c.LowPart == 25;
	}
	__declspec(dllexport) bool  TestFuncL(ULONGLONG a, ULARGE_INTEGER b, SIZE_T c){
		return a == 26 && b.HighPart == 27 && b.LowPart == 28 && c == 29;
	}





	

	__declspec(dllexport) void TestFuncRefA(BYTE* a, BOOLEAN* b, SHORT* c){
		(*a)++;
		(*b) = (*b) ? 0 : 1;
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefB(USHORT* a, WORD* b, INT* c){
		(*a)++;
		(*b)++;
		(*c)++;
	}

	__declspec(dllexport) void  TestFuncRefC(LONG* a, bool* b, UINT* c){
		(*a)++;
		(*b) = !(*b);
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefD(ULONG* a, DWORD* b, INT64* c){
		(*a)++;
		(*b)++;
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefE(UINT64* a, INT* c){
		(*a)++;
		(*c)++;
	}
	__declspec(dllexport) void TestFuncRefF(FLOAT* b, double* c){
		(*b)++;
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefG(INT_PTR* a, LONG_PTR* b, LRESULT* c){
		(*a)++;
		(*b)++;
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefH(LPARAM* a, UINT_PTR* b, ULONG_PTR* c){
		(*a)++;
		(*b)++;
		(*c)++;
	}
	__declspec(dllexport) void  TestFuncRefI(DWORD_PTR* a, WPARAM* b){
		(*a)++;
		(*b)++;
	}
}
