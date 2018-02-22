using System;
using System.Globalization;
using System.Numerics;

namespace Dirichlet.Numerics
{
	// Token: 0x02000002 RID: 2
	public struct Int128 : IComparable<Int128>, IEquatable<Int128>, IFormattable, IComparable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000216A File Offset: 0x0000036A
		public static Int128 MinValue
		{
			get
			{
				return Int128.minValue;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002171 File Offset: 0x00000371
		public static Int128 MaxValue
		{
			get
			{
				return Int128.maxValue;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		public static Int128 Zero
		{
			get
			{
				return Int128.zero;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000217F File Offset: 0x0000037F
		public static Int128 One
		{
			get
			{
				return Int128.one;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002186 File Offset: 0x00000386
		public static Int128 MinusOne
		{
			get
			{
				return Int128.minusOne;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00003E84 File Offset: 0x00002084
		public static Int128 Parse(string value)
		{
			Int128 result;
			if (!Int128.TryParse(value, out result))
			{
				throw new FormatException();
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000218D File Offset: 0x0000038D
		public static bool TryParse(string value, out Int128 result)
		{
			return Int128.TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003EA4 File Offset: 0x000020A4
		public static bool TryParse(string value, NumberStyles style, IFormatProvider format, out Int128 result)
		{
			BigInteger a;
			if (!BigInteger.TryParse(value, style, format, out a))
			{
				result = Int128.Zero;
				return false;
			}
			UInt128.Create(out result.v, a);
			return true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
		public Int128(long value)
		{
			UInt128.Create(out this.v, value);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021AA File Offset: 0x000003AA
		public Int128(ulong value)
		{
			UInt128.Create(out this.v, value);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B8 File Offset: 0x000003B8
		public Int128(double value)
		{
			UInt128.Create(out this.v, value);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C6 File Offset: 0x000003C6
		public Int128(decimal value)
		{
			UInt128.Create(out this.v, value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D4 File Offset: 0x000003D4
		public Int128(BigInteger value)
		{
			UInt128.Create(out this.v, value);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021E2 File Offset: 0x000003E2
		public ulong S0
		{
			get
			{
				return this.v.S0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021EF File Offset: 0x000003EF
		public ulong S1
		{
			get
			{
				return this.v.S1;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
		public bool IsZero
		{
			get
			{
				return this.v.IsZero;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002209 File Offset: 0x00000409
		public bool IsOne
		{
			get
			{
				return this.v.IsOne;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002216 File Offset: 0x00000416
		public bool IsPowerOfTwo
		{
			get
			{
				return this.v.IsPowerOfTwo;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002223 File Offset: 0x00000423
		public bool IsEven
		{
			get
			{
				return this.v.IsEven;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002230 File Offset: 0x00000430
		public bool IsNegative
		{
			get
			{
				return this.v.S1 > 9223372036854775807UL;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002248 File Offset: 0x00000448
		public int Sign
		{
			get
			{
				if (!this.IsNegative)
				{
					return this.v.Sign;
				}
				return -1;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003ED8 File Offset: 0x000020D8
		public override string ToString()
		{
			return this.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003F00 File Offset: 0x00002100
		public string ToString(string format)
		{
			return this.ToString(format);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000225F File Offset: 0x0000045F
		public string ToString(IFormatProvider provider)
		{
			return this.ToString(null, provider);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003F24 File Offset: 0x00002124
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, provider);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003F48 File Offset: 0x00002148
		public static explicit operator Int128(double a)
		{
			Int128 result;
			UInt128.Create(out result.v, a);
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003F64 File Offset: 0x00002164
		public static implicit operator Int128(sbyte a)
		{
			Int128 result;
			UInt128.Create(out result.v, (long)a);
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003F80 File Offset: 0x00002180
		public static implicit operator Int128(byte a)
		{
			Int128 result;
			UInt128.Create(out result.v, (long)((ulong)a));
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003F64 File Offset: 0x00002164
		public static implicit operator Int128(short a)
		{
			Int128 result;
			UInt128.Create(out result.v, (long)a);
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003F80 File Offset: 0x00002180
		public static implicit operator Int128(ushort a)
		{
			Int128 result;
			UInt128.Create(out result.v, (long)((ulong)a));
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003F64 File Offset: 0x00002164
		public static implicit operator Int128(int a)
		{
			Int128 result;
			UInt128.Create(out result.v, (long)a);
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003F9C File Offset: 0x0000219C
		public static implicit operator Int128(uint a)
		{
			Int128 result;
			UInt128.Create(out result.v, (ulong)a);
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003FB8 File Offset: 0x000021B8
		public static implicit operator Int128(long a)
		{
			Int128 result;
			UInt128.Create(out result.v, a);
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003FD4 File Offset: 0x000021D4
		public static implicit operator Int128(ulong a)
		{
			Int128 result;
			UInt128.Create(out result.v, a);
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003FF0 File Offset: 0x000021F0
		public static explicit operator Int128(decimal a)
		{
			Int128 result;
			UInt128.Create(out result.v, a);
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000400C File Offset: 0x0000220C
		public static explicit operator Int128(UInt128 a)
		{
			Int128 result;
			result.v = a;
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002269 File Offset: 0x00000469
		public static explicit operator UInt128(Int128 a)
		{
			return a.v;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004024 File Offset: 0x00002224
		public static explicit operator Int128(BigInteger a)
		{
			Int128 result;
			UInt128.Create(out result.v, a);
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002271 File Offset: 0x00000471
		public static explicit operator sbyte(Int128 a)
		{
			return (sbyte)a.v.S0;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002280 File Offset: 0x00000480
		public static explicit operator byte(Int128 a)
		{
			return (byte)a.v.S0;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000228F File Offset: 0x0000048F
		public static explicit operator short(Int128 a)
		{
			return (short)a.v.S0;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000229E File Offset: 0x0000049E
		public static explicit operator ushort(Int128 a)
		{
			return (ushort)a.v.S0;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000022AD File Offset: 0x000004AD
		public static explicit operator int(Int128 a)
		{
			return (int)a.v.S0;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000022BC File Offset: 0x000004BC
		public static explicit operator uint(Int128 a)
		{
			return (uint)a.v.S0;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000022CB File Offset: 0x000004CB
		public static explicit operator long(Int128 a)
		{
			return (long)a.v.S0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000022CB File Offset: 0x000004CB
		public static explicit operator ulong(Int128 a)
		{
			return a.v.S0;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004040 File Offset: 0x00002240
		public static explicit operator decimal(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 a2;
				UInt128.Negate(out a2, ref a.v);
				return -(decimal)a2;
			}
			return (decimal)a.v;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000407C File Offset: 0x0000227C
		public static implicit operator BigInteger(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 a2;
				UInt128.Negate(out a2, ref a.v);
				return -a2;
			}
			return a.v;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000040B8 File Offset: 0x000022B8
		public static explicit operator float(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				return -UInt128.ConvertToFloat(ref @uint);
			}
			return UInt128.ConvertToFloat(ref a.v);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000040F4 File Offset: 0x000022F4
		public static explicit operator double(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				return -UInt128.ConvertToDouble(ref @uint);
			}
			return UInt128.ConvertToDouble(ref a.v);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004130 File Offset: 0x00002330
		public static Int128 operator <<(Int128 a, int b)
		{
			Int128 result;
			UInt128.LeftShift(out result.v, ref a.v, b);
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004154 File Offset: 0x00002354
		public static Int128 operator >>(Int128 a, int b)
		{
			Int128 result;
			UInt128.ArithmeticRightShift(out result.v, ref a.v, b);
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004178 File Offset: 0x00002378
		public static Int128 operator &(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.And(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000022D9 File Offset: 0x000004D9
		public static int operator &(Int128 a, int b)
		{
			return (int)(a.v & (uint)b);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000022E7 File Offset: 0x000004E7
		public static int operator &(int a, Int128 b)
		{
			return (int)(b.v & (uint)a);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022F5 File Offset: 0x000004F5
		public static long operator &(Int128 a, long b)
		{
			return (long)(a.v & (ulong)b);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002303 File Offset: 0x00000503
		public static long operator &(long a, Int128 b)
		{
			return (long)(b.v & (ulong)a);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000041A0 File Offset: 0x000023A0
		public static Int128 operator |(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.Or(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000041C8 File Offset: 0x000023C8
		public static Int128 operator ^(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.ExclusiveOr(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000041F0 File Offset: 0x000023F0
		public static Int128 operator ~(Int128 a)
		{
			Int128 result;
			UInt128.Not(out result.v, ref a.v);
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004214 File Offset: 0x00002414
		public static Int128 operator +(Int128 a, long b)
		{
			Int128 result;
			if (b < 0L)
			{
				UInt128.Subtract(out result.v, ref a.v, (ulong)(-(ulong)b));
			}
			else
			{
				UInt128.Add(out result.v, ref a.v, (ulong)b);
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000425C File Offset: 0x0000245C
		public static Int128 operator +(long a, Int128 b)
		{
			Int128 result;
			if (a < 0L)
			{
				UInt128.Subtract(out result.v, ref b.v, (ulong)(-(ulong)a));
			}
			else
			{
				UInt128.Add(out result.v, ref b.v, (ulong)a);
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000042A4 File Offset: 0x000024A4
		public static Int128 operator +(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.Add(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000042CC File Offset: 0x000024CC
		public static Int128 operator ++(Int128 a)
		{
			Int128 result;
			UInt128.Add(out result.v, ref a.v, 1UL);
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000042F8 File Offset: 0x000024F8
		public static Int128 operator -(Int128 a, long b)
		{
			Int128 result;
			if (b < 0L)
			{
				UInt128.Add(out result.v, ref a.v, (ulong)(-(ulong)b));
			}
			else
			{
				UInt128.Subtract(out result.v, ref a.v, (ulong)b);
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004340 File Offset: 0x00002540
		public static Int128 operator -(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.Subtract(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002311 File Offset: 0x00000511
		public static Int128 operator +(Int128 a)
		{
			return a;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004368 File Offset: 0x00002568
		public static Int128 operator -(Int128 a)
		{
			Int128 result;
			UInt128.Negate(out result.v, ref a.v);
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000438C File Offset: 0x0000258C
		public static Int128 operator --(Int128 a)
		{
			Int128 result;
			UInt128.Subtract(out result.v, ref a.v, 1UL);
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000043B8 File Offset: 0x000025B8
		public static Int128 operator *(Int128 a, int b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000043D0 File Offset: 0x000025D0
		public static Int128 operator *(int a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000043E8 File Offset: 0x000025E8
		public static Int128 operator *(Int128 a, uint b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004400 File Offset: 0x00002600
		public static Int128 operator *(uint a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004418 File Offset: 0x00002618
		public static Int128 operator *(Int128 a, long b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004430 File Offset: 0x00002630
		public static Int128 operator *(long a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004448 File Offset: 0x00002648
		public static Int128 operator *(Int128 a, ulong b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, b);
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004460 File Offset: 0x00002660
		public static Int128 operator *(ulong a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref b, a);
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004478 File Offset: 0x00002678
		public static Int128 operator *(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004494 File Offset: 0x00002694
		public static Int128 operator /(Int128 a, int b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, b);
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000044AC File Offset: 0x000026AC
		public static Int128 operator /(Int128 a, uint b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, b);
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000044C4 File Offset: 0x000026C4
		public static Int128 operator /(Int128 a, long b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, b);
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000044DC File Offset: 0x000026DC
		public static Int128 operator /(Int128 a, ulong b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, b);
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000044F4 File Offset: 0x000026F4
		public static Int128 operator /(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, ref b);
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002314 File Offset: 0x00000514
		public static int operator %(Int128 a, int b)
		{
			return Int128.Remainder(ref a, b);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000231E File Offset: 0x0000051E
		public static int operator %(Int128 a, uint b)
		{
			return Int128.Remainder(ref a, b);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002328 File Offset: 0x00000528
		public static long operator %(Int128 a, long b)
		{
			return Int128.Remainder(ref a, b);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002332 File Offset: 0x00000532
		public static long operator %(Int128 a, ulong b)
		{
			return Int128.Remainder(ref a, b);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004510 File Offset: 0x00002710
		public static Int128 operator %(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Remainder(out result, ref a, ref b);
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000233C File Offset: 0x0000053C
		public static bool operator <(Int128 a, UInt128 b)
		{
			return a.CompareTo(b) < 0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002349 File Offset: 0x00000549
		public static bool operator <(UInt128 a, Int128 b)
		{
			return b.CompareTo(a) > 0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002356 File Offset: 0x00000556
		public static bool operator <(Int128 a, Int128 b)
		{
			return Int128.LessThan(ref a.v, ref b.v);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000236B File Offset: 0x0000056B
		public static bool operator <(Int128 a, int b)
		{
			return Int128.LessThan(ref a.v, (long)b);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000237B File Offset: 0x0000057B
		public static bool operator <(int a, Int128 b)
		{
			return Int128.LessThan((long)a, ref b.v);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000238B File Offset: 0x0000058B
		public static bool operator <(Int128 a, uint b)
		{
			return Int128.LessThan(ref a.v, (long)((ulong)b));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000239B File Offset: 0x0000059B
		public static bool operator <(uint a, Int128 b)
		{
			return Int128.LessThan((long)((ulong)a), ref b.v);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000023AB File Offset: 0x000005AB
		public static bool operator <(Int128 a, long b)
		{
			return Int128.LessThan(ref a.v, b);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000023BA File Offset: 0x000005BA
		public static bool operator <(long a, Int128 b)
		{
			return Int128.LessThan(a, ref b.v);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000023C9 File Offset: 0x000005C9
		public static bool operator <(Int128 a, ulong b)
		{
			return Int128.LessThan(ref a.v, b);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000023D8 File Offset: 0x000005D8
		public static bool operator <(ulong a, Int128 b)
		{
			return Int128.LessThan(a, ref b.v);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000023E7 File Offset: 0x000005E7
		public static bool operator <=(Int128 a, UInt128 b)
		{
			return a.CompareTo(b) <= 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000023F7 File Offset: 0x000005F7
		public static bool operator <=(UInt128 a, Int128 b)
		{
			return b.CompareTo(a) >= 0;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002407 File Offset: 0x00000607
		public static bool operator <=(Int128 a, Int128 b)
		{
			return !Int128.LessThan(ref b.v, ref a.v);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000241F File Offset: 0x0000061F
		public static bool operator <=(Int128 a, int b)
		{
			return !Int128.LessThan((long)b, ref a.v);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002432 File Offset: 0x00000632
		public static bool operator <=(int a, Int128 b)
		{
			return !Int128.LessThan(ref b.v, (long)a);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002445 File Offset: 0x00000645
		public static bool operator <=(Int128 a, uint b)
		{
			return !Int128.LessThan((long)((ulong)b), ref a.v);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002458 File Offset: 0x00000658
		public static bool operator <=(uint a, Int128 b)
		{
			return !Int128.LessThan(ref b.v, (long)((ulong)a));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000246B File Offset: 0x0000066B
		public static bool operator <=(Int128 a, long b)
		{
			return !Int128.LessThan(b, ref a.v);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000247D File Offset: 0x0000067D
		public static bool operator <=(long a, Int128 b)
		{
			return !Int128.LessThan(ref b.v, a);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000248F File Offset: 0x0000068F
		public static bool operator <=(Int128 a, ulong b)
		{
			return !Int128.LessThan(b, ref a.v);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000024A1 File Offset: 0x000006A1
		public static bool operator <=(ulong a, Int128 b)
		{
			return !Int128.LessThan(ref b.v, a);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000024B3 File Offset: 0x000006B3
		public static bool operator >(Int128 a, UInt128 b)
		{
			return a.CompareTo(b) > 0;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000024C0 File Offset: 0x000006C0
		public static bool operator >(UInt128 a, Int128 b)
		{
			return b.CompareTo(a) < 0;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000024CD File Offset: 0x000006CD
		public static bool operator >(Int128 a, Int128 b)
		{
			return Int128.LessThan(ref b.v, ref a.v);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000024E2 File Offset: 0x000006E2
		public static bool operator >(Int128 a, int b)
		{
			return Int128.LessThan((long)b, ref a.v);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000024F2 File Offset: 0x000006F2
		public static bool operator >(int a, Int128 b)
		{
			return Int128.LessThan(ref b.v, (long)a);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002502 File Offset: 0x00000702
		public static bool operator >(Int128 a, uint b)
		{
			return Int128.LessThan((long)((ulong)b), ref a.v);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002512 File Offset: 0x00000712
		public static bool operator >(uint a, Int128 b)
		{
			return Int128.LessThan(ref b.v, (long)((ulong)a));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002522 File Offset: 0x00000722
		public static bool operator >(Int128 a, long b)
		{
			return Int128.LessThan(b, ref a.v);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002531 File Offset: 0x00000731
		public static bool operator >(long a, Int128 b)
		{
			return Int128.LessThan(ref b.v, a);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002540 File Offset: 0x00000740
		public static bool operator >(Int128 a, ulong b)
		{
			return Int128.LessThan(b, ref a.v);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000254F File Offset: 0x0000074F
		public static bool operator >(ulong a, Int128 b)
		{
			return Int128.LessThan(ref b.v, a);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000255E File Offset: 0x0000075E
		public static bool operator >=(Int128 a, UInt128 b)
		{
			return a.CompareTo(b) >= 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000256E File Offset: 0x0000076E
		public static bool operator >=(UInt128 a, Int128 b)
		{
			return b.CompareTo(a) <= 0;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000257E File Offset: 0x0000077E
		public static bool operator >=(Int128 a, Int128 b)
		{
			return !Int128.LessThan(ref a.v, ref b.v);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002596 File Offset: 0x00000796
		public static bool operator >=(Int128 a, int b)
		{
			return !Int128.LessThan(ref a.v, (long)b);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000025A9 File Offset: 0x000007A9
		public static bool operator >=(int a, Int128 b)
		{
			return !Int128.LessThan((long)a, ref b.v);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000025BC File Offset: 0x000007BC
		public static bool operator >=(Int128 a, uint b)
		{
			return !Int128.LessThan(ref a.v, (long)((ulong)b));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000025CF File Offset: 0x000007CF
		public static bool operator >=(uint a, Int128 b)
		{
			return !Int128.LessThan((long)((ulong)a), ref b.v);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000025E2 File Offset: 0x000007E2
		public static bool operator >=(Int128 a, long b)
		{
			return !Int128.LessThan(ref a.v, b);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000025F4 File Offset: 0x000007F4
		public static bool operator >=(long a, Int128 b)
		{
			return !Int128.LessThan(a, ref b.v);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002606 File Offset: 0x00000806
		public static bool operator >=(Int128 a, ulong b)
		{
			return !Int128.LessThan(ref a.v, b);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002618 File Offset: 0x00000818
		public static bool operator >=(ulong a, Int128 b)
		{
			return !Int128.LessThan(a, ref b.v);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000262A File Offset: 0x0000082A
		public static bool operator ==(UInt128 a, Int128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002634 File Offset: 0x00000834
		public static bool operator ==(Int128 a, UInt128 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000263E File Offset: 0x0000083E
		public static bool operator ==(Int128 a, Int128 b)
		{
			return a.v.Equals(b.v);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002652 File Offset: 0x00000852
		public static bool operator ==(Int128 a, int b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000265C File Offset: 0x0000085C
		public static bool operator ==(int a, Int128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002666 File Offset: 0x00000866
		public static bool operator ==(Int128 a, uint b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002670 File Offset: 0x00000870
		public static bool operator ==(uint a, Int128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000267A File Offset: 0x0000087A
		public static bool operator ==(Int128 a, long b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002684 File Offset: 0x00000884
		public static bool operator ==(long a, Int128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000268E File Offset: 0x0000088E
		public static bool operator ==(Int128 a, ulong b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002698 File Offset: 0x00000898
		public static bool operator ==(ulong a, Int128 b)
		{
			return b.Equals(a);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000026A2 File Offset: 0x000008A2
		public static bool operator !=(UInt128 a, Int128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000026AF File Offset: 0x000008AF
		public static bool operator !=(Int128 a, UInt128 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000026BC File Offset: 0x000008BC
		public static bool operator !=(Int128 a, Int128 b)
		{
			return !a.v.Equals(b.v);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000026D3 File Offset: 0x000008D3
		public static bool operator !=(Int128 a, int b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000026E0 File Offset: 0x000008E0
		public static bool operator !=(int a, Int128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000026ED File Offset: 0x000008ED
		public static bool operator !=(Int128 a, uint b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000026FA File Offset: 0x000008FA
		public static bool operator !=(uint a, Int128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002707 File Offset: 0x00000907
		public static bool operator !=(Int128 a, long b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002714 File Offset: 0x00000914
		public static bool operator !=(long a, Int128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002721 File Offset: 0x00000921
		public static bool operator !=(Int128 a, ulong b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000272E File Offset: 0x0000092E
		public static bool operator !=(ulong a, Int128 b)
		{
			return !b.Equals(a);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000273B File Offset: 0x0000093B
		public int CompareTo(UInt128 other)
		{
			if (this.IsNegative)
			{
				return -1;
			}
			return this.v.CompareTo(other);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002753 File Offset: 0x00000953
		public int CompareTo(Int128 other)
		{
			return Int128.SignedCompare(ref this.v, other.S0, other.S1);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000276E File Offset: 0x0000096E
		public int CompareTo(int other)
		{
			return Int128.SignedCompare(ref this.v, (ulong)((long)other), (ulong)((long)(other >> 31)));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002782 File Offset: 0x00000982
		public int CompareTo(uint other)
		{
			return Int128.SignedCompare(ref this.v, (ulong)other, 0UL);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000279A File Offset: 0x0000099A
		public int CompareTo(long other)
		{
			return Int128.SignedCompare(ref this.v, (ulong)other, (ulong)(other >> 63));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000027AC File Offset: 0x000009AC
		public int CompareTo(ulong other)
		{
			return Int128.SignedCompare(ref this.v, other, 0UL);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000027C3 File Offset: 0x000009C3
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Int128))
			{
				throw new ArgumentException();
			}
			return this.CompareTo((Int128)obj);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000452C File Offset: 0x0000272C
		private static bool LessThan(ref UInt128 a, ref UInt128 b)
		{
			long s = (long)a.S1;
			long s2 = (long)b.S1;
			if (s != s2)
			{
				return s < s2;
			}
			return a.S0 < b.S0;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004560 File Offset: 0x00002760
		private static bool LessThan(ref UInt128 a, long b)
		{
			long s = (long)a.S1;
			long num = b >> 63;
			if (s != num)
			{
				return s < num;
			}
			return a.S0 < (ulong)b;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000458C File Offset: 0x0000278C
		private static bool LessThan(long a, ref UInt128 b)
		{
			long num = a >> 63;
			long s = (long)b.S1;
			if (num != s)
			{
				return num < s;
			}
			return a < (long)b.S0;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000045B8 File Offset: 0x000027B8
		private static bool LessThan(ref UInt128 a, ulong b)
		{
			long s = (long)a.S1;
			if (s != 0L)
			{
				return s < 0L;
			}
			return a.S0 < b;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045E8 File Offset: 0x000027E8
		private static bool LessThan(ulong a, ref UInt128 b)
		{
			long s = (long)b.S1;
			if (s != 0L)
			{
				return 0L < s;
			}
			return a < b.S0;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004618 File Offset: 0x00002818
		private static int SignedCompare(ref UInt128 a, ulong bs0, ulong bs1)
		{
			ulong s = a.S1;
			if (s != bs1)
			{
				long num = (long)s;
				return num.CompareTo((long)bs1);
			}
			return a.S0.CompareTo(bs0);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool Equals(UInt128 other)
		{
			return !this.IsNegative && this.v.Equals(other);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000027FC File Offset: 0x000009FC
		public bool Equals(Int128 other)
		{
			return this.v.Equals(other.v);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000464C File Offset: 0x0000284C
		public bool Equals(int other)
		{
			if (other < 0)
			{
				return this.v.S1 == ulong.MaxValue && this.v.S0 == (ulong)other;
			}
			return this.v.S1 == 0UL && this.v.S0 == (ulong)other;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool Equals(uint other)
		{
			return this.v.S1 == 0UL && this.v.S0 == (ulong)other;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000046A4 File Offset: 0x000028A4
		public bool Equals(long other)
		{
			if (other < 0L)
			{
				return this.v.S1 == ulong.MaxValue && this.v.S0 == (ulong)other;
			}
			return this.v.S1 == 0UL && this.v.S0 == (ulong)other;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000282F File Offset: 0x00000A2F
		public bool Equals(ulong other)
		{
			return this.v.S1 == 0UL && this.v.S0 == other;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000284E File Offset: 0x00000A4E
		public override bool Equals(object obj)
		{
			return obj is Int128 && this.Equals((Int128)obj);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002866 File Offset: 0x00000A66
		public override int GetHashCode()
		{
			return this.v.GetHashCode();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004704 File Offset: 0x00002904
		public static void Multiply(out Int128 c, ref Int128 a, int b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0)
				{
					UInt128.Multiply(out c.v, ref @uint, (uint)(-(uint)b));
					return;
				}
				UInt128.Multiply(out c.v, ref @uint, (uint)b);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b < 0)
				{
					UInt128.Multiply(out c.v, ref a.v, (uint)(-(uint)b));
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Multiply(out c.v, ref a.v, (uint)b);
				return;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000478C File Offset: 0x0000298C
		public static void Multiply(out Int128 c, ref Int128 a, uint b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Multiply(out c.v, ref @uint, b);
				UInt128.Negate(ref c.v);
				return;
			}
			UInt128.Multiply(out c.v, ref a.v, b);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000047DC File Offset: 0x000029DC
		public static void Multiply(out Int128 c, ref Int128 a, long b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0L)
				{
					UInt128.Multiply(out c.v, ref @uint, (ulong)(-(ulong)b));
					return;
				}
				UInt128.Multiply(out c.v, ref @uint, (ulong)b);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b < 0L)
				{
					UInt128.Multiply(out c.v, ref a.v, (ulong)(-(ulong)b));
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Multiply(out c.v, ref a.v, (ulong)b);
				return;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004874 File Offset: 0x00002A74
		public static void Multiply(out Int128 c, ref Int128 a, ulong b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Multiply(out c.v, ref @uint, b);
				UInt128.Negate(ref c.v);
				return;
			}
			UInt128.Multiply(out c.v, ref a.v, b);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000048C4 File Offset: 0x00002AC4
		public static void Multiply(out Int128 c, ref Int128 a, ref Int128 b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b.IsNegative)
				{
					UInt128 uint2;
					UInt128.Negate(out uint2, ref b.v);
					UInt128.Multiply(out c.v, ref @uint, ref uint2);
					return;
				}
				UInt128.Multiply(out c.v, ref @uint, ref b.v);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b.IsNegative)
				{
					UInt128 uint3;
					UInt128.Negate(out uint3, ref b.v);
					UInt128.Multiply(out c.v, ref a.v, ref uint3);
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Multiply(out c.v, ref a.v, ref b.v);
				return;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004704 File Offset: 0x00002904
		public static void Divide(out Int128 c, ref Int128 a, int b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0)
				{
					UInt128.Multiply(out c.v, ref @uint, (uint)(-(uint)b));
					return;
				}
				UInt128.Multiply(out c.v, ref @uint, (uint)b);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b < 0)
				{
					UInt128.Multiply(out c.v, ref a.v, (uint)(-(uint)b));
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Multiply(out c.v, ref a.v, (uint)b);
				return;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004978 File Offset: 0x00002B78
		public static void Divide(out Int128 c, ref Int128 a, uint b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Divide(out c.v, ref @uint, b);
				UInt128.Negate(ref c.v);
				return;
			}
			UInt128.Divide(out c.v, ref a.v, b);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000049C8 File Offset: 0x00002BC8
		public static void Divide(out Int128 c, ref Int128 a, long b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0L)
				{
					UInt128.Divide(out c.v, ref @uint, (ulong)(-(ulong)b));
					return;
				}
				UInt128.Divide(out c.v, ref @uint, (ulong)b);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b < 0L)
				{
					UInt128.Divide(out c.v, ref a.v, (ulong)(-(ulong)b));
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Divide(out c.v, ref a.v, (ulong)b);
				return;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004A60 File Offset: 0x00002C60
		public static void Divide(out Int128 c, ref Int128 a, ulong b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Divide(out c.v, ref @uint, b);
				UInt128.Negate(ref c.v);
				return;
			}
			UInt128.Divide(out c.v, ref a.v, b);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public static void Divide(out Int128 c, ref Int128 a, ref Int128 b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b.IsNegative)
				{
					UInt128 uint2;
					UInt128.Negate(out uint2, ref b.v);
					UInt128.Divide(out c.v, ref @uint, ref uint2);
					return;
				}
				UInt128.Divide(out c.v, ref @uint, ref b.v);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b.IsNegative)
				{
					UInt128 uint3;
					UInt128.Negate(out uint3, ref b.v);
					UInt128.Divide(out c.v, ref a.v, ref uint3);
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Divide(out c.v, ref a.v, ref b.v);
				return;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004B64 File Offset: 0x00002D64
		public static int Remainder(ref Int128 a, int b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0)
				{
					return (int)UInt128.Remainder(ref @uint, (uint)(-(uint)b));
				}
				return (int)(-(int)UInt128.Remainder(ref @uint, (uint)b));
			}
			else
			{
				if (b < 0)
				{
					return (int)(-(int)UInt128.Remainder(ref a.v, (uint)(-(uint)b)));
				}
				return (int)UInt128.Remainder(ref a.v, (uint)b);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public static int Remainder(ref Int128 a, uint b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				return (int)(-(int)UInt128.Remainder(ref @uint, b));
			}
			return (int)UInt128.Remainder(ref a.v, b);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public static long Remainder(ref Int128 a, long b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b < 0L)
				{
					return (long)UInt128.Remainder(ref @uint, (ulong)(-(ulong)b));
				}
				return (long)(-(long)UInt128.Remainder(ref @uint, (ulong)b));
			}
			else
			{
				if (b < 0L)
				{
					return (long)(-(long)UInt128.Remainder(ref a.v, (ulong)(-(ulong)b)));
				}
				return (long)UInt128.Remainder(ref a.v, (ulong)b);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004C64 File Offset: 0x00002E64
		public static long Remainder(ref Int128 a, ulong b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				return (long)(-(long)UInt128.Remainder(ref @uint, b));
			}
			return (long)UInt128.Remainder(ref a.v, b);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004C9C File Offset: 0x00002E9C
		public static void Remainder(out Int128 c, ref Int128 a, ref Int128 b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b.IsNegative)
				{
					UInt128 uint2;
					UInt128.Negate(out uint2, ref b.v);
					UInt128.Remainder(out c.v, ref @uint, ref uint2);
					return;
				}
				UInt128.Remainder(out c.v, ref @uint, ref b.v);
				UInt128.Negate(ref c.v);
				return;
			}
			else
			{
				if (b.IsNegative)
				{
					UInt128 uint3;
					UInt128.Negate(out uint3, ref b.v);
					UInt128.Remainder(out c.v, ref a.v, ref uint3);
					UInt128.Negate(ref c.v);
					return;
				}
				UInt128.Remainder(out c.v, ref a.v, ref b.v);
				return;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D50 File Offset: 0x00002F50
		public static Int128 Abs(Int128 a)
		{
			if (!a.IsNegative)
			{
				return a;
			}
			Int128 result;
			UInt128.Negate(out result.v, ref a.v);
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D7C File Offset: 0x00002F7C
		public static Int128 Square(long a)
		{
			if (a < 0L)
			{
				a = -a;
			}
			Int128 result;
			UInt128.Square(out result.v, (ulong)a);
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public static Int128 Square(Int128 a)
		{
			Int128 result;
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Square(out result.v, ref @uint);
			}
			else
			{
				UInt128.Square(out result.v, ref a.v);
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static Int128 Cube(long a)
		{
			Int128 result;
			if (a < 0L)
			{
				UInt128.Cube(out result.v, (ulong)(-(ulong)a));
				UInt128.Negate(ref result.v);
			}
			else
			{
				UInt128.Cube(out result.v, (ulong)a);
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004E34 File Offset: 0x00003034
		public static Int128 Cube(Int128 a)
		{
			Int128 result;
			if (a < 0)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				UInt128.Cube(out result.v, ref @uint);
				UInt128.Negate(ref result.v);
			}
			else
			{
				UInt128.Cube(out result.v, ref a.v);
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002879 File Offset: 0x00000A79
		public static void Add(ref Int128 a, long b)
		{
			if (b < 0L)
			{
				UInt128.Subtract(ref a.v, (ulong)(-(ulong)b));
				return;
			}
			UInt128.Add(ref a.v, (ulong)b);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000028A1 File Offset: 0x00000AA1
		public static void Add(ref Int128 a, ref Int128 b)
		{
			UInt128.Add(ref a.v, ref b.v);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000028B4 File Offset: 0x00000AB4
		public static void Subtract(ref Int128 a, long b)
		{
			if (b < 0L)
			{
				UInt128.Add(ref a.v, (ulong)(-(ulong)b));
				return;
			}
			UInt128.Subtract(ref a.v, (ulong)b);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000028DC File Offset: 0x00000ADC
		public static void Subtract(ref Int128 a, ref Int128 b)
		{
			UInt128.Subtract(ref a.v, ref b.v);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000028EF File Offset: 0x00000AEF
		public static void Add(ref Int128 a, Int128 b)
		{
			UInt128.Add(ref a.v, ref b.v);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002903 File Offset: 0x00000B03
		public static void Subtract(ref Int128 a, Int128 b)
		{
			UInt128.Subtract(ref a.v, ref b.v);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004E88 File Offset: 0x00003088
		public static void AddProduct(ref Int128 a, ref UInt128 b, int c)
		{
			UInt128 @uint;
			if (c < 0)
			{
				UInt128.Multiply(out @uint, ref b, (uint)(-(uint)c));
				UInt128.Subtract(ref a.v, ref @uint);
				return;
			}
			UInt128.Multiply(out @uint, ref b, (uint)c);
			UInt128.Add(ref a.v, ref @uint);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004EC8 File Offset: 0x000030C8
		public static void AddProduct(ref Int128 a, ref UInt128 b, long c)
		{
			UInt128 @uint;
			if (c < 0L)
			{
				UInt128.Multiply(out @uint, ref b, (ulong)(-(ulong)c));
				UInt128.Subtract(ref a.v, ref @uint);
				return;
			}
			UInt128.Multiply(out @uint, ref b, (ulong)c);
			UInt128.Add(ref a.v, ref @uint);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004F10 File Offset: 0x00003110
		public static void SubtractProduct(ref Int128 a, ref UInt128 b, int c)
		{
			UInt128 @uint;
			if (c < 0)
			{
				UInt128.Multiply(out @uint, ref b, (uint)(-(uint)c));
				UInt128.Add(ref a.v, ref @uint);
				return;
			}
			UInt128.Multiply(out @uint, ref b, (uint)c);
			UInt128.Subtract(ref a.v, ref @uint);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004F50 File Offset: 0x00003150
		public static void SubtractProduct(ref Int128 a, ref UInt128 b, long c)
		{
			UInt128 @uint;
			if (c < 0L)
			{
				UInt128.Multiply(out @uint, ref b, (ulong)(-(ulong)c));
				UInt128.Add(ref a.v, ref @uint);
				return;
			}
			UInt128.Multiply(out @uint, ref b, (ulong)c);
			UInt128.Subtract(ref a.v, ref @uint);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002917 File Offset: 0x00000B17
		public static void AddProduct(ref Int128 a, UInt128 b, int c)
		{
			Int128.AddProduct(ref a, ref b, c);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002922 File Offset: 0x00000B22
		public static void AddProduct(ref Int128 a, UInt128 b, long c)
		{
			Int128.AddProduct(ref a, ref b, c);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000292D File Offset: 0x00000B2D
		public static void SubtractProduct(ref Int128 a, UInt128 b, int c)
		{
			Int128.SubtractProduct(ref a, ref b, c);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002938 File Offset: 0x00000B38
		public static void SubtractProduct(ref Int128 a, UInt128 b, long c)
		{
			Int128.SubtractProduct(ref a, ref b, c);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004F98 File Offset: 0x00003198
		public static void Pow(out Int128 result, ref Int128 value, int exponent)
		{
			if (exponent < 0)
			{
				throw new ArgumentException("exponent must not be negative");
			}
			if (!value.IsNegative)
			{
				UInt128.Pow(out result.v, ref value.v, (uint)exponent);
				return;
			}
			UInt128 @uint;
			UInt128.Negate(out @uint, ref value.v);
			if ((exponent & 1) == 0)
			{
				UInt128.Pow(out result.v, ref @uint, (uint)exponent);
				return;
			}
			UInt128.Pow(out result.v, ref @uint, (uint)exponent);
			UInt128.Negate(ref result.v);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000500C File Offset: 0x0000320C
		public static Int128 Pow(Int128 value, int exponent)
		{
			Int128 result;
			Int128.Pow(out result, ref value, exponent);
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002943 File Offset: 0x00000B43
		public static ulong FloorSqrt(Int128 a)
		{
			if (a.IsNegative)
			{
				throw new ArgumentException("argument must not be negative");
			}
			return UInt128.FloorSqrt(a.v);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002964 File Offset: 0x00000B64
		public static ulong CeilingSqrt(Int128 a)
		{
			if (a.IsNegative)
			{
				throw new ArgumentException("argument must not be negative");
			}
			return UInt128.CeilingSqrt(a.v);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005024 File Offset: 0x00003224
		public static long FloorCbrt(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 a2;
				UInt128.Negate(out a2, ref a.v);
				return (long)(-(long)UInt128.FloorCbrt(a2));
			}
			return (long)UInt128.FloorCbrt(a.v);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000505C File Offset: 0x0000325C
		public static long CeilingCbrt(Int128 a)
		{
			if (a.IsNegative)
			{
				UInt128 a2;
				UInt128.Negate(out a2, ref a.v);
				return (long)(-(long)UInt128.CeilingCbrt(a2));
			}
			return (long)UInt128.CeilingCbrt(a.v);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002985 File Offset: 0x00000B85
		public static Int128 Min(Int128 a, Int128 b)
		{
			if (Int128.LessThan(ref a.v, ref b.v))
			{
				return a;
			}
			return b;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000299F File Offset: 0x00000B9F
		public static Int128 Max(Int128 a, Int128 b)
		{
			if (Int128.LessThan(ref b.v, ref a.v))
			{
				return a;
			}
			return b;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000029B9 File Offset: 0x00000BB9
		public static double Log(Int128 a)
		{
			return Int128.Log(a, 2.7182818284590451);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000029CA File Offset: 0x00000BCA
		public static double Log10(Int128 a)
		{
			return Int128.Log(a, 10.0);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000029DB File Offset: 0x00000BDB
		public static double Log(Int128 a, double b)
		{
			if (a.IsNegative || a.IsZero)
			{
				throw new ArgumentException("argument must be positive");
			}
			return Math.Log(UInt128.ConvertToDouble(ref a.v), b);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000042A4 File Offset: 0x000024A4
		public static Int128 Add(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.Add(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004340 File Offset: 0x00002540
		public static Int128 Subtract(Int128 a, Int128 b)
		{
			Int128 result;
			UInt128.Subtract(out result.v, ref a.v, ref b.v);
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004478 File Offset: 0x00002678
		public static Int128 Multiply(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Multiply(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000044F4 File Offset: 0x000026F4
		public static Int128 Divide(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Divide(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004510 File Offset: 0x00002710
		public static Int128 Remainder(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.Remainder(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005094 File Offset: 0x00003294
		public static Int128 DivRem(Int128 a, Int128 b, out Int128 remainder)
		{
			Int128 result;
			Int128.Divide(out result, ref a, ref b);
			Int128.Remainder(out remainder, ref a, ref b);
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004368 File Offset: 0x00002568
		public static Int128 Negate(Int128 a)
		{
			Int128 result;
			UInt128.Negate(out result.v, ref a.v);
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000050B8 File Offset: 0x000032B8
		public static Int128 GreatestCommonDivisor(Int128 a, Int128 b)
		{
			Int128 result;
			Int128.GreatestCommonDivisor(out result, ref a, ref b);
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000050D4 File Offset: 0x000032D4
		public static void GreatestCommonDivisor(out Int128 c, ref Int128 a, ref Int128 b)
		{
			if (a.IsNegative)
			{
				UInt128 @uint;
				UInt128.Negate(out @uint, ref a.v);
				if (b.IsNegative)
				{
					UInt128 uint2;
					UInt128.Negate(out uint2, ref b.v);
					UInt128.GreatestCommonDivisor(out c.v, ref @uint, ref uint2);
					return;
				}
				UInt128.GreatestCommonDivisor(out c.v, ref @uint, ref b.v);
				return;
			}
			else
			{
				if (b.IsNegative)
				{
					UInt128 uint3;
					UInt128.Negate(out uint3, ref b.v);
					UInt128.GreatestCommonDivisor(out c.v, ref a.v, ref uint3);
					return;
				}
				UInt128.GreatestCommonDivisor(out c.v, ref a.v, ref b.v);
				return;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002A0C File Offset: 0x00000C0C
		public static void LeftShift(ref Int128 c, int d)
		{
			UInt128.LeftShift(ref c.v, d);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002A1A File Offset: 0x00000C1A
		public static void LeftShift(ref Int128 c)
		{
			UInt128.LeftShift(ref c.v);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002A27 File Offset: 0x00000C27
		public static void RightShift(ref Int128 c, int d)
		{
			UInt128.ArithmeticRightShift(ref c.v, d);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002A35 File Offset: 0x00000C35
		public static void RightShift(ref Int128 c)
		{
			UInt128.ArithmeticRightShift(ref c.v);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002A42 File Offset: 0x00000C42
		public static void Swap(ref Int128 a, ref Int128 b)
		{
			UInt128.Swap(ref a.v, ref b.v);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002A55 File Offset: 0x00000C55
		public static int Compare(Int128 a, Int128 b)
		{
			return a.CompareTo(b);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002A5F File Offset: 0x00000C5F
		public static void Shift(out Int128 c, ref Int128 a, int d)
		{
			UInt128.ArithmeticShift(out c.v, ref a.v, d);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002A73 File Offset: 0x00000C73
		public static void Shift(ref Int128 c, int d)
		{
			UInt128.ArithmeticShift(ref c.v, d);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005170 File Offset: 0x00003370
		public static Int128 ModAdd(Int128 a, Int128 b, Int128 modulus)
		{
			Int128 result;
			UInt128.ModAdd(out result.v, ref a.v, ref b.v, ref modulus.v);
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000051A0 File Offset: 0x000033A0
		public static Int128 ModSub(Int128 a, Int128 b, Int128 modulus)
		{
			Int128 result;
			UInt128.ModSub(out result.v, ref a.v, ref b.v, ref modulus.v);
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000051D0 File Offset: 0x000033D0
		public static Int128 ModMul(Int128 a, Int128 b, Int128 modulus)
		{
			Int128 result;
			UInt128.ModMul(out result.v, ref a.v, ref b.v, ref modulus.v);
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005200 File Offset: 0x00003400
		public static Int128 ModPow(Int128 value, Int128 exponent, Int128 modulus)
		{
			Int128 result;
			UInt128.ModPow(out result.v, ref value.v, ref exponent.v, ref modulus.v);
			return result;
		}

		// Token: 0x04000001 RID: 1
		private UInt128 v;

		// Token: 0x04000002 RID: 2
		private static readonly Int128 minValue = (Int128)((UInt128)1 << 127);

		// Token: 0x04000003 RID: 3
		private static readonly Int128 maxValue = (Int128)(((UInt128)1 << 127) - 1UL);

		// Token: 0x04000004 RID: 4
		private static readonly Int128 zero = 0;

		// Token: 0x04000005 RID: 5
		private static readonly Int128 one = 1;

		// Token: 0x04000006 RID: 6
		private static readonly Int128 minusOne = -1;
	}
}
