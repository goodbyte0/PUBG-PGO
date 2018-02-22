using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x0200000E RID: 14
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct EncryptedActor
	{
		// Token: 0x04000036 RID: 54
		[FixedBuffer(typeof(ulong), 43)]
		public EncryptedActor.<ptr_table>e__FixedBuffer ptr_table;

		// Token: 0x04000037 RID: 55
		public ushort index;

		// Token: 0x04000038 RID: 56
		[FixedBuffer(typeof(byte), 6)]
		private EncryptedActor.<unk2>e__FixedBuffer unk2;

		// Token: 0x04000039 RID: 57
		public ushort xor;

		// Token: 0x0400003A RID: 58
		[FixedBuffer(typeof(byte), 6)]
		private EncryptedActor.<unk3>e__FixedBuffer unk3;

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 344)]
		public struct <ptr_table>e__FixedBuffer
		{
			// Token: 0x0400003B RID: 59
			public ulong FixedElementField;
		}

		// Token: 0x02000010 RID: 16
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 6)]
		public struct <unk2>e__FixedBuffer
		{
			// Token: 0x0400003C RID: 60
			public byte FixedElementField;
		}

		// Token: 0x02000011 RID: 17
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 6)]
		public struct <unk3>e__FixedBuffer
		{
			// Token: 0x0400003D RID: 61
			public byte FixedElementField;
		}
	}
}
