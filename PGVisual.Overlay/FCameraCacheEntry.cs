using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x02000013 RID: 19
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct FCameraCacheEntry
	{
		// Token: 0x04000045 RID: 69
		public float TimeStamp;

		// Token: 0x04000046 RID: 70
		[FixedBuffer(typeof(byte), 12)]
		private FCameraCacheEntry.<UnknownData00>e__FixedBuffer UnknownData00;

		// Token: 0x04000047 RID: 71
		public FMinimalViewInfo POV;

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = 12)]
		public struct <UnknownData00>e__FixedBuffer
		{
			// Token: 0x04000048 RID: 72
			public byte FixedElementField;
		}
	}
}
