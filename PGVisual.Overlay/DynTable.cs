using System;

namespace PGVisual.Overlay
{
	// Token: 0x0200000B RID: 11
	internal class DynTable
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public void generate(int rotator, int[] decrypt_offsets)
		{
			for (uint num = 0u; num < 256u; num += 1u)
			{
				uint num2 = num;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				num2 >>= 1;
				if ((num2 & 1u) != 0u)
				{
					num2 ^= (uint)rotator;
				}
				this.table[(int)num] = num2 >> 1;
			}
			uint num3 = 512u;
			for (int i = 0; i < 256; i++)
			{
				uint num4 = this.table[(int)(num3 - 512u)];
				num3 += 1u;
				num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
				checked
				{
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[0]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[1]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[2]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[3]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[4]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[5]))))] = num4;
					num4 = (num4 >> 8 ^ this.table[(int)(num4 & 255u)]);
					this.table[(int)((IntPtr)(unchecked((ulong)num3 + (ulong)((long)decrypt_offsets[6]))))] = num4;
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000383C File Offset: 0x00001A3C
		public uint get(uint idx)
		{
			return this.table[(int)idx];
		}

		// Token: 0x04000021 RID: 33
		private uint[] table = new uint[2048];
	}
}
