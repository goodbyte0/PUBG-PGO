using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x02000009 RID: 9
	internal struct EncryptedBlock
	{
		// Token: 0x0400001F RID: 31
		[FixedBuffer(typeof(long), 48)]
		public EncryptedBlock.<data>e__FixedBuffer data;

		// Token: 0x0200000A RID: 10
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		public struct <data>e__FixedBuffer
		{
			// Token: 0x04000020 RID: 32
			public long FixedElementField;
		}
	}
}
