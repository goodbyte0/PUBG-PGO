using System;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x02000015 RID: 21
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct FQuat
	{
		// Token: 0x04000049 RID: 73
		public float x;

		// Token: 0x0400004A RID: 74
		public float y;

		// Token: 0x0400004B RID: 75
		public float z;

		// Token: 0x0400004C RID: 76
		public float w;
	}
}
