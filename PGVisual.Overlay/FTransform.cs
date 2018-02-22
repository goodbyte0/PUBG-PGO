using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x02000016 RID: 22
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct FTransform
	{
		// Token: 0x0400004D RID: 77
		public FQuat rot;

		// Token: 0x0400004E RID: 78
		public Vector3 translation;

		// Token: 0x0400004F RID: 79
		[FixedBuffer(typeof(char), 4)]
		private FTransform.<pad>e__FixedBuffer pad;

		// Token: 0x04000050 RID: 80
		public Vector3 scale;

		// Token: 0x04000051 RID: 81
		[FixedBuffer(typeof(char), 4)]
		private FTransform.<pad1>e__FixedBuffer pad1;

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 8)]
		public struct <pad>e__FixedBuffer
		{
			// Token: 0x04000052 RID: 82
			public char FixedElementField;
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 8)]
		public struct <pad1>e__FixedBuffer
		{
			// Token: 0x04000053 RID: 83
			public char FixedElementField;
		}
	}
}
