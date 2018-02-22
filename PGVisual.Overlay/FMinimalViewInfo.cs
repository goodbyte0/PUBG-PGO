using System;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x02000012 RID: 18
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct FMinimalViewInfo
	{
		// Token: 0x0400003E RID: 62
		public Vector3 Location;

		// Token: 0x0400003F RID: 63
		public Vector3 Rotation;

		// Token: 0x04000040 RID: 64
		public float FOV;

		// Token: 0x04000041 RID: 65
		public float OrthoWidth;

		// Token: 0x04000042 RID: 66
		public float OrthoNearClipPlane;

		// Token: 0x04000043 RID: 67
		public float OrthoFarClipPlane;

		// Token: 0x04000044 RID: 68
		public float AspectRatio;
	}
}
