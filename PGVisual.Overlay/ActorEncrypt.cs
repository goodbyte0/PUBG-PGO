using System;

namespace PGVisual.Overlay
{
	// Token: 0x0200000C RID: 12
	internal class ActorEncrypt
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public unsafe ulong decryptPtr(ulong encryptedPtr)
		{
			EncryptedBlock encryptedBlock = Program.Client.ReadMemory<EncryptedBlock>(encryptedPtr);
			ulong num = (ulong)(&encryptedBlock.data.FixedElementField)[this.decrypt_p21((&encryptedBlock.data.FixedElementField)[44], (&encryptedBlock.data.FixedElementField)[45]) * 8UL / 8UL];
			ulong num2 = this.decrypt_p22((&encryptedBlock.data.FixedElementField)[46], (&encryptedBlock.data.FixedElementField)[47]);
			return num ^ num2;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000385E File Offset: 0x00001A5E
		private int ubyte0(int i)
		{
			return i & 255;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00003867 File Offset: 0x00001A67
		private int ubyte1(int i)
		{
			return i >> 8 & 255;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00003872 File Offset: 0x00001A72
		private int ubyte2(int i)
		{
			return i >> 16 & 255;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000387E File Offset: 0x00001A7E
		private int ubyte3(int i)
		{
			return i >> 24 & 255;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000388A File Offset: 0x00001A8A
		private int word0(long l)
		{
			return (int)(l & 65535L);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00003898 File Offset: 0x00001A98
		public void init_decryption()
		{
			this.dyn_table_r.generate(this.decrypt_rotator, this.decrypt_offsets);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007E44 File Offset: 0x00006044
		private ushort decrypt_p1(long left, long right)
		{
			ushort num = (ushort)(right ^ ~left ^ 3365L);
			for (int i = 0; i < 5; i++)
			{
				uint num2 = (uint)(num ^ 17408) >> 8;
				int num3 = (int)((num ^ 85) & 255);
				num = (ushort)((int)this.byte_table[(int)this.byte_table[(int)num2]] | (int)this.byte_table[(int)this.byte_table[num3]] << 8);
			}
			return num;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007EA8 File Offset: 0x000060A8
		private ulong decrypt_p20(long left, long right, ulong a, ulong b, ulong c, ulong d_idx)
		{
			ushort num = this.decrypt_p1(left, right);
			int idx = this.ubyte3((int)((ulong)num ^ b));
			ulong num2 = (ulong)((long)this.ubyte1((int)num) ^ (long)(c + 512UL));
			int idx2 = this.ubyte0((int)((ulong)num ^ a)) + 768;
			ulong num3 = d_idx + 256UL;
			uint num4 = this.dyn_table_r.get((uint)idx2);
			uint num5 = this.dyn_table_r.get((uint)idx);
			uint num6 = this.dyn_table_r.get((uint)num2);
			uint num7 = this.dyn_table_r.get((uint)num3);
			return (ulong)(~(num4 ^ num5 ^ num6 ^ num7) % 43u);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000038B1 File Offset: 0x00001AB1
		private ulong decrypt_p21(long left, long right)
		{
			return this.decrypt_p20(left, right, 188UL, 3618593468UL, 90UL, 175UL);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000038DF File Offset: 0x00001ADF
		private ulong decrypt_p22(long left, long right)
		{
			return this.decrypt_p20(left, right, 12UL, 1558700812UL, 227UL, 231UL);
		}

		// Token: 0x04000022 RID: 34
		private byte[] byte_table = new byte[]
		{
			15,
			14,
			13,
			12,
			11,
			10,
			9,
			8,
			7,
			6,
			5,
			4,
			3,
			2,
			1,
			0,
			31,
			30,
			29,
			28,
			27,
			26,
			25,
			24,
			23,
			22,
			21,
			20,
			19,
			18,
			17,
			16,
			47,
			46,
			45,
			44,
			43,
			42,
			41,
			40,
			39,
			38,
			37,
			36,
			35,
			34,
			33,
			32,
			63,
			62,
			61,
			60,
			59,
			58,
			57,
			56,
			55,
			54,
			53,
			52,
			51,
			50,
			49,
			48,
			79,
			78,
			77,
			76,
			75,
			74,
			73,
			72,
			71,
			70,
			69,
			68,
			67,
			66,
			65,
			64,
			95,
			94,
			93,
			92,
			91,
			90,
			89,
			88,
			87,
			86,
			85,
			84,
			83,
			82,
			81,
			80,
			111,
			110,
			109,
			108,
			107,
			106,
			105,
			104,
			103,
			102,
			101,
			100,
			99,
			98,
			97,
			96,
			127,
			126,
			125,
			124,
			123,
			122,
			121,
			120,
			119,
			118,
			117,
			116,
			115,
			114,
			113,
			112,
			143,
			142,
			141,
			140,
			139,
			138,
			137,
			136,
			135,
			134,
			133,
			132,
			131,
			130,
			129,
			128,
			159,
			158,
			157,
			156,
			155,
			154,
			153,
			152,
			151,
			150,
			149,
			148,
			147,
			146,
			145,
			144,
			175,
			174,
			173,
			172,
			171,
			170,
			169,
			168,
			167,
			166,
			165,
			164,
			163,
			162,
			161,
			160,
			191,
			190,
			189,
			188,
			187,
			186,
			185,
			184,
			183,
			182,
			181,
			180,
			179,
			178,
			177,
			176,
			207,
			206,
			205,
			204,
			203,
			202,
			201,
			200,
			199,
			198,
			197,
			196,
			195,
			194,
			193,
			192,
			223,
			222,
			221,
			220,
			219,
			218,
			217,
			216,
			215,
			214,
			213,
			212,
			211,
			210,
			209,
			208,
			239,
			238,
			237,
			236,
			235,
			234,
			233,
			232,
			231,
			230,
			229,
			228,
			227,
			226,
			225,
			224,
			byte.MaxValue,
			254,
			253,
			252,
			251,
			250,
			249,
			248,
			247,
			246,
			245,
			244,
			243,
			242,
			241,
			240
		};

		// Token: 0x04000023 RID: 35
		private int decrypt_rotator = 866889406;

		// Token: 0x04000024 RID: 36
		private int[] decrypt_offsets = new int[]
		{
			-257,
			-1,
			255,
			511,
			767,
			1023,
			1279
		};

		// Token: 0x04000025 RID: 37
		private DynTable dyn_table_r = new DynTable();
	}
}
