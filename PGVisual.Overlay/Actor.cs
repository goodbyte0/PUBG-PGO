using System;

namespace PGVisual.Overlay
{
	// Token: 0x02000008 RID: 8
	internal class Actor : BaseActor
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00007A6C File Offset: 0x00005C6C
		public Actor(ulong actorPtr)
		{
			this.ptr = actorPtr;
			this.rootComp = Program.Client.ReadPtr(actorPtr + 392UL);
			if (this.rootComp != 0UL)
			{
				this.position3D = Program.Client.ReadMemory<Vector3>(this.rootComp + 596UL);
				this.health = Program.Client.ReadMemory<float>(actorPtr + 4444UL);
				this.mesh = Program.Client.ReadPtr(actorPtr + 1040UL);
				if (this.mesh != 0UL)
				{
					this.boneArray = Program.Client.ReadPtr(this.mesh + 2400UL);
					Vector3 bone = this.GetBone(6);
					Program.Pubg.worldToScreen(bone, out this.positionHead2D);
				}
				Program.Pubg.worldToScreen(this.position3D, out this.position2D);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007B5C File Offset: 0x00005D5C
		public Vector3 GetBone(int boneIndex)
		{
			if (this.boneArray != 0UL)
			{
				FTransform transform = Program.Client.ReadMemory<FTransform>(this.boneArray + (ulong)((long)(boneIndex * 48)));
				FTransform transform2 = Program.Client.ReadMemory<FTransform>(this.mesh + 640UL);
				D3DMatrix d3DMatrix = D3DMatrix.Multiplicate(new D3DMatrix(transform), new D3DMatrix(transform2));
				return new Vector3(d3DMatrix._41, d3DMatrix._42, d3DMatrix._43);
			}
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000381F File Offset: 0x00001A1F
		public bool IsValid()
		{
			return this.rootComp != 0UL && this.mesh > 0UL;
		}

		// Token: 0x04000018 RID: 24
		public ulong mesh;

		// Token: 0x04000019 RID: 25
		public ulong boneArray;

		// Token: 0x0400001A RID: 26
		public Vector3 position3D;

		// Token: 0x0400001B RID: 27
		public Vector3 position2D;

		// Token: 0x0400001C RID: 28
		public Vector3 positionHead2D;

		// Token: 0x0400001D RID: 29
		public ulong rootComp;

		// Token: 0x0400001E RID: 30
		public float health;
	}
}
