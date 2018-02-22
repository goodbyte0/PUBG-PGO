using System;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
	// Token: 0x0200000D RID: 13
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal struct D3DMatrix
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00007FA4 File Offset: 0x000061A4
		public static D3DMatrix Multiplicate(D3DMatrix pM1, D3DMatrix pM2)
		{
			return new D3DMatrix
			{
				_11 = pM1._11 * pM2._11 + pM1._12 * pM2._21 + pM1._13 * pM2._31 + pM1._14 * pM2._41,
				_12 = pM1._11 * pM2._12 + pM1._12 * pM2._22 + pM1._13 * pM2._32 + pM1._14 * pM2._42,
				_13 = pM1._11 * pM2._13 + pM1._12 * pM2._23 + pM1._13 * pM2._33 + pM1._14 * pM2._43,
				_14 = pM1._11 * pM2._14 + pM1._12 * pM2._24 + pM1._13 * pM2._34 + pM1._14 * pM2._44,
				_21 = pM1._21 * pM2._11 + pM1._22 * pM2._21 + pM1._23 * pM2._31 + pM1._24 * pM2._41,
				_22 = pM1._21 * pM2._12 + pM1._22 * pM2._22 + pM1._23 * pM2._32 + pM1._24 * pM2._42,
				_23 = pM1._21 * pM2._13 + pM1._22 * pM2._23 + pM1._23 * pM2._33 + pM1._24 * pM2._43,
				_24 = pM1._21 * pM2._14 + pM1._22 * pM2._24 + pM1._23 * pM2._34 + pM1._24 * pM2._44,
				_31 = pM1._31 * pM2._11 + pM1._32 * pM2._21 + pM1._33 * pM2._31 + pM1._34 * pM2._41,
				_32 = pM1._31 * pM2._12 + pM1._32 * pM2._22 + pM1._33 * pM2._32 + pM1._34 * pM2._42,
				_33 = pM1._31 * pM2._13 + pM1._32 * pM2._23 + pM1._33 * pM2._33 + pM1._34 * pM2._43,
				_34 = pM1._31 * pM2._14 + pM1._32 * pM2._24 + pM1._33 * pM2._34 + pM1._34 * pM2._44,
				_41 = pM1._41 * pM2._11 + pM1._42 * pM2._21 + pM1._43 * pM2._31 + pM1._44 * pM2._41,
				_42 = pM1._41 * pM2._12 + pM1._42 * pM2._22 + pM1._43 * pM2._32 + pM1._44 * pM2._42,
				_43 = pM1._41 * pM2._13 + pM1._42 * pM2._23 + pM1._43 * pM2._33 + pM1._44 * pM2._43,
				_44 = pM1._41 * pM2._14 + pM1._42 * pM2._24 + pM1._43 * pM2._34 + pM1._44 * pM2._44
			};
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000839C File Offset: 0x0000659C
		public D3DMatrix(FTransform transform)
		{
			this._41 = transform.translation.X;
			this._42 = transform.translation.Y;
			this._43 = transform.translation.Z;
			float num = transform.rot.x + transform.rot.x;
			float num2 = transform.rot.y + transform.rot.y;
			float num3 = transform.rot.z + transform.rot.z;
			float num4 = transform.rot.x * num;
			float num5 = transform.rot.y * num2;
			float num6 = transform.rot.z * num3;
			this._11 = (1f - (num5 + num6)) * transform.scale.X;
			this._22 = (1f - (num4 + num6)) * transform.scale.Y;
			this._33 = (1f - (num4 + num5)) * transform.scale.Z;
			float num7 = transform.rot.y * num3;
			float num8 = transform.rot.w * num;
			this._32 = (num7 - num8) * transform.scale.Z;
			this._23 = (num7 + num8) * transform.scale.Y;
			float num9 = transform.rot.x * num2;
			float num10 = transform.rot.w * num3;
			this._21 = (num9 - num10) * transform.scale.Y;
			this._12 = (num9 + num10) * transform.scale.X;
			float num11 = transform.rot.x * num3;
			float num12 = transform.rot.w * num2;
			this._31 = (num11 + num12) * transform.scale.Z;
			this._13 = (num11 - num12) * transform.scale.X;
			this._14 = 0f;
			this._24 = 0f;
			this._34 = 0f;
			this._44 = 1f;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000085B8 File Offset: 0x000067B8
		public D3DMatrix(Vector3 rot, Vector3 origin)
		{
			float num = rot.X * 3.14159274f / 180f;
			float num2 = rot.Y * 3.14159274f / 180f;
			float num3 = rot.Z * 3.14159274f / 180f;
			float num4 = (float)Math.Sin((double)num);
			float num5 = (float)Math.Cos((double)num);
			float num6 = (float)Math.Sin((double)num2);
			float num7 = (float)Math.Cos((double)num2);
			float num8 = (float)Math.Sin((double)num3);
			float num9 = (float)Math.Cos((double)num3);
			this._11 = num5 * num7;
			this._12 = num5 * num6;
			this._13 = num4;
			this._14 = 0f;
			this._21 = num8 * num4 * num7 - num9 * num6;
			this._22 = num8 * num4 * num6 + num9 * num7;
			this._23 = -num8 * num5;
			this._24 = 0f;
			this._31 = -(num9 * num4 * num7 + num8 * num6);
			this._32 = num7 * num8 - num9 * num4 * num6;
			this._33 = num9 * num5;
			this._34 = 0f;
			this._41 = origin.X;
			this._42 = origin.Y;
			this._43 = origin.Z;
			this._44 = 1f;
		}

		// Token: 0x04000026 RID: 38
		public float _11;

		// Token: 0x04000027 RID: 39
		public float _12;

		// Token: 0x04000028 RID: 40
		public float _13;

		// Token: 0x04000029 RID: 41
		public float _14;

		// Token: 0x0400002A RID: 42
		public float _21;

		// Token: 0x0400002B RID: 43
		public float _22;

		// Token: 0x0400002C RID: 44
		public float _23;

		// Token: 0x0400002D RID: 45
		public float _24;

		// Token: 0x0400002E RID: 46
		public float _31;

		// Token: 0x0400002F RID: 47
		public float _32;

		// Token: 0x04000030 RID: 48
		public float _33;

		// Token: 0x04000031 RID: 49
		public float _34;

		// Token: 0x04000032 RID: 50
		public float _41;

		// Token: 0x04000033 RID: 51
		public float _42;

		// Token: 0x04000034 RID: 52
		public float _43;

		// Token: 0x04000035 RID: 53
		public float _44;
	}
}
