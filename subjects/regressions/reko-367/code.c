// code.c
// Generated by decompiling code.bin
// using Reko decompiler version 0.6.1.0.

#include "code.h"

void fn80000000(word32 d2)
{
	fn800003CC(d2);
	return;
}

real80 fn80000132(word32 d2, real96 rArg04, real96 rArg10)
{
	word32 dwLoc14_17 = 0x00;
	while ((real96) (real80) dwLoc14_17 < rArg10)
		dwLoc14_17 = dwLoc14_17 + 0x01;
	return (real80) DPB(rLoc24, dwLoc10, 0);
}

real80 fn8000018E(word32 d2, real96 rArg04)
{
	int32 dwLoc14_19 = 0x01;
	while ((real96) (real80) dwLoc14_19 <= rArg04)
		dwLoc14_19 = dwLoc14_19 + 0x01;
	return (real80) DPB(rLoc24, dwLoc10, 0);
}

real80 fn800001F2(word32 d2, real96 rArg04)
{
	int32 dwLoc20_28 = 0x03;
	while (100 - dwLoc20_28 >= 0x00)
	{
		fn80000132(d2, (real96) (real80) rArg04, (real96) (real80) dwLoc20_28);
		real96 v19_67 = (real96) (real80) dwLoc20_28;
		fn8000018E(d2, v19_67);
		rLoc3C = v19_67;
		dwLoc20_28 = dwLoc20_28 + 0x02;
	}
	return (real80) DPB(rLoc3C, dwLoc10, 0);
}

real80 fn800002AE(word32 d2, real96 rArg04, Eq_70 & fp2Out)
{
	int32 dwLoc20_27 = 0x02;
	while (100 - dwLoc20_27 >= 0x00)
	{
		fn80000132(d2, (real96) (real80) rArg04, (real96) (real80) dwLoc20_27);
		real96 v19_67 = (real96) (real80) dwLoc20_27;
		fn8000018E(d2, v19_67);
		rLoc3C = v19_67;
		dwLoc20_27 = dwLoc20_27 + 0x02;
	}
	real80 fp2_118;
	*fp2Out = fp2;
	return (real80) DPB(rLoc3C, dwLoc10, 0);
}

void fn8000036C(word32 d2, real96 rArg04)
{
	fn800001F2(d2, (real96) (real80) rArg04);
	real80 fp2_32;
	fn800002AE(d2, (real96) (real80) rArg04, out fp2_32);
	return;
}

void fn800003CC(word32 d2)
{
	real96 v6_10 = (real96) (real80) *(real96 *) 0x80000538;
	fn80000132(d2, (real96) (real80) v6_10, (real96) (real80) v6_10);
	fn8000018E(d2, (real96) (real80) v6_10);
	fn800001F2(d2, (real96) (real80) v6_10);
	real80 fp2_50;
	fn800002AE(d2, (real96) (real80) v6_10, out fp2_50);
	fn8000036C(d2, (real96) (real80) v6_10);
	return;
}

