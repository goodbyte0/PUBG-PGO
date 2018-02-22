using System;

namespace PGVisual.Overlay
{
	// Token: 0x02000007 RID: 7
	internal class LocalActor : BaseActor
	{
		// Token: 0x06000224 RID: 548 RVA: 0x000037E1 File Offset: 0x000019E1
		public LocalActor(ulong actorPtr, ulong localPlayer)
		{
			this.ptr = actorPtr;
			this.localPlayer = localPlayer;
			if (this.localPlayer != 0UL)
			{
				this.position3D = Program.Client.ReadMemory<Vector3>(this.localPlayer + 112UL);
			}
		}

		// Token: 0x04000016 RID: 22
		private ulong localPlayer;

		// Token: 0x04000017 RID: 23
		public Vector3 position3D;
	}
}
