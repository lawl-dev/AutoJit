


extern "C" {
	__declspec(dllexport) bool TestFuncA(BYTE a, BOOLEAN b, SHORT c);
	__declspec(dllexport) bool TestFuncB(USHORT a, WORD b, INT c);
	__declspec(dllexport) bool TestFuncC(LONG a, BOOL b, UINT c);
	__declspec(dllexport) bool TestFuncD(ULONG a, DWORD b, INT64 c);
	__declspec(dllexport) bool TestFuncE(UINT64 a, void* b, HWND c);
	__declspec(dllexport) bool TestFuncF(HANDLE a, FLOAT b, double c);
	__declspec(dllexport) bool TestFuncG(INT_PTR a, LONG_PTR b, LRESULT c);
	__declspec(dllexport) bool TestFuncH(LPARAM a, UINT_PTR b, ULONG_PTR c);
	__declspec(dllexport) bool TestFuncI(DWORD_PTR a, WPARAM b, LPCSTR c);
	__declspec(dllexport) bool TestFuncJ(LPCWSTR a, LPVOID b);
	__declspec(dllexport) bool TestFuncK(HRESULT a, LONGLONG b, LARGE_INTEGER c);
	__declspec(dllexport) bool TestFuncL(ULONGLONG a, ULARGE_INTEGER b, SIZE_T c);


	__declspec(dllexport) void TestFuncRefA(BYTE* a, BOOLEAN* b, SHORT* c);
	__declspec(dllexport) void TestFuncRefB(USHORT* a, WORD* b, INT* c);
	__declspec(dllexport) void TestFuncRefC(LONG* a, bool* b, UINT* c);
	__declspec(dllexport) void TestFuncRefD(ULONG* a, DWORD* b, INT64* c);
	__declspec(dllexport) void TestFuncRefE(UINT64* a, HWND* c);
	__declspec(dllexport) void TestFuncRefF(FLOAT* b, double* c);
	__declspec(dllexport) void TestFuncRefG(INT_PTR* a, LONG_PTR* b, LRESULT* c);
	__declspec(dllexport) void TestFuncRefH(LPARAM* a, UINT_PTR* b, ULONG_PTR* c);
	__declspec(dllexport) void TestFuncRefI(DWORD_PTR* a, WPARAM* b);
}


