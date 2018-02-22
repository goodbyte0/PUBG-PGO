using System;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Dirichlet.Numerics
{
	// Token: 0x02000003 RID: 3
	public struct UInt128 : IComparable<UInt128>, IEquatable<UInt128>, IFormattable, IComparable
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002A81 File Offset: 0x00000C81
		public static UInt128 MinValue
		{
			get
			{
				return UInt128.zero;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002A88 File Offset: 0x00000C88
		public static UInt128 MaxValue
		{
			get
			{
				return UInt128.maxValue;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002A81 File Offset: 0x00000C81
		public static UInt128 Zero
		{
			get
			{
				return UInt128.zero;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002A8F File Offset: 0x00000C8F
		public static UInt128 One
		{
			get
			{
				return UInt128.one;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000529C File Offset: 0x0000349C
		public static UInt128 Parse(string value)
		{
			UInt128 result;
			if (!UInt128.TryParse(value, out result))
			{
				throw new FormatException();
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002A96 File Offset: 0x00000C96
		public static bool TryParse(string value, out UInt128 result)
		{
			return UInt128.TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000052BC File Offset: 0x000034BC
		public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out UInt128 result)
		{
			BigInteger a;
			if (!BigInteger.TryParse(value, style, provider, out a))
			{
				result = UInt128.Zero;
				return false;
			}
			UInt128.Create(out result, a);
			return true;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public UInt128(long value)
		{
			UInt128.Create(out this, value);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002AAE File Offset: 0x00000CAE
		public UInt128(ulong value)
		{
			UInt128.Create(out this, value);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public UInt128(decimal value)
		{
			UInt128.Create(out this, value);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public UInt128(double value)
		{
			UInt128.Create(out this, value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public UInt128(BigInteger value)
		{
			UInt128.Create(out this, value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public static void Create(out UInt128 c, uint r0, uint r1, uint r2, uint r3)
		{
			c.s0 = ((ulong)r1 << 32 | (ulong)r0);
			c.s1 = ((ulong)r3 << 32 | (ulong)r2);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public static void Create(out UInt128 c, ulong s0, ulong s1)
		{
			c.s0 = s0;
			c.s1 = s1;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002B01 File Offset: 0x00000D01
		public static void Create(out UInt128 c, long a)
		{
			c.s0 = (ulong)a;
			c.s1 = ((a < 0L) ? ulong.MaxValue : 0UL);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002B30 File Offset: 0x00000D30
		public static void Create(out UInt128 c, ulong a)
		{
			c.s0 = a;
			c.s1 = 0UL;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000052EC File Offset: 0x000034EC
		public static void Create(out UInt128 c, decimal a)
		{
			int[] bits = decimal.GetBits(decimal.Truncate(a));
			UInt128.Create(out c, (uint)bits[0], (uint)bits[1], (uint)bits[2], 0u);
			if (a < 0m)
			{
				UInt128.Negate(ref c);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005328 File Offset: 0x00003528
		public static void Create(out UInt128 c, BigInteger a)
		{
			int sign = a.Sign;
			if (sign == -1)
			{
				a = -a;
			}
			c.s0 = (ulong)(a & ulong.MaxValue);
			c.s1 = (ulong)(a >> 64);
			if (sign == -1)
			{
				UInt128.Negate(ref c);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005384 File Offset: 0x00003584
		public static void Create(out UInt128 c, double a)
		{
			bool flag = false;
			if (a < 0.0)
			{
				flag = true;
				a = -a;
			}
			if (a <= 1.8446744073709552E+19)
			{
				c.s0 = (ulong)a;
				c.s1 = 0UL;
			}
			else
			{
				int num = Math.Max((int)Math.Ceiling(Math.Log(a, 2.0)) - 63, 0);
				c.s0 = (ulong)(a / Math.Pow(2.0, (double)num));
				c.s1 = 0UL;
				UInt128.LeftShift(ref c, num);
			}
			if (flag)
			{
				UInt128.Negate(ref c);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002B48 File Offset: 0x00000D48
		private uint r0
		{
			get
			{
				return (uint)this.s0;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002B51 File Offset: 0x00000D51
		private uint r1
		{
			get
			{
				return (uint)(this.s0 >> 32);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00002B5D File Offset: 0x00000D5D
		private uint r2
		{
			get
			{
				return (uint)this.s1;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002B66 File Offset: 0x00000D66
		private uint r3
		{
			get
			{
				return (uint)(this.s1 >> 32);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002B72 File Offset: 0x00000D72
		public ulong S0
		{
			get
			{
				return this.s0;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00002B7A File Offset: 0x00000D7A
		public ulong S1
		{
			get
			{
				return this.s1;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002B82 File Offset: 0x00000D82
		public bool IsZero
		{
			get
			{
				return (this.s0 | this.s1) == 0UL;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002B9C File Offset: 0x00000D9C
		public bool IsOne
		{
			get
			{
				return (this.s1 ^ this.s0) == 1UL;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005424 File Offset: 0x00003624
		public bool IsPowerOfTwo
		{
			get
			{
				return (this & this - 1UL).IsZero;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00002BB6 File Offset: 0x00000DB6
		public bool IsEven
		{
			get
			{
				return (this.s0 & 1UL) == 0UL;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public int Sign
		{
			get
			{
				if (!this.IsZero)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005458 File Offset: 0x00003658
		public override string ToString()
		{
			return this.ToString();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005480 File Offset: 0x00003680
		public string ToString(string format)
		{
			return this.ToString(format);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public string ToString(IFormatProvider provider)
		{
			return this.ToString(null, provider);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000054A4 File Offset: 0x000036A4
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, provider);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000054C8 File Offset: 0x000036C8
		public static explicit operator UInt128(double a)
		{
			UInt128 result;
			UInt128.Create(out result, a);
			return result;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000054E0 File Offset: 0x000036E0
		public static explicit operator UInt128(sbyte a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)a);
			return result;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000054F8 File Offset: 0x000036F8
		public static implicit operator UInt128(byte a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)((ulong)a));
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000054E0 File Offset: 0x000036E0
		public static explicit operator UInt128(short a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)a);
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000054F8 File Offset: 0x000036F8
		public static implicit operator UInt128(ushort a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)((ulong)a));
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000054E0 File Offset: 0x000036E0
		public static explicit operator UInt128(int a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)a);
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000054F8 File Offset: 0x000036F8
		public static implicit operator UInt128(uint a)
		{
			UInt128 result;
			UInt128.Create(out result, (long)((ulong)a));
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005510 File Offset: 0x00003710
		public static explicit operator UInt128(long a)
		{
			UInt128 result;
			UInt128.Create(out result, a);
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005528 File Offset: 0x00003728
		public static implicit operator UInt128(ulong a)
		{
			UInt128 result;
			UInt128.Create(out result, a);
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005540 File Offset: 0x00003740
		public static explicit operator UInt128(decimal a)
		{
			UInt128 result;
			UInt128.Create(out result, a);
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005558 File Offset: 0x00003758
		public static explicit operator UInt128(BigInteger a)
		{
			UInt128 result;
			UInt128.Create(out result, a);
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00002BEA File Offset: 0x00000DEA
		public static explicit operator float(UInt128 a)
		{
			return UInt128.ConvertToFloat(ref a);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public static explicit operator double(UInt128 a)
		{
			return UInt128.ConvertToDouble(ref a);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00002BFC File Offset: 0x00000DFC
		public static float ConvertToFloat(ref UInt128 a)
		{
			if (a.s1 == 0UL)
			{
				return a.s0;
			}
			return a.s1 * 1.84467441E+19f + a.s0;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00002C26 File Offset: 0x00000E26
		public static double ConvertToDouble(ref UInt128 a)
		{
			if (a.s1 == 0UL)
			{
				return a.s0;
			}
			return a.s1 * 1.8446744073709552E+19 + a.s0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00002C54 File Offset: 0x00000E54
		public static explicit operator sbyte(UInt128 a)
		{
			return (sbyte)a.s0;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002C5D File Offset: 0x00000E5D
		public static explicit operator byte(UInt128 a)
		{
			return (byte)a.s0;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00002C66 File Offset: 0x00000E66
		public static explicit operator short(UInt128 a)
		{
			return (short)a.s0;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002C6F File Offset: 0x00000E6F
		public static explicit operator ushort(UInt128 a)
		{
			return (ushort)a.s0;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00002C78 File Offset: 0x00000E78
		public static explicit operator int(UInt128 a)
		{
			return (int)a.s0;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002B48 File Offset: 0x00000D48
		public static explicit operator uint(UInt128 a)
		{
			return (uint)a.s0;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002B72 File Offset: 0x00000D72
		public static explicit operator long(UInt128 a)
		{
			return (long)a.s0;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002B72 File Offset: 0x00000D72
		public static explicit operator ulong(UInt128 a)
		{
			return a.s0;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005570 File Offset: 0x00003770
		public static explicit operator decimal(UInt128 a)
		{
			if (a.s1 == 0UL)
			{
				return a.s0;
			}
			int num = Math.Max(0, 32 - UInt128.GetBitLength(a.s1));
			UInt128 @uint;
			UInt128.RightShift(out @uint, ref a, num);
			return new decimal((int)a.r0, (int)a.r1, (int)a.r2, false, (byte)num);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002C81 File Offset: 0x00000E81
		public static implicit operator BigInteger(UInt128 a)
		{
			if (a.s1 == 0UL)
			{
				return a.s0;
			}
			return a.s1 << 64 | a.s0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000055D0 File Offset: 0x000037D0
		public static UInt128 operator <<(UInt128 a, int b)
		{
			UInt128 result;
			UInt128.LeftShift(out result, ref a, b);
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000055E8 File Offset: 0x000037E8
		public static UInt128 operator >>(UInt128 a, int b)
		{
			UInt128 result;
			UInt128.RightShift(out result, ref a, b);
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005600 File Offset: 0x00003800
		public static UInt128 operator &(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.And(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public static uint operator &(UInt128 a, uint b)
		{
			return (uint)a.s0 & b;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static uint operator &(uint a, UInt128 b)
		{
			return a & (uint)b.s0;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00002CCF File Offset: 0x00000ECF
		public static ulong operator &(UInt128 a, ulong b)
		{
			return a.s0 & b;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002CD9 File Offset: 0x00000ED9
		public static ulong operator &(ulong a, UInt128 b)
		{
			return a & b.s0;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000561C File Offset: 0x0000381C
		public static UInt128 operator |(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Or(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005638 File Offset: 0x00003838
		public static UInt128 operator ^(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.ExclusiveOr(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005654 File Offset: 0x00003854
		public static UInt128 operator ~(UInt128 a)
		{
			UInt128 result;
			UInt128.Not(out result, ref a);
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000566C File Offset: 0x0000386C
		public static UInt128 operator +(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Add(out result, ref a, ref b);
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005688 File Offset: 0x00003888
		public static UInt128 operator +(UInt128 a, ulong b)
		{
			UInt128 result;
			UInt128.Add(out result, ref a, b);
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000056A0 File Offset: 0x000038A0
		public static UInt128 operator +(ulong a, UInt128 b)
		{
			UInt128 result;
			UInt128.Add(out result, ref b, a);
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000056B8 File Offset: 0x000038B8
		public static UInt128 operator ++(UInt128 a)
		{
			UInt128 result;
			UInt128.Add(out result, ref a, 1UL);
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056D8 File Offset: 0x000038D8
		public static UInt128 operator -(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Subtract(out result, ref a, ref b);
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000056F4 File Offset: 0x000038F4
		public static UInt128 operator -(UInt128 a, ulong b)
		{
			UInt128 result;
			UInt128.Subtract(out result, ref a, b);
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000570C File Offset: 0x0000390C
		public static UInt128 operator -(ulong a, UInt128 b)
		{
			UInt128 result;
			UInt128.Subtract(out result, a, ref b);
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005724 File Offset: 0x00003924
		public static UInt128 operator --(UInt128 a)
		{
			UInt128 result;
			UInt128.Subtract(out result, ref a, 1UL);
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002311 File Offset: 0x00000511
		public static UInt128 operator +(UInt128 a)
		{
			return a;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005744 File Offset: 0x00003944
		public static UInt128 operator *(UInt128 a, uint b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000575C File Offset: 0x0000395C
		public static UInt128 operator *(uint a, UInt128 b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005774 File Offset: 0x00003974
		public static UInt128 operator *(UInt128 a, ulong b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000578C File Offset: 0x0000398C
		public static UInt128 operator *(ulong a, UInt128 b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000057A4 File Offset: 0x000039A4
		public static UInt128 operator *(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000057C0 File Offset: 0x000039C0
		public static UInt128 operator /(UInt128 a, ulong b)
		{
			UInt128 result;
			UInt128.Divide(out result, ref a, b);
			return result;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000057D8 File Offset: 0x000039D8
		public static UInt128 operator /(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Divide(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public static ulong operator %(UInt128 a, uint b)
		{
			return (ulong)UInt128.Remainder(ref a, b);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002CEE File Offset: 0x00000EEE
		public static ulong operator %(UInt128 a, ulong b)
		{
			return UInt128.Remainder(ref a, b);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000057F4 File Offset: 0x000039F4
		public static UInt128 operator %(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Remainder(out result, ref a, ref b);
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public static bool operator <(UInt128 a, UInt128 b)
		{
			return UInt128.LessThan(ref a, ref b);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00002D03 File Offset: 0x00000F03
		public static bool operator <(UInt128 a, int b)
		{
			return UInt128.LessThan(ref a, (long)b);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00002D0E File Offset: 0x00000F0E
		public static bool operator <(int a, UInt128 b)
		{
			return UInt128.LessThan((long)a, ref b);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002D19 File Offset: 0x00000F19
		public static bool operator <(UInt128 a, uint b)
		{
			return UInt128.LessThan(ref a, (long)((ulong)b));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002D24 File Offset: 0x00000F24
		public static bool operator <(uint a, UInt128 b)
		{
			return UInt128.LessThan((long)((ulong)a), ref b);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002D2F File Offset: 0x00000F2F
		public static bool operator <(UInt128 a, long b)
		{
			return UInt128.LessThan(ref a, b);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002D39 File Offset: 0x00000F39
		public static bool operator <(long a, UInt128 b)
		{
			return UInt128.LessThan(a, ref b);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00002D43 File Offset: 0x00000F43
		public static bool operator <(UInt128 a, ulong b)
		{
			return UInt128.LessThan(ref a, b);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00002D4D File Offset: 0x00000F4D
		public static bool operator <(ulong a, UInt128 b)
		{
			return UInt128.LessThan(a, ref b);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002D57 File Offset: 0x00000F57
		public static bool operator <=(UInt128 a, UInt128 b)
		{
			return !UInt128.LessThan(ref b, ref a);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002D65 File Offset: 0x00000F65
		public static bool operator <=(UInt128 a, int b)
		{
			return !UInt128.LessThan((long)b, ref a);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002D73 File Offset: 0x00000F73
		public static bool operator <=(int a, UInt128 b)
		{
			return !UInt128.LessThan(ref b, (long)a);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00002D81 File Offset: 0x00000F81
		public static bool operator <=(UInt128 a, uint b)
		{
			return !UInt128.LessThan((long)((ulong)b), ref a);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00002D8F File Offset: 0x00000F8F
		public static bool operator <=(uint a, UInt128 b)
		{
			return !UInt128.LessThan(ref b, (long)((ulong)a));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00002D9D File Offset: 0x00000F9D
		public static bool operator <=(UInt128 a, long b)
		{
			return !UInt128.LessThan(b, ref a);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002DAA File Offset: 0x00000FAA
		public static bool operator <=(long a, UInt128 b)
		{
			return !UInt128.LessThan(ref b, a);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public static bool operator <=(UInt128 a, ulong b)
		{
			return !UInt128.LessThan(b, ref a);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static bool operator <=(ulong a, UInt128 b)
		{
			return !UInt128.LessThan(ref b, a);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public static bool operator >(UInt128 a, UInt128 b)
		{
			return UInt128.LessThan(ref b, ref a);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002DDC File Offset: 0x00000FDC
		public static bool operator >(UInt128 a, int b)
		{
			return UInt128.LessThan((long)b, ref a);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public static bool operator >(int a, UInt128 b)
		{
			return UInt128.LessThan(ref b, (long)a);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public static bool operator >(UInt128 a, uint b)
		{
			return UInt128.LessThan((long)((ulong)b), ref a);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00002DFD File Offset: 0x00000FFD
		public static bool operator >(uint a, UInt128 b)
		{
			return UInt128.LessThan(ref b, (long)((ulong)a));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00002E08 File Offset: 0x00001008
		public static bool operator >(UInt128 a, long b)
		{
			return UInt128.LessThan(b, ref a);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002E12 File Offset: 0x00001012
		public static bool operator >(long a, UInt128 b)
		{
			return UInt128.LessThan(ref b, a);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00002E1C File Offset: 0x0000101C
		public static bool operator >(UInt128 a, ulong b)
		{
			return UInt128.LessThan(b, ref a);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002E26 File Offset: 0x00001026
		public static bool operator >(ulong a, UInt128 b)
		{
			return UInt128.LessThan(ref b, a);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002E30 File Offset: 0x00001030
		public static bool operator >=(UInt128 a, UInt128 b)
		{
			return !UInt128.LessThan(ref a, ref b);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002E3E File Offset: 0x0000103E
		public static bool operator >=(UInt128 a, int b)
		{
			return !UInt128.LessThan(ref a, (long)b);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00002E4C File Offset: 0x0000104C
		public static bool operator >=(int a, UInt128 b)
		{
			return !UInt128.LessThan((long)a, ref b);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002E5A File Offset: 0x0000105A
		public static bool operator >=(UInt128 a, uint b)
		{
			return !UInt128.LessThan(ref a, (long)((ulong)b));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002E68 File Offset: 0x00001068
		public static bool operator >=(uint a, UInt128 b)
		{
			return !UInt128.LessThan((long)((ulong)a), ref b);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002E76 File Offset: 0x00001076
		public static bool operator >=(UInt128 a, long b)
		{
			return !UInt128.LessThan(ref a, b);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002E83 File Offset: 0x00001083
		public static bool operator >=(long a, UInt128 b)
		{
			return !UInt128.LessThan(a, ref b);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002E90 File Offset: 0x00001090
		public static bool operator >=(UInt128 a, ulong b)
		{
			return !UInt128.LessThan(ref a, b);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002E9D File Offset: 0x0000109D
		public static bool operator >=(ulong a, UInt128 b)
		{
			return !UInt128.LessThan(a, ref b);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002EAA File Offset: 0x000010AA
		public static bool operator ==(UInt128 a, UInt128 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00002EB4 File Offset: 0x000010B4
		public static bool operator ==(UInt128 a, int b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00002EBE File Offset: 0x000010BE
		public static bool operator ==(int a, UInt128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00002EC8 File Offset: 0x000010C8
		public static bool operator ==(UInt128 a, uint b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00002ED2 File Offset: 0x000010D2
		public static bool operator ==(uint a, UInt128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002EDC File Offset: 0x000010DC
		public static bool operator ==(UInt128 a, long b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002EE6 File Offset: 0x000010E6
		public static bool operator ==(long a, UInt128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static bool operator ==(UInt128 a, ulong b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00002EFA File Offset: 0x000010FA
		public static bool operator ==(ulong a, UInt128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00002F04 File Offset: 0x00001104
		public static bool operator !=(UInt128 a, UInt128 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002F11 File Offset: 0x00001111
		public static bool operator !=(UInt128 a, int b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00002F1E File Offset: 0x0000111E
		public static bool operator !=(int a, UInt128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00002F2B File Offset: 0x0000112B
		public static bool operator !=(UInt128 a, uint b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00002F38 File Offset: 0x00001138
		public static bool operator !=(uint a, UInt128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00002F45 File Offset: 0x00001145
		public static bool operator !=(UInt128 a, long b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002F52 File Offset: 0x00001152
		public static bool operator !=(long a, UInt128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00002F5F File Offset: 0x0000115F
		public static bool operator !=(UInt128 a, ulong b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00002F6C File Offset: 0x0000116C
		public static bool operator !=(ulong a, UInt128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00002F79 File Offset: 0x00001179
		public int CompareTo(UInt128 other)
		{
			if (this.s1 != other.s1)
			{
				return this.s1.CompareTo(other.s1);
			}
			return this.s0.CompareTo(other.s0);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00002FAC File Offset: 0x000011AC
		public int CompareTo(int other)
		{
			if (this.s1 == 0UL && other >= 0)
			{
				return this.s0.CompareTo((ulong)((long)other));
			}
			return 1;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00002FC9 File Offset: 0x000011C9
		public int CompareTo(uint other)
		{
			if (this.s1 != 0UL)
			{
				return 1;
			}
			return this.s0.CompareTo((ulong)other);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00002FE2 File Offset: 0x000011E2
		public int CompareTo(long other)
		{
			if (this.s1 == 0UL && other >= 0L)
			{
				return this.s0.CompareTo((ulong)other);
			}
			return 1;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003006 File Offset: 0x00001206
		public int CompareTo(ulong other)
		{
			if (this.s1 != 0UL)
			{
				return 1;
			}
			return this.s0.CompareTo(other);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000301E File Offset: 0x0000121E
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is UInt128))
			{
				throw new ArgumentException();
			}
			return this.CompareTo((UInt128)obj);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000303F File Offset: 0x0000123F
		private static bool LessThan(ref UInt128 a, long b)
		{
			return b >= 0L && a.s1 == 0UL && a.s0 < (ulong)b;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003060 File Offset: 0x00001260
		private static bool LessThan(long a, ref UInt128 b)
		{
			return a < 0L || b.s1 != 0UL || a < (long)b.s0;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003081 File Offset: 0x00001281
		private static bool LessThan(ref UInt128 a, ulong b)
		{
			return a.s1 == 0UL && a.s0 < b;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003096 File Offset: 0x00001296
		private static bool LessThan(ulong a, ref UInt128 b)
		{
			return b.s1 != 0UL || a < b.s0;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000030AB File Offset: 0x000012AB
		private static bool LessThan(ref UInt128 a, ref UInt128 b)
		{
			if (a.s1 != b.s1)
			{
				return a.s1 < b.s1;
			}
			return a.s0 < b.s0;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000030D8 File Offset: 0x000012D8
		public static bool Equals(ref UInt128 a, ref UInt128 b)
		{
			return a.s0 == b.s0 && a.s1 == b.s1;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000030D8 File Offset: 0x000012D8
		public bool Equals(UInt128 other)
		{
			return this.s0 == other.s0 && this.s1 == other.s1;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000030F8 File Offset: 0x000012F8
		public bool Equals(int other)
		{
			return other >= 0 && this.s0 == (ulong)other && this.s1 == 0UL;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000311B File Offset: 0x0000131B
		public bool Equals(uint other)
		{
			return this.s0 == (ulong)other && this.s1 == 0UL;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000313A File Offset: 0x0000133A
		public bool Equals(long other)
		{
			return other >= 0L && this.s0 == (ulong)other && this.s1 == 0UL;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003164 File Offset: 0x00001364
		public bool Equals(ulong other)
		{
			return this.s0 == other && this.s1 == 0UL;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003182 File Offset: 0x00001382
		public override bool Equals(object obj)
		{
			return obj is UInt128 && this.Equals((UInt128)obj);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000319A File Offset: 0x0000139A
		public override int GetHashCode()
		{
			return this.s0.GetHashCode() ^ this.s1.GetHashCode();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000031B3 File Offset: 0x000013B3
		public static void Multiply(out UInt128 c, ulong a, ulong b)
		{
			UInt128.Multiply64(out c, a, b);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000031BD File Offset: 0x000013BD
		public static void Multiply(out UInt128 c, ref UInt128 a, uint b)
		{
			if (a.s1 == 0UL)
			{
				UInt128.Multiply64(out c, a.s0, b);
				return;
			}
			UInt128.Multiply128(out c, ref a, b);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000031DD File Offset: 0x000013DD
		public static void Multiply(out UInt128 c, ref UInt128 a, ulong b)
		{
			if (a.s1 == 0UL)
			{
				UInt128.Multiply64(out c, a.s0, b);
				return;
			}
			UInt128.Multiply128(out c, ref a, b);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005810 File Offset: 0x00003A10
		public static void Multiply(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			if ((a.s1 | b.s1) == 0UL)
			{
				UInt128.Multiply64(out c, a.s0, b.s0);
				return;
			}
			if (a.s1 == 0UL)
			{
				UInt128.Multiply128(out c, ref b, a.s0);
				return;
			}
			if (b.s1 == 0UL)
			{
				UInt128.Multiply128(out c, ref a, b.s0);
				return;
			}
			UInt128.Multiply128(out c, ref a, ref b);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005874 File Offset: 0x00003A74
		private static void Multiply(out UInt128.UInt256 c, ref UInt128 a, ref UInt128 b)
		{
			UInt128 @uint;
			UInt128.Multiply64(out @uint, a.s0, b.s0);
			UInt128 uint2;
			UInt128.Multiply64(out uint2, a.s0, b.s1);
			UInt128 uint3;
			UInt128.Multiply64(out uint3, a.s1, b.s0);
			UInt128 uint4;
			UInt128.Multiply64(out uint4, a.s1, b.s1);
			uint num = 0u;
			uint num2 = 0u;
			c.s0 = @uint.S0;
			c.s1 = UInt128.Add(UInt128.Add(@uint.s1, uint2.s0, ref num), uint3.s0, ref num);
			c.s2 = UInt128.Add(UInt128.Add(UInt128.Add(uint2.s1, uint3.s1, ref num2), uint4.s0, ref num2), (ulong)num, ref num2);
			c.s3 = uint4.s1 + (ulong)num2;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00002311 File Offset: 0x00000511
		public static UInt128 Abs(UInt128 a)
		{
			return a;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005948 File Offset: 0x00003B48
		public static UInt128 Square(ulong a)
		{
			UInt128 result;
			UInt128.Square(out result, a);
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005960 File Offset: 0x00003B60
		public static UInt128 Square(UInt128 a)
		{
			UInt128 result;
			UInt128.Square(out result, ref a);
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000031FD File Offset: 0x000013FD
		public static void Square(out UInt128 c, ulong a)
		{
			UInt128.Square64(out c, a);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003206 File Offset: 0x00001406
		public static void Square(out UInt128 c, ref UInt128 a)
		{
			if (a.s1 == 0UL)
			{
				UInt128.Square64(out c, a.s0);
				return;
			}
			UInt128.Multiply128(out c, ref a, ref a);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005978 File Offset: 0x00003B78
		public static UInt128 Cube(ulong a)
		{
			UInt128 result;
			UInt128.Cube(out result, a);
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005990 File Offset: 0x00003B90
		public static UInt128 Cube(UInt128 a)
		{
			UInt128 result;
			UInt128.Cube(out result, ref a);
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000059A8 File Offset: 0x00003BA8
		public static void Cube(out UInt128 c, ulong a)
		{
			UInt128 @uint;
			UInt128.Square(out @uint, a);
			UInt128.Multiply(out c, ref @uint, a);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000059C8 File Offset: 0x00003BC8
		public static void Cube(out UInt128 c, ref UInt128 a)
		{
			UInt128 @uint;
			if (a.s1 == 0UL)
			{
				UInt128.Square64(out @uint, a.s0);
				UInt128.Multiply(out c, ref @uint, a.s0);
				return;
			}
			UInt128.Multiply128(out @uint, ref a, ref a);
			UInt128.Multiply128(out c, ref @uint, ref a);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00003225 File Offset: 0x00001425
		public static void Add(out UInt128 c, ulong a, ulong b)
		{
			c.s0 = a + b;
			c.s1 = 0UL;
			if (c.s0 < a && c.s0 < b)
			{
				c.s1 += 1UL;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005A0C File Offset: 0x00003C0C
		public static void Add(out UInt128 c, ref UInt128 a, ulong b)
		{
			c.s0 = a.s0 + b;
			c.s1 = a.s1;
			if (c.s0 < a.s0 && c.s0 < b)
			{
				c.s1 += 1UL;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005A60 File Offset: 0x00003C60
		public static void Add(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			c.s0 = a.s0 + b.s0;
			c.s1 = a.s1 + b.s1;
			if (c.s0 < a.s0 && c.s0 < b.s0)
			{
				c.s1 += 1UL;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005AC4 File Offset: 0x00003CC4
		private static ulong Add(ulong a, ulong b, ref uint carry)
		{
			ulong num = a + b;
			if (num < a && num < b)
			{
				carry += 1u;
			}
			return num;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public static void Add(ref UInt128 a, ulong b)
		{
			ulong num = a.s0 + b;
			if (num < a.s0 && num < b)
			{
				a.s1 += 1UL;
			}
			a.s0 = num;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005B24 File Offset: 0x00003D24
		public static void Add(ref UInt128 a, ref UInt128 b)
		{
			ulong num = a.s0 + b.s0;
			if (num < a.s0 && num < b.s0)
			{
				a.s1 += 1UL;
			}
			a.s0 = num;
			a.s1 += b.s1;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00003264 File Offset: 0x00001464
		public static void Add(ref UInt128 a, UInt128 b)
		{
			UInt128.Add(ref a, ref b);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000326E File Offset: 0x0000146E
		public static void Subtract(out UInt128 c, ref UInt128 a, ulong b)
		{
			c.s0 = a.s0 - b;
			c.s1 = a.s1;
			if (a.s0 < b)
			{
				c.s1 -= 1UL;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005B7C File Offset: 0x00003D7C
		public static void Subtract(out UInt128 c, ulong a, ref UInt128 b)
		{
			c.s0 = a - b.s0;
			c.s1 = 0UL - b.s1;
			if (a < b.s0)
			{
				c.s1 -= 1UL;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005BCC File Offset: 0x00003DCC
		public static void Subtract(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			c.s0 = a.s0 - b.s0;
			c.s1 = a.s1 - b.s1;
			if (a.s0 < b.s0)
			{
				c.s1 -= 1UL;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000032A6 File Offset: 0x000014A6
		public static void Subtract(ref UInt128 a, ulong b)
		{
			if (a.s0 < b)
			{
				a.s1 -= 1UL;
			}
			a.s0 -= b;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005C20 File Offset: 0x00003E20
		public static void Subtract(ref UInt128 a, ref UInt128 b)
		{
			if (a.s0 < b.s0)
			{
				a.s1 -= 1UL;
			}
			a.s0 -= b.s0;
			a.s1 -= b.s1;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000032CF File Offset: 0x000014CF
		public static void Subtract(ref UInt128 a, UInt128 b)
		{
			UInt128.Subtract(ref a, ref b);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005C70 File Offset: 0x00003E70
		private static void Square64(out UInt128 w, ulong u)
		{
			ulong num = (ulong)((uint)u);
			ulong num2 = u >> 32;
			ulong num3 = num * num;
			uint num4 = (uint)num3;
			ulong num5 = num * num2;
			num3 = (num3 >> 32) + num5;
			ulong num6 = num3 >> 32;
			num3 = (ulong)((uint)num3) + num5;
			w.s0 = (num3 << 32 | (ulong)num4);
			w.s1 = (num3 >> 32) + num6 + num2 * num2;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000032D9 File Offset: 0x000014D9
		private static void Multiply64(out UInt128 w, uint u, uint v)
		{
			w.s0 = (ulong)u * (ulong)v;
			w.s1 = 0UL;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005CC0 File Offset: 0x00003EC0
		private static void Multiply64(out UInt128 w, ulong u, uint v)
		{
			ulong num = (ulong)((uint)u);
			ulong num2 = u >> 32;
			ulong num3 = num * (ulong)v;
			uint num4 = (uint)num3;
			num3 = (num3 >> 32) + num2 * (ulong)v;
			w.s0 = (num3 << 32 | (ulong)num4);
			w.s1 = num3 >> 32;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005D00 File Offset: 0x00003F00
		private static void Multiply64(out UInt128 w, ulong u, ulong v)
		{
			ulong num = (ulong)((uint)u);
			ulong num2 = u >> 32;
			ulong num3 = (ulong)((uint)v);
			ulong num4 = v >> 32;
			ulong num5 = num * num3;
			uint num6 = (uint)num5;
			num5 = (num5 >> 32) + num * num4;
			ulong num7 = num5 >> 32;
			num5 = (ulong)((uint)num5) + num2 * num3;
			w.s0 = (num5 << 32 | (ulong)num6);
			w.s1 = (num5 >> 32) + num7 + num2 * num4;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005D68 File Offset: 0x00003F68
		private static void Multiply64(out UInt128 w, ulong u, ulong v, ulong c)
		{
			ulong num = (ulong)((uint)u);
			ulong num2 = u >> 32;
			ulong num3 = (ulong)((uint)v);
			ulong num4 = v >> 32;
			ulong num5 = num * num3 + (ulong)((uint)c);
			uint num6 = (uint)num5;
			num5 = (num5 >> 32) + num * num4 + (c >> 32);
			ulong num7 = num5 >> 32;
			num5 = (ulong)((uint)num5) + num2 * num3;
			w.s0 = (num5 << 32 | (ulong)num6);
			w.s1 = (num5 >> 32) + num7 + num2 * num4;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005DD8 File Offset: 0x00003FD8
		private static ulong MultiplyHigh64(ulong u, ulong v, ulong c)
		{
			ulong num = (ulong)((uint)u);
			ulong num2 = u >> 32;
			ulong num3 = (ulong)((uint)v);
			ulong num4 = v >> 32;
			ulong num5 = (num * num3 + (ulong)((uint)c) >> 32) + num * num4 + (c >> 32);
			ulong num6 = num5 >> 32;
			return ((ulong)((uint)num5) + num2 * num3 >> 32) + num6 + num2 * num4;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000032F5 File Offset: 0x000014F5
		private static void Multiply128(out UInt128 w, ref UInt128 u, uint v)
		{
			UInt128.Multiply64(out w, u.s0, v);
			w.s1 += u.s1 * (ulong)v;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003317 File Offset: 0x00001517
		private static void Multiply128(out UInt128 w, ref UInt128 u, ulong v)
		{
			UInt128.Multiply64(out w, u.s0, v);
			w.s1 += u.s1 * v;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00003338 File Offset: 0x00001538
		private static void Multiply128(out UInt128 w, ref UInt128 u, ref UInt128 v)
		{
			UInt128.Multiply64(out w, u.s0, v.s0);
			w.s1 += u.s1 * v.s0 + u.s0 * v.s1;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00003371 File Offset: 0x00001571
		public static void Divide(out UInt128 w, ref UInt128 u, uint v)
		{
			if (u.s1 == 0UL)
			{
				UInt128.Divide64(out w, u.s0, (ulong)v);
				return;
			}
			if (u.s1 <= 4294967295UL)
			{
				UInt128.Divide96(out w, ref u, v);
				return;
			}
			UInt128.Divide128(out w, ref u, v);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005E20 File Offset: 0x00004020
		public static void Divide(out UInt128 w, ref UInt128 u, ulong v)
		{
			if (u.s1 == 0UL)
			{
				UInt128.Divide64(out w, u.s0, v);
				return;
			}
			uint num = (uint)v;
			if (v == (ulong)num)
			{
				if (u.s1 <= 4294967295UL)
				{
					UInt128.Divide96(out w, ref u, num);
					return;
				}
				UInt128.Divide128(out w, ref u, num);
				return;
			}
			else
			{
				if (u.s1 <= 4294967295UL)
				{
					UInt128.Divide96(out w, ref u, v);
					return;
				}
				UInt128.Divide128(out w, ref u, v);
				return;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005E90 File Offset: 0x00004090
		public static void Divide(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			if (UInt128.LessThan(ref a, ref b))
			{
				c = UInt128.Zero;
				return;
			}
			if (b.s1 == 0UL)
			{
				UInt128.Divide(out c, ref a, b.s0);
				return;
			}
			if (b.s1 <= 4294967295UL)
			{
				UInt128 @uint;
				UInt128.Create(out c, UInt128.DivRem96(out @uint, ref a, ref b));
				return;
			}
			UInt128 uint2;
			UInt128.Create(out c, (long)((ulong)UInt128.DivRem128(out uint2, ref a, ref b)));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000033AC File Offset: 0x000015AC
		public static uint Remainder(ref UInt128 u, uint v)
		{
			if (u.s1 == 0UL)
			{
				return (uint)(u.s0 % (ulong)v);
			}
			if (u.s1 <= 4294967295UL)
			{
				return UInt128.Remainder96(ref u, v);
			}
			return UInt128.Remainder128(ref u, v);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005EFC File Offset: 0x000040FC
		public static ulong Remainder(ref UInt128 u, ulong v)
		{
			if (u.s1 == 0UL)
			{
				return u.s0 % v;
			}
			uint num = (uint)v;
			if (v == (ulong)num)
			{
				if (u.s1 <= 4294967295UL)
				{
					return (ulong)UInt128.Remainder96(ref u, num);
				}
				return (ulong)UInt128.Remainder128(ref u, num);
			}
			else
			{
				if (u.s1 <= 4294967295UL)
				{
					return UInt128.Remainder96(ref u, v);
				}
				return UInt128.Remainder128(ref u, v);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005F68 File Offset: 0x00004168
		public static void Remainder(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			if (UInt128.LessThan(ref a, ref b))
			{
				c = a;
				return;
			}
			if (b.s1 == 0UL)
			{
				UInt128.Create(out c, UInt128.Remainder(ref a, b.s0));
				return;
			}
			if (b.s1 <= 4294967295UL)
			{
				UInt128.DivRem96(out c, ref a, ref b);
				return;
			}
			UInt128.DivRem128(out c, ref a, ref b);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005FCC File Offset: 0x000041CC
		public static void Remainder(ref UInt128 a, ref UInt128 b)
		{
			UInt128 @uint = a;
			UInt128.Remainder(out a, ref @uint, ref b);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000033E1 File Offset: 0x000015E1
		private static void Remainder(out UInt128 c, ref UInt128.UInt256 a, ref UInt128 b)
		{
			if (b.r3 == 0u)
			{
				UInt128.Remainder192(out c, ref a, ref b);
				return;
			}
			UInt128.Remainder256(out c, ref a, ref b);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000033FC File Offset: 0x000015FC
		private static void Divide64(out UInt128 w, ulong u, ulong v)
		{
			w.s1 = 0UL;
			w.s0 = u / v;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005FEC File Offset: 0x000041EC
		private static void Divide96(out UInt128 w, ref UInt128 u, uint v)
		{
			uint r = u.r2;
			uint num = r / v;
			ulong num2 = (ulong)(r - num * v) << 32 | (ulong)u.r1;
			uint num3 = (uint)(num2 / (ulong)v);
			uint num4 = (uint)((num2 - (ulong)(num3 * v) << 32 | (ulong)u.r0) / (ulong)v);
			w.s1 = (ulong)num;
			w.s0 = ((ulong)num3 << 32 | (ulong)num4);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006044 File Offset: 0x00004244
		private static void Divide128(out UInt128 w, ref UInt128 u, uint v)
		{
			uint r = u.r3;
			uint num = r / v;
			ulong num2 = (ulong)(r - num * v) << 32 | (ulong)u.r2;
			uint num3 = (uint)(num2 / (ulong)v);
			ulong num4 = num2 - (ulong)(num3 * v) << 32 | (ulong)u.r1;
			uint num5 = (uint)(num4 / (ulong)v);
			uint num6 = (uint)((num4 - (ulong)(num5 * v) << 32 | (ulong)u.r0) / (ulong)v);
			w.s1 = ((ulong)num << 32 | (ulong)num3);
			w.s0 = ((ulong)num5 << 32 | (ulong)num6);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000060B8 File Offset: 0x000042B8
		private static void Divide96(out UInt128 w, ref UInt128 u, ulong v)
		{
			w.s1 = 0UL;
			w.s0 = 0UL;
			int num = UInt128.GetBitLength((uint)(v >> 32));
			int num2 = 32 - num;
			ulong num3 = v << num2;
			uint v2 = (uint)(num3 >> 32);
			uint v3 = (uint)num3;
			uint num4 = u.r0;
			uint num5 = u.r1;
			uint num6 = u.r2;
			uint u2 = 0u;
			if (num2 != 0)
			{
				u2 = num6 >> num;
				num6 = (num6 << num2 | num5 >> num);
				num5 = (num5 << num2 | num4 >> num);
				num4 <<= num2;
			}
			uint num7 = UInt128.DivRem(u2, ref num6, ref num5, v2, v3);
			uint num8 = UInt128.DivRem(num6, ref num5, ref num4, v2, v3);
			w.s0 = ((ulong)num7 << 32 | (ulong)num8);
			w.s1 = 0UL;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006190 File Offset: 0x00004390
		private static void Divide128(out UInt128 w, ref UInt128 u, ulong v)
		{
			w.s1 = 0UL;
			w.s0 = 0UL;
			int num = UInt128.GetBitLength((uint)(v >> 32));
			int num2 = 32 - num;
			ulong num3 = v << num2;
			uint v2 = (uint)(num3 >> 32);
			uint v3 = (uint)num3;
			uint num4 = u.r0;
			uint num5 = u.r1;
			uint num6 = u.r2;
			uint num7 = u.r3;
			uint u2 = 0u;
			if (num2 != 0)
			{
				u2 = num7 >> num;
				num7 = (num7 << num2 | num6 >> num);
				num6 = (num6 << num2 | num5 >> num);
				num5 = (num5 << num2 | num4 >> num);
				num4 <<= num2;
			}
			w.s1 = (ulong)UInt128.DivRem(u2, ref num7, ref num6, v2, v3);
			uint num8 = UInt128.DivRem(num7, ref num6, ref num5, v2, v3);
			uint num9 = UInt128.DivRem(num6, ref num5, ref num4, v2, v3);
			w.s0 = ((ulong)num8 << 32 | (ulong)num9);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003416 File Offset: 0x00001616
		private static uint Remainder96(ref UInt128 u, uint v)
		{
			return (uint)((((ulong)(u.r2 % v) << 32 | (ulong)u.r1) % (ulong)v << 32 | (ulong)u.r0) % (ulong)v);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000343E File Offset: 0x0000163E
		private static uint Remainder128(ref UInt128 u, uint v)
		{
			return (uint)(((((ulong)(u.r3 % v) << 32 | (ulong)u.r2) % (ulong)v << 32 | (ulong)u.r1) % (ulong)v << 32 | (ulong)u.r0) % (ulong)v);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006288 File Offset: 0x00004488
		private static ulong Remainder96(ref UInt128 u, ulong v)
		{
			int num = UInt128.GetBitLength((uint)(v >> 32));
			int num2 = 32 - num;
			ulong num3 = v << num2;
			uint v2 = (uint)(num3 >> 32);
			uint v3 = (uint)num3;
			uint num4 = u.r0;
			uint num5 = u.r1;
			uint num6 = u.r2;
			uint u2 = 0u;
			if (num2 != 0)
			{
				u2 = num6 >> num;
				num6 = (num6 << num2 | num5 >> num);
				num5 = (num5 << num2 | num4 >> num);
				num4 <<= num2;
			}
			UInt128.DivRem(u2, ref num6, ref num5, v2, v3);
			UInt128.DivRem(num6, ref num5, ref num4, v2, v3);
			return ((ulong)num5 << 32 | (ulong)num4) >> num2;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006330 File Offset: 0x00004530
		private static ulong Remainder128(ref UInt128 u, ulong v)
		{
			int num = UInt128.GetBitLength((uint)(v >> 32));
			int num2 = 32 - num;
			ulong num3 = v << num2;
			uint v2 = (uint)(num3 >> 32);
			uint v3 = (uint)num3;
			uint num4 = u.r0;
			uint num5 = u.r1;
			uint num6 = u.r2;
			uint num7 = u.r3;
			uint u2 = 0u;
			if (num2 != 0)
			{
				u2 = num7 >> num;
				num7 = (num7 << num2 | num6 >> num);
				num6 = (num6 << num2 | num5 >> num);
				num5 = (num5 << num2 | num4 >> num);
				num4 <<= num2;
			}
			UInt128.DivRem(u2, ref num7, ref num6, v2, v3);
			UInt128.DivRem(num7, ref num6, ref num5, v2, v3);
			UInt128.DivRem(num6, ref num5, ref num4, v2, v3);
			return ((ulong)num5 << 32 | (ulong)num4) >> num2;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006400 File Offset: 0x00004600
		private static ulong DivRem96(out UInt128 rem, ref UInt128 a, ref UInt128 b)
		{
			int d = 32 - UInt128.GetBitLength(b.r2);
			UInt128 @uint;
			UInt128.LeftShift64(out @uint, ref b, d);
			uint u = (uint)UInt128.LeftShift64(out rem, ref a, d);
			uint r = @uint.r2;
			uint r2 = @uint.r1;
			uint r3 = @uint.r0;
			uint r4 = rem.r3;
			uint r5 = rem.r2;
			uint r6 = rem.r1;
			uint r7 = rem.r0;
			ulong num = (ulong)UInt128.DivRem(u, ref r4, ref r5, ref r6, r, r2, r3);
			uint num2 = UInt128.DivRem(r4, ref r5, ref r6, ref r7, r, r2, r3);
			UInt128.Create(out rem, r7, r6, r5, 0u);
			ulong result = num << 32 | (ulong)num2;
			UInt128.RightShift64(ref rem, d);
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000064A8 File Offset: 0x000046A8
		private static uint DivRem128(out UInt128 rem, ref UInt128 a, ref UInt128 b)
		{
			int d = 32 - UInt128.GetBitLength(b.r3);
			UInt128 @uint;
			UInt128.LeftShift64(out @uint, ref b, d);
			uint u = (uint)UInt128.LeftShift64(out rem, ref a, d);
			uint r = rem.r3;
			uint r2 = rem.r2;
			uint r3 = rem.r1;
			uint r4 = rem.r0;
			uint result = UInt128.DivRem(u, ref r, ref r2, ref r3, ref r4, @uint.r3, @uint.r2, @uint.r1, @uint.r0);
			UInt128.Create(out rem, r4, r3, r2, r);
			UInt128.RightShift64(ref rem, d);
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006534 File Offset: 0x00004734
		private static void Remainder192(out UInt128 c, ref UInt128.UInt256 a, ref UInt128 b)
		{
			int d = 32 - UInt128.GetBitLength(b.r2);
			UInt128 @uint;
			UInt128.LeftShift64(out @uint, ref b, d);
			uint r = @uint.r2;
			uint r2 = @uint.r1;
			uint r3 = @uint.r0;
			UInt128.UInt256 uint2;
			UInt128.LeftShift64(out uint2, ref a, d);
			uint r4 = uint2.r6;
			uint r5 = uint2.r5;
			uint r6 = uint2.r4;
			uint r7 = uint2.r3;
			uint r8 = uint2.r2;
			uint r9 = uint2.r1;
			uint r10 = uint2.r0;
			UInt128.DivRem(r4, ref r5, ref r6, ref r7, r, r2, r3);
			UInt128.DivRem(r5, ref r6, ref r7, ref r8, r, r2, r3);
			UInt128.DivRem(r6, ref r7, ref r8, ref r9, r, r2, r3);
			UInt128.DivRem(r7, ref r8, ref r9, ref r10, r, r2, r3);
			UInt128.Create(out c, r10, r9, r8, 0u);
			UInt128.RightShift64(ref c, d);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006614 File Offset: 0x00004814
		private static void Remainder256(out UInt128 c, ref UInt128.UInt256 a, ref UInt128 b)
		{
			int d = 32 - UInt128.GetBitLength(b.r3);
			UInt128 @uint;
			UInt128.LeftShift64(out @uint, ref b, d);
			uint r = @uint.r3;
			uint r2 = @uint.r2;
			uint r3 = @uint.r1;
			uint r4 = @uint.r0;
			UInt128.UInt256 uint2;
			uint u = (uint)UInt128.LeftShift64(out uint2, ref a, d);
			uint r5 = uint2.r7;
			uint r6 = uint2.r6;
			uint r7 = uint2.r5;
			uint r8 = uint2.r4;
			uint r9 = uint2.r3;
			uint r10 = uint2.r2;
			uint r11 = uint2.r1;
			uint r12 = uint2.r0;
			UInt128.DivRem(u, ref r5, ref r6, ref r7, ref r8, r, r2, r3, r4);
			UInt128.DivRem(r5, ref r6, ref r7, ref r8, ref r9, r, r2, r3, r4);
			UInt128.DivRem(r6, ref r7, ref r8, ref r9, ref r10, r, r2, r3, r4);
			UInt128.DivRem(r7, ref r8, ref r9, ref r10, ref r11, r, r2, r3, r4);
			UInt128.DivRem(r8, ref r9, ref r10, ref r11, ref r12, r, r2, r3, r4);
			UInt128.Create(out c, r12, r11, r10, r9);
			UInt128.RightShift64(ref c, d);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006730 File Offset: 0x00004930
		private static ulong Q(uint u0, uint u1, uint u2, uint v1, uint v2)
		{
			ulong num = (ulong)u0 << 32 | (ulong)u1;
			ulong num2 = (u0 == v1) ? 4294967295UL : (num / (ulong)v1);
			ulong num3 = num - num2 * (ulong)v1;
			if (num3 == (ulong)((uint)num3) && (ulong)v2 * num2 > (num3 << 32 | (ulong)u2))
			{
				num2 -= 1UL;
				num3 += (ulong)v1;
				if (num3 == (ulong)((uint)num3) && (ulong)v2 * num2 > (num3 << 32 | (ulong)u2))
				{
					num2 -= 1UL;
					num3 += (ulong)v1;
				}
			}
			return num2;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000067AC File Offset: 0x000049AC
		private static uint DivRem(uint u0, ref uint u1, ref uint u2, uint v1, uint v2)
		{
			ulong num = UInt128.Q(u0, u1, u2, v1, v2);
			ulong num2 = num * (ulong)v2;
			long num3 = (long)((ulong)u2 - (ulong)((uint)num2));
			num2 >>= 32;
			u2 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v1;
			num3 += (long)((ulong)u1 - (ulong)((uint)num2));
			num2 >>= 32;
			u1 = (uint)num3;
			num3 >>= 32;
			num3 += (long)((ulong)u0 - (ulong)((uint)num2));
			if (num3 != 0L)
			{
				num -= 1UL;
				num2 = (ulong)u2 + (ulong)v2;
				u2 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u1 + (ulong)v1;
				u1 = (uint)num2;
			}
			return (uint)num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000683C File Offset: 0x00004A3C
		private static uint DivRem(uint u0, ref uint u1, ref uint u2, ref uint u3, uint v1, uint v2, uint v3)
		{
			ulong num = UInt128.Q(u0, u1, u2, v1, v2);
			ulong num2 = num * (ulong)v3;
			long num3 = (long)((ulong)u3 - (ulong)((uint)num2));
			num2 >>= 32;
			u3 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v2;
			num3 += (long)((ulong)u2 - (ulong)((uint)num2));
			num2 >>= 32;
			u2 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v1;
			num3 += (long)((ulong)u1 - (ulong)((uint)num2));
			num2 >>= 32;
			u1 = (uint)num3;
			num3 >>= 32;
			num3 += (long)((ulong)u0 - (ulong)((uint)num2));
			if (num3 != 0L)
			{
				num -= 1UL;
				num2 = (ulong)u3 + (ulong)v3;
				u3 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u2 + (ulong)v2;
				u2 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u1 + (ulong)v1;
				u1 = (uint)num2;
			}
			return (uint)num;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006900 File Offset: 0x00004B00
		private static uint DivRem(uint u0, ref uint u1, ref uint u2, ref uint u3, ref uint u4, uint v1, uint v2, uint v3, uint v4)
		{
			ulong num = UInt128.Q(u0, u1, u2, v1, v2);
			ulong num2 = num * (ulong)v4;
			long num3 = (long)((ulong)u4 - (ulong)((uint)num2));
			num2 >>= 32;
			u4 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v3;
			num3 += (long)((ulong)u3 - (ulong)((uint)num2));
			num2 >>= 32;
			u3 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v2;
			num3 += (long)((ulong)u2 - (ulong)((uint)num2));
			num2 >>= 32;
			u2 = (uint)num3;
			num3 >>= 32;
			num2 += num * (ulong)v1;
			num3 += (long)((ulong)u1 - (ulong)((uint)num2));
			num2 >>= 32;
			u1 = (uint)num3;
			num3 >>= 32;
			num3 += (long)((ulong)u0 - (ulong)((uint)num2));
			if (num3 != 0L)
			{
				num -= 1UL;
				num2 = (ulong)u4 + (ulong)v4;
				u4 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u3 + (ulong)v3;
				u3 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u2 + (ulong)v2;
				u2 = (uint)num2;
				num2 >>= 32;
				num2 += (ulong)u1 + (ulong)v1;
				u1 = (uint)num2;
			}
			return (uint)num;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00003474 File Offset: 0x00001674
		public static void ModAdd(out UInt128 c, ref UInt128 a, ref UInt128 b, ref UInt128 modulus)
		{
			UInt128.Add(out c, ref a, ref b);
			if (!UInt128.LessThan(ref c, ref modulus) || (UInt128.LessThan(ref c, ref a) && UInt128.LessThan(ref c, ref b)))
			{
				UInt128.Subtract(ref c, ref modulus);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000034A0 File Offset: 0x000016A0
		public static void ModSub(out UInt128 c, ref UInt128 a, ref UInt128 b, ref UInt128 modulus)
		{
			UInt128.Subtract(out c, ref a, ref b);
			if (UInt128.LessThan(ref a, ref b))
			{
				UInt128.Add(ref c, ref modulus);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000069FC File Offset: 0x00004BFC
		public static void ModMul(out UInt128 c, ref UInt128 a, ref UInt128 b, ref UInt128 modulus)
		{
			if (modulus.s1 == 0UL)
			{
				UInt128 @uint;
				UInt128.Multiply64(out @uint, a.s0, b.s0);
				UInt128.Create(out c, UInt128.Remainder(ref @uint, modulus.s0));
				return;
			}
			UInt128.UInt256 uint2;
			UInt128.Multiply(out uint2, ref a, ref b);
			UInt128.Remainder(out c, ref uint2, ref modulus);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006A4C File Offset: 0x00004C4C
		public static void ModMul(ref UInt128 a, ref UInt128 b, ref UInt128 modulus)
		{
			if (modulus.s1 == 0UL)
			{
				UInt128 @uint;
				UInt128.Multiply64(out @uint, a.s0, b.s0);
				UInt128.Create(out a, UInt128.Remainder(ref @uint, modulus.s0));
				return;
			}
			UInt128.UInt256 uint2;
			UInt128.Multiply(out uint2, ref a, ref b);
			UInt128.Remainder(out a, ref uint2, ref modulus);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006A9C File Offset: 0x00004C9C
		public static void ModPow(out UInt128 result, ref UInt128 value, ref UInt128 exponent, ref UInt128 modulus)
		{
			result = UInt128.one;
			UInt128 @uint = value;
			ulong num = exponent.s0;
			if (exponent.s1 != 0UL)
			{
				for (int i = 0; i < 64; i++)
				{
					if ((num & 1UL) != 0UL)
					{
						UInt128.ModMul(ref result, ref @uint, ref modulus);
					}
					UInt128.ModMul(ref @uint, ref @uint, ref modulus);
					num >>= 1;
				}
				num = exponent.s1;
			}
			while (num != 0UL)
			{
				if ((num & 1UL) != 0UL)
				{
					UInt128.ModMul(ref result, ref @uint, ref modulus);
				}
				if (num != 1UL)
				{
					UInt128.ModMul(ref @uint, ref @uint, ref modulus);
				}
				num >>= 1;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000034BA File Offset: 0x000016BA
		public static void Shift(out UInt128 c, ref UInt128 a, int d)
		{
			if (d < 0)
			{
				UInt128.RightShift(out c, ref a, -d);
				return;
			}
			UInt128.LeftShift(out c, ref a, d);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000034D2 File Offset: 0x000016D2
		public static void ArithmeticShift(out UInt128 c, ref UInt128 a, int d)
		{
			if (d < 0)
			{
				UInt128.ArithmeticRightShift(out c, ref a, -d);
				return;
			}
			UInt128.LeftShift(out c, ref a, d);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006B38 File Offset: 0x00004D38
		public static ulong LeftShift64(out UInt128 c, ref UInt128 a, int d)
		{
			if (d == 0)
			{
				c = a;
				return 0UL;
			}
			int num = 64 - d;
			c.s1 = (a.s1 << d | a.s0 >> num);
			c.s0 = a.s0 << d;
			return a.s1 >> num;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006B9C File Offset: 0x00004D9C
		private static ulong LeftShift64(out UInt128.UInt256 c, ref UInt128.UInt256 a, int d)
		{
			if (d == 0)
			{
				c = a;
				return 0UL;
			}
			int num = 64 - d;
			c.s3 = (a.s3 << d | a.s2 >> num);
			c.s2 = (a.s2 << d | a.s1 >> num);
			c.s1 = (a.s1 << d | a.s0 >> num);
			c.s0 = a.s0 << d;
			return a.s3 >> num;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static void LeftShift(out UInt128 c, ref UInt128 a, int b)
		{
			if (b < 64)
			{
				UInt128.LeftShift64(out c, ref a, b);
				return;
			}
			if (b == 64)
			{
				c.s0 = 0UL;
				c.s1 = a.s0;
				return;
			}
			c.s0 = 0UL;
			c.s1 = a.s0 << b - 64;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006C9C File Offset: 0x00004E9C
		public static void RightShift64(out UInt128 c, ref UInt128 a, int b)
		{
			if (b == 0)
			{
				c = a;
				return;
			}
			c.s0 = (a.s0 >> b | a.s1 << 64 - b);
			c.s1 = a.s1 >> b;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006CEC File Offset: 0x00004EEC
		public static void RightShift(out UInt128 c, ref UInt128 a, int b)
		{
			if (b < 64)
			{
				UInt128.RightShift64(out c, ref a, b);
				return;
			}
			if (b == 64)
			{
				c.s0 = a.s1;
				c.s1 = 0UL;
				return;
			}
			c.s0 = a.s1 >> b - 64;
			c.s1 = 0UL;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006D4C File Offset: 0x00004F4C
		public static void ArithmeticRightShift64(out UInt128 c, ref UInt128 a, int b)
		{
			if (b == 0)
			{
				c = a;
				return;
			}
			c.s0 = (a.s0 >> b | a.s1 << 64 - b);
			c.s1 = a.s1 >> b;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006D9C File Offset: 0x00004F9C
		public static void ArithmeticRightShift(out UInt128 c, ref UInt128 a, int b)
		{
			if (b < 64)
			{
				UInt128.ArithmeticRightShift64(out c, ref a, b);
				return;
			}
			if (b == 64)
			{
				c.s0 = a.s1;
				c.s1 = a.s1 >> 63;
				return;
			}
			c.s0 = a.s1 >> b - 64;
			c.s1 = a.s1 >> 63;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000034EA File Offset: 0x000016EA
		public static void And(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			c.s0 = (a.s0 & b.s0);
			c.s1 = (a.s1 & b.s1);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00003512 File Offset: 0x00001712
		public static void Or(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			c.s0 = (a.s0 | b.s0);
			c.s1 = (a.s1 | b.s1);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000353A File Offset: 0x0000173A
		public static void ExclusiveOr(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			c.s0 = (a.s0 ^ b.s0);
			c.s1 = (a.s1 ^ b.s1);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00003562 File Offset: 0x00001762
		public static void Not(out UInt128 c, ref UInt128 a)
		{
			c.s0 = ~a.s0;
			c.s1 = ~a.s1;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006DFC File Offset: 0x00004FFC
		public static void Negate(ref UInt128 a)
		{
			ulong num = a.s0;
			a.s0 = 0UL - num;
			a.s1 = 0UL - a.s1;
			if (num > 0UL)
			{
				a.s1 -= 1UL;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006E58 File Offset: 0x00005058
		public static void Negate(out UInt128 c, ref UInt128 a)
		{
			c.s0 = 0UL - a.s0;
			c.s1 = 0UL - a.s1;
			if (a.s0 > 0UL)
			{
				c.s1 -= 1UL;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006EB8 File Offset: 0x000050B8
		public static void Pow(out UInt128 result, ref UInt128 value, uint exponent)
		{
			result = UInt128.one;
			while (exponent != 0u)
			{
				if ((exponent & 1u) != 0u)
				{
					UInt128 @uint = result;
					UInt128.Multiply(out result, ref @uint, ref value);
				}
				if (exponent != 1u)
				{
					UInt128 uint2 = value;
					UInt128.Square(out value, ref uint2);
				}
				exponent >>= 1;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006F04 File Offset: 0x00005104
		public static UInt128 Pow(UInt128 value, uint exponent)
		{
			UInt128 result;
			UInt128.Pow(out result, ref value, exponent);
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006F1C File Offset: 0x0000511C
		public static ulong FloorSqrt(UInt128 a)
		{
			if (a.s1 == 0UL && a.s0 <= UInt128.maxRep)
			{
				return (ulong)Math.Sqrt(a.s0);
			}
			ulong num = (ulong)Math.Sqrt(UInt128.ConvertToDouble(ref a));
			if (a.s1 < UInt128.maxRepSquaredHigh)
			{
				UInt128 @uint;
				UInt128.Square(out @uint, num);
				ulong num2 = a.s0 - @uint.s0;
				if (num2 > 9223372036854775807UL)
				{
					num -= 1UL;
				}
				else if (num2 - (num << 1) <= 9223372036854775807UL)
				{
					num += 1UL;
				}
				return num;
			}
			return UInt128.FloorSqrt(ref a, num);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006FC8 File Offset: 0x000051C8
		public static ulong CeilingSqrt(UInt128 a)
		{
			if (a.s1 == 0UL && a.s0 <= UInt128.maxRep)
			{
				return (ulong)Math.Ceiling(Math.Sqrt(a.s0));
			}
			ulong num = (ulong)Math.Ceiling(Math.Sqrt(UInt128.ConvertToDouble(ref a)));
			if (a.s1 < UInt128.maxRepSquaredHigh)
			{
				UInt128 @uint;
				UInt128.Square(out @uint, num);
				ulong num2 = @uint.s0 - a.s0;
				if (num2 > 9223372036854775807UL)
				{
					num += 1UL;
				}
				else if (num2 - (num << 1) <= 9223372036854775807UL)
				{
					num -= 1UL;
				}
				return num;
			}
			num = UInt128.FloorSqrt(ref a, num);
			UInt128 uint2;
			UInt128.Square(out uint2, num);
			if (uint2.S0 != a.S0 || uint2.S1 != a.S1)
			{
				num += 1UL;
			}
			return num;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000070B0 File Offset: 0x000052B0
		private static ulong FloorSqrt(ref UInt128 a, ulong s)
		{
			ulong num = 0UL;
			ulong num2;
			for (;;)
			{
				UInt128 @uint;
				UInt128.Divide(out @uint, ref a, s);
				UInt128 uint2;
				UInt128.Add(out uint2, ref @uint, s);
				num2 = uint2.S0 >> 1;
				if (uint2.S1 != 0UL)
				{
					num2 |= 9223372036854775808UL;
				}
				if (num2 == num)
				{
					break;
				}
				num = s;
				s = num2;
			}
			if (num2 < s)
			{
				s = num2;
			}
			return s;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007114 File Offset: 0x00005314
		public static ulong FloorCbrt(UInt128 a)
		{
			ulong num = (ulong)Math.Pow(UInt128.ConvertToDouble(ref a), 0.33333333333333331);
			UInt128 b;
			UInt128.Cube(out b, num);
			if (a < b)
			{
				num -= 1UL;
			}
			else
			{
				UInt128 @uint;
				UInt128.Multiply(out @uint, 3UL * num, num + 1UL);
				UInt128 uint2;
				UInt128.Subtract(out uint2, ref a, ref b);
				if (UInt128.LessThan(ref @uint, ref uint2))
				{
					num += 1UL;
				}
			}
			return num;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007198 File Offset: 0x00005398
		public static ulong CeilingCbrt(UInt128 a)
		{
			ulong num = (ulong)Math.Ceiling(Math.Pow(UInt128.ConvertToDouble(ref a), 0.33333333333333331));
			UInt128 a2;
			UInt128.Cube(out a2, num);
			if (a2 < a)
			{
				num += 1UL;
			}
			else
			{
				UInt128 @uint;
				UInt128.Multiply(out @uint, 3UL * num, num + 1UL);
				UInt128 uint2;
				UInt128.Subtract(out uint2, ref a2, ref a);
				if (UInt128.LessThan(ref @uint, ref uint2))
				{
					num -= 1UL;
				}
			}
			return num;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000357E File Offset: 0x0000177E
		public static UInt128 Min(UInt128 a, UInt128 b)
		{
			if (UInt128.LessThan(ref a, ref b))
			{
				return a;
			}
			return b;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000358E File Offset: 0x0000178E
		public static UInt128 Max(UInt128 a, UInt128 b)
		{
			if (UInt128.LessThan(ref b, ref a))
			{
				return a;
			}
			return b;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000359E File Offset: 0x0000179E
		public static double Log(UInt128 a)
		{
			return UInt128.Log(a, 2.7182818284590451);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000035AF File Offset: 0x000017AF
		public static double Log10(UInt128 a)
		{
			return UInt128.Log(a, 10.0);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000035C0 File Offset: 0x000017C0
		public static double Log(UInt128 a, double b)
		{
			return Math.Log(UInt128.ConvertToDouble(ref a), b);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000566C File Offset: 0x0000386C
		public static UInt128 Add(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Add(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000056D8 File Offset: 0x000038D8
		public static UInt128 Subtract(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Subtract(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000057A4 File Offset: 0x000039A4
		public static UInt128 Multiply(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Multiply(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000057D8 File Offset: 0x000039D8
		public static UInt128 Divide(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Divide(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000057F4 File Offset: 0x000039F4
		public static UInt128 Remainder(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.Remainder(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007220 File Offset: 0x00005420
		public static UInt128 DivRem(UInt128 a, UInt128 b, out UInt128 remainder)
		{
			UInt128 result;
			UInt128.Divide(out result, ref a, ref b);
			UInt128.Remainder(out remainder, ref a, ref b);
			return result;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007244 File Offset: 0x00005444
		public static UInt128 ModAdd(UInt128 a, UInt128 b, UInt128 modulus)
		{
			UInt128 result;
			UInt128.ModAdd(out result, ref a, ref b, ref modulus);
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007260 File Offset: 0x00005460
		public static UInt128 ModSub(UInt128 a, UInt128 b, UInt128 modulus)
		{
			UInt128 result;
			UInt128.ModSub(out result, ref a, ref b, ref modulus);
			return result;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000727C File Offset: 0x0000547C
		public static UInt128 ModMul(UInt128 a, UInt128 b, UInt128 modulus)
		{
			UInt128 result;
			UInt128.ModMul(out result, ref a, ref b, ref modulus);
			return result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007298 File Offset: 0x00005498
		public static UInt128 ModPow(UInt128 value, UInt128 exponent, UInt128 modulus)
		{
			UInt128 result;
			UInt128.ModPow(out result, ref value, ref exponent, ref modulus);
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000072B4 File Offset: 0x000054B4
		public static UInt128 Negate(UInt128 a)
		{
			UInt128 result;
			UInt128.Negate(out result, ref a);
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000072CC File Offset: 0x000054CC
		public static UInt128 GreatestCommonDivisor(UInt128 a, UInt128 b)
		{
			UInt128 result;
			UInt128.GreatestCommonDivisor(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000035CF File Offset: 0x000017CF
		private static void RightShift64(ref UInt128 c, int d)
		{
			if (d == 0)
			{
				return;
			}
			c.s0 = (c.s1 << 64 - d | c.s0 >> d);
			c.s1 >>= d;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00003603 File Offset: 0x00001803
		public static void RightShift(ref UInt128 c, int d)
		{
			if (d < 64)
			{
				UInt128.RightShift64(ref c, d);
				return;
			}
			c.s0 = c.s1 >> d - 64;
			c.s1 = 0UL;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00003635 File Offset: 0x00001835
		public static void Shift(ref UInt128 c, int d)
		{
			if (d < 0)
			{
				UInt128.RightShift(ref c, -d);
				return;
			}
			UInt128.LeftShift(ref c, d);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000364B File Offset: 0x0000184B
		public static void ArithmeticShift(ref UInt128 c, int d)
		{
			if (d < 0)
			{
				UInt128.ArithmeticRightShift(ref c, -d);
				return;
			}
			UInt128.LeftShift(ref c, d);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00003661 File Offset: 0x00001861
		public static void RightShift(ref UInt128 c)
		{
			c.s0 = (c.s1 << 63 | c.s0 >> 1);
			c.s1 >>= 1;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00003686 File Offset: 0x00001886
		private static void ArithmeticRightShift64(ref UInt128 c, int d)
		{
			if (d == 0)
			{
				return;
			}
			c.s0 = (c.s1 << 64 - d | c.s0 >> d);
			c.s1 >>= d;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000036BD File Offset: 0x000018BD
		public static void ArithmeticRightShift(ref UInt128 c, int d)
		{
			if (d < 64)
			{
				UInt128.ArithmeticRightShift64(ref c, d);
				return;
			}
			c.s0 = c.s1 >> d - 64;
			c.s1 = 0UL;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000036EF File Offset: 0x000018EF
		public static void ArithmeticRightShift(ref UInt128 c)
		{
			c.s0 = (c.s1 << 63 | c.s0 >> 1);
			c.s1 >>= 1;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000072E8 File Offset: 0x000054E8
		private static ulong LeftShift64(ref UInt128 c, int d)
		{
			if (d == 0)
			{
				return 0UL;
			}
			int num = 64 - d;
			ulong result = c.s1 >> num;
			c.s1 = (c.s1 << d | c.s0 >> num);
			c.s0 <<= d;
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00003717 File Offset: 0x00001917
		public static void LeftShift(ref UInt128 c, int d)
		{
			if (d < 64)
			{
				UInt128.LeftShift64(ref c, d);
				return;
			}
			c.s1 = c.s0 << d - 64;
			c.s0 = 0UL;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000374A File Offset: 0x0000194A
		public static void LeftShift(ref UInt128 c)
		{
			c.s1 = (c.s1 << 1 | c.s0 >> 63);
			c.s0 <<= 1;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007340 File Offset: 0x00005540
		public static void Swap(ref UInt128 a, ref UInt128 b)
		{
			ulong num = a.s0;
			ulong num2 = a.s1;
			a.s0 = b.s0;
			a.s1 = b.s1;
			b.s0 = num;
			b.s1 = num2;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007384 File Offset: 0x00005584
		public static void GreatestCommonDivisor(out UInt128 c, ref UInt128 a, ref UInt128 b)
		{
			UInt128 @uint;
			UInt128 uint2;
			if (a.s1 == 0UL != (b.s1 == 0UL) && !a.IsZero && !b.IsZero)
			{
				if (UInt128.LessThan(ref a, ref b))
				{
					@uint = a;
					UInt128.Remainder(out uint2, ref b, ref a);
				}
				else
				{
					uint2 = b;
					UInt128.Remainder(out @uint, ref a, ref b);
				}
			}
			else
			{
				@uint = a;
				uint2 = b;
			}
			if (@uint.IsZero)
			{
				c = uint2;
				return;
			}
			if (uint2.IsZero)
			{
				c = @uint;
				return;
			}
			if (UInt128.LessThan(ref @uint, ref uint2))
			{
				UInt128.Swap(ref @uint, ref uint2);
			}
			while (@uint.s1 != 0UL && !b.IsZero)
			{
				int d = 63 - UInt128.GetBitLength(@uint.s1);
				UInt128 uint3;
				UInt128.Shift(out uint3, ref @uint, d);
				UInt128 uint4;
				UInt128.Shift(out uint4, ref uint2, d);
				long num = (long)uint3.s1;
				long num2 = (long)uint4.s1;
				if (num2 != 0L)
				{
					long num3 = 1L;
					long num4 = 0L;
					long num5 = 0L;
					long num6 = 1L;
					bool flag = true;
					for (;;)
					{
						long num7 = num / num2;
						long num8 = num3 - num7 * num5;
						long num9 = num4 - num7 * num6;
						long num10 = num;
						num = num2;
						num2 = num10 - num7 * num2;
						if (flag = !flag)
						{
							if (num2 < -num8)
							{
								break;
							}
							if (num - num2 < num9 - num6)
							{
								break;
							}
						}
						else if (num2 < -num9 || num - num2 < num8 - num5)
						{
							break;
						}
						num3 = num5;
						num4 = num6;
						num5 = num8;
						num6 = num9;
					}
					IL_1A5:
					if (num3 == 1L && num4 == 0L)
					{
						UInt128 uint5;
						UInt128.Remainder(out uint5, ref @uint, ref uint2);
						@uint = uint2;
						uint2 = uint5;
						continue;
					}
					UInt128 uint6;
					UInt128 uint7;
					if (flag)
					{
						UInt128.AddProducts(out uint6, num4, ref uint2, num3, ref @uint);
						UInt128.AddProducts(out uint7, num5, ref @uint, num6, ref uint2);
					}
					else
					{
						UInt128.AddProducts(out uint6, num3, ref @uint, num4, ref uint2);
						UInt128.AddProducts(out uint7, num6, ref uint2, num5, ref @uint);
					}
					@uint = uint6;
					uint2 = uint7;
					continue;
					goto IL_1A5;
				}
				UInt128 uint8;
				UInt128.Remainder(out uint8, ref @uint, ref uint2);
				@uint = uint2;
				uint2 = uint8;
			}
			if (uint2.IsZero)
			{
				c = @uint;
				return;
			}
			ulong num11 = @uint.s0;
			ulong num12 = uint2.s0;
			while (num11 > 4294967295UL && num12 != 0UL)
			{
				ulong num13 = num11 % num12;
				num11 = num12;
				num12 = num13;
			}
			if (num12 != 0UL)
			{
				uint num14 = (uint)num11;
				uint num16;
				for (uint num15 = (uint)num12; num15 != 0u; num15 = num16)
				{
					num16 = num14 % num15;
					num14 = num15;
				}
				UInt128.Create(out c, (long)((ulong)num14));
				return;
			}
			UInt128.Create(out c, num11);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000761C File Offset: 0x0000581C
		private static void AddProducts(out UInt128 result, long x, ref UInt128 u, long y, ref UInt128 v)
		{
			UInt128 @uint;
			UInt128.Multiply(out @uint, ref u, (ulong)x);
			UInt128 uint2;
			UInt128.Multiply(out uint2, ref v, (ulong)(-(ulong)y));
			UInt128.Subtract(out result, ref @uint, ref uint2);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000376F File Offset: 0x0000196F
		public static int Compare(UInt128 a, UInt128 b)
		{
			return a.CompareTo(b);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007648 File Offset: 0x00005848
		private static int GetBitLength(uint value)
		{
			uint num = value >> 16;
			if (num != 0u)
			{
				uint num2 = num >> 8;
				if (num2 != 0u)
				{
					return (int)(UInt128.bitLength[(int)num2] + 24);
				}
				return (int)(UInt128.bitLength[(int)num] + 16);
			}
			else
			{
				uint num3 = value >> 8;
				if (num3 != 0u)
				{
					return (int)(UInt128.bitLength[(int)num3] + 8);
				}
				return (int)UInt128.bitLength[(int)value];
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007694 File Offset: 0x00005894
		private static int GetBitLength(ulong value)
		{
			ulong num = value >> 32;
			if (num != 0UL)
			{
				return UInt128.GetBitLength((uint)num) + 32;
			}
			return UInt128.GetBitLength((uint)value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000076BC File Offset: 0x000058BC
		public static void Reduce(out UInt128 w, ref UInt128 u, ref UInt128 v, ref UInt128 n, ulong k0)
		{
			UInt128 @uint;
			UInt128.Multiply64(out @uint, u.s0, v.s0);
			ulong num = @uint.s0;
			UInt128.Multiply64(out @uint, u.s1, v.s0, @uint.s1);
			ulong b = @uint.s0;
			ulong num2 = @uint.s1;
			ulong u2 = num * k0;
			UInt128.Multiply64(out @uint, u2, n.s1, UInt128.MultiplyHigh64(u2, n.s0, num));
			UInt128.Add(ref @uint, b);
			num = @uint.s0;
			UInt128.Add(out @uint, @uint.s1, num2);
			b = @uint.s0;
			num2 = @uint.s1;
			UInt128.Multiply64(out @uint, u.s0, v.s1, num);
			num = @uint.s0;
			UInt128.Multiply64(out @uint, u.s1, v.s1, @uint.s1);
			UInt128.Add(ref @uint, b);
			b = @uint.s0;
			UInt128.Add(out @uint, @uint.s1, num2);
			num2 = @uint.s0;
			ulong num3 = @uint.s1;
			u2 = num * k0;
			UInt128.Multiply64(out @uint, u2, n.s1, UInt128.MultiplyHigh64(u2, n.s0, num));
			UInt128.Add(ref @uint, b);
			num = @uint.s0;
			UInt128.Add(out @uint, @uint.s1, num2);
			b = @uint.s0;
			num2 = num3 + @uint.s1;
			UInt128.Create(out w, num, b);
			if (num2 != 0UL || !UInt128.LessThan(ref w, ref n))
			{
				UInt128.Subtract(ref w, ref n);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007820 File Offset: 0x00005A20
		public static void Reduce(out UInt128 w, ref UInt128 t, ref UInt128 n, ulong k0)
		{
			ulong num = t.s0;
			ulong b = t.s1;
			ulong num2 = 0UL;
			for (int i = 0; i < 2; i++)
			{
				ulong u = num * k0;
				UInt128 @uint;
				UInt128.Multiply64(out @uint, u, n.s1, UInt128.MultiplyHigh64(u, n.s0, num));
				UInt128.Add(ref @uint, b);
				num = @uint.s0;
				UInt128.Add(out @uint, @uint.s1, num2);
				b = @uint.s0;
				num2 = @uint.s1;
			}
			UInt128.Create(out w, num, b);
			if (num2 != 0UL || !UInt128.LessThan(ref w, ref n))
			{
				UInt128.Subtract(ref w, ref n);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000078C0 File Offset: 0x00005AC0
		public static UInt128 Reduce(UInt128 u, UInt128 v, UInt128 n, ulong k0)
		{
			UInt128 result;
			UInt128.Reduce(out result, ref u, ref v, ref n, k0);
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000078DC File Offset: 0x00005ADC
		public static UInt128 Reduce(UInt128 t, UInt128 n, ulong k0)
		{
			UInt128 result;
			UInt128.Reduce(out result, ref t, ref n, k0);
			return result;
		}

		// Token: 0x04000007 RID: 7
		private ulong s0;

		// Token: 0x04000008 RID: 8
		private ulong s1;

		// Token: 0x04000009 RID: 9
		private static readonly UInt128 maxValue = ~(UInt128)0;

		// Token: 0x0400000A RID: 10
		private static readonly UInt128 zero = (UInt128)0;

		// Token: 0x0400000B RID: 11
		private static readonly UInt128 one = (UInt128)1;

		// Token: 0x0400000C RID: 12
		private const int maxRepShift = 53;

		// Token: 0x0400000D RID: 13
		private static readonly ulong maxRep = 9007199254740992UL;

		// Token: 0x0400000E RID: 14
		private static readonly UInt128 maxRepSquaredHigh = 4398046511104UL;

		// Token: 0x0400000F RID: 15
		private static byte[] bitLength = Enumerable.Range(0, 256).Select(delegate(int value)
		{
			int num = 0;
			while (value != 0)
			{
				value >>= 1;
				num++;
			}
			return (byte)num;
		}).ToArray<byte>();

		// Token: 0x02000004 RID: 4
		private struct UInt256
		{
			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000214 RID: 532 RVA: 0x00003779 File Offset: 0x00001979
			public uint r0
			{
				get
				{
					return (uint)this.s0;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000215 RID: 533 RVA: 0x00003782 File Offset: 0x00001982
			public uint r1
			{
				get
				{
					return (uint)(this.s0 >> 32);
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000216 RID: 534 RVA: 0x0000378E File Offset: 0x0000198E
			public uint r2
			{
				get
				{
					return (uint)this.s1;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000217 RID: 535 RVA: 0x00003797 File Offset: 0x00001997
			public uint r3
			{
				get
				{
					return (uint)(this.s1 >> 32);
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000218 RID: 536 RVA: 0x000037A3 File Offset: 0x000019A3
			public uint r4
			{
				get
				{
					return (uint)this.s2;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000219 RID: 537 RVA: 0x000037AC File Offset: 0x000019AC
			public uint r5
			{
				get
				{
					return (uint)(this.s2 >> 32);
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600021A RID: 538 RVA: 0x000037B8 File Offset: 0x000019B8
			public uint r6
			{
				get
				{
					return (uint)this.s3;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600021B RID: 539 RVA: 0x000037C1 File Offset: 0x000019C1
			public uint r7
			{
				get
				{
					return (uint)(this.s3 >> 32);
				}
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600021C RID: 540 RVA: 0x00007978 File Offset: 0x00005B78
			public UInt128 t0
			{
				get
				{
					UInt128 result;
					UInt128.Create(out result, this.s0, this.s1);
					return result;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0000799C File Offset: 0x00005B9C
			public UInt128 t1
			{
				get
				{
					UInt128 result;
					UInt128.Create(out result, this.s2, this.s3);
					return result;
				}
			}

			// Token: 0x0600021E RID: 542 RVA: 0x000079C0 File Offset: 0x00005BC0
			public static implicit operator BigInteger(UInt128.UInt256 a)
			{
				return a.s3 << 192 | a.s2 << 128 | a.s1 << 64 | a.s0;
			}

			// Token: 0x0600021F RID: 543 RVA: 0x00007A24 File Offset: 0x00005C24
			public override string ToString()
			{
				return this.ToString();
			}

			// Token: 0x04000010 RID: 16
			public ulong s0;

			// Token: 0x04000011 RID: 17
			public ulong s1;

			// Token: 0x04000012 RID: 18
			public ulong s2;

			// Token: 0x04000013 RID: 19
			public ulong s3;
		}
	}
}
