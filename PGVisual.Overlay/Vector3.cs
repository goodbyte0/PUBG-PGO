using System;

namespace PGVisual.Overlay
{
	// Token: 0x02000023 RID: 35
	internal struct Vector3
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00003B91 File Offset: 0x00001D91
		public Vector3(float xVal, float yVal, float zVal)
		{
			this.X = xVal;
			this.Y = yVal;
			this.Z = zVal;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static Vector3 operator +(Vector3 mv1, Vector3 mv2)
		{
			return new Vector3(mv1.X + mv2.X, mv1.Y + mv2.Y, mv1.Z + mv2.Z);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003BD6 File Offset: 0x00001DD6
		public static Vector3 operator -(Vector3 mv1, Vector3 mv2)
		{
			return new Vector3(mv1.X - mv2.X, mv1.Y - mv2.Y, mv1.Z - mv2.Z);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003C04 File Offset: 0x00001E04
		public static Vector3 operator -(Vector3 mv1, float var)
		{
			return new Vector3(mv1.X - var, mv1.Y - var, mv1.Z - var);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00003C23 File Offset: 0x00001E23
		public static Vector3 operator *(Vector3 mv1, Vector3 mv2)
		{
			return new Vector3(mv1.X * mv2.X, mv1.Y * mv2.Y, mv1.Z * mv2.Z);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00003C51 File Offset: 0x00001E51
		public static Vector3 operator *(Vector3 mv, float var)
		{
			return new Vector3(mv.X * var, mv.Y * var, mv.Z * var);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009900 File Offset: 0x00007B00
		public static Vector3 operator %(Vector3 mv1, Vector3 mv2)
		{
			return new Vector3(mv1.Y * mv2.Z - mv1.Z * mv2.Y, mv1.Z * mv2.X - mv1.X * mv2.Z, mv1.X * mv2.Y - mv1.Y * mv2.X);
		}

		// Token: 0x1700002C RID: 44
		public float this[int key]
		{
			get
			{
				return this.GetValueByIndex(key);
			}
			set
			{
				this.SetValueByIndex(key, value);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00003C83 File Offset: 0x00001E83
		private void SetValueByIndex(int key, float value)
		{
			if (key == 0)
			{
				this.X = value;
				return;
			}
			if (key == 1)
			{
				this.Y = value;
				return;
			}
			if (key == 2)
			{
				this.Z = value;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00003CA7 File Offset: 0x00001EA7
		private float GetValueByIndex(int key)
		{
			if (key == 0)
			{
				return this.X;
			}
			if (key == 1)
			{
				return this.Y;
			}
			return this.Z;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public float DotProduct(Vector3 mv)
		{
			return this.X * mv.X + this.Y * mv.Y + this.Z * mv.Z;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003C51 File Offset: 0x00001E51
		public Vector3 ScaleBy(float value)
		{
			return new Vector3(this.X * value, this.Y * value, this.Z * value);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003C23 File Offset: 0x00001E23
		public Vector3 ComponentProduct(Vector3 mv)
		{
			return new Vector3(this.X * mv.X, this.Y * mv.Y, this.Z * mv.Z);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00003CEF File Offset: 0x00001EEF
		public void ComponentProductUpdate(Vector3 mv)
		{
			this.X *= mv.X;
			this.Y *= mv.Y;
			this.Z *= mv.Z;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009900 File Offset: 0x00007B00
		public Vector3 VectorProduct(Vector3 mv)
		{
			return new Vector3(this.Y * mv.Z - this.Z * mv.Y, this.Z * mv.X - this.X * mv.Z, this.X * mv.Y - this.Y * mv.X);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public float ScalarProduct(Vector3 mv)
		{
			return this.X * mv.X + this.Y * mv.Y + this.Z * mv.Z;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009964 File Offset: 0x00007B64
		public float Distance(Vector3 v)
		{
			return (float)Math.Sqrt(Math.Pow((double)(v.X - this.X), 2.0) + Math.Pow((double)(v.Y - this.Y), 2.0) + Math.Pow((double)(v.Z - this.Z), 2.0));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000099D0 File Offset: 0x00007BD0
		public void AddScaledVector(Vector3 mv, float scale)
		{
			this.X += mv.X * scale;
			this.Y += mv.Y * scale;
			this.Z += mv.Z * scale;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00003D2A File Offset: 0x00001F2A
		public float Magnitude()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00003D5C File Offset: 0x00001F5C
		public float SquareMagnitude()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00003D87 File Offset: 0x00001F87
		public void Trim(float size)
		{
			if (this.SquareMagnitude() > size * size)
			{
				this.Normalize();
				this.X *= size;
				this.Y *= size;
				this.Z *= size;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009A1C File Offset: 0x00007C1C
		public void Normalize()
		{
			float num = this.Magnitude();
			if (num > 0f)
			{
				this.X /= num;
				this.Y /= num;
				this.Z /= num;
				return;
			}
			this.X = 0f;
			this.Y = 0f;
			this.Z = 0f;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public Vector3 Inverted()
		{
			return new Vector3(-this.X, -this.Y, -this.Z);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009A84 File Offset: 0x00007C84
		public Vector3 Unit()
		{
			Vector3 result = this;
			result.Normalize();
			return result;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public void Clear()
		{
			this.X = 0f;
			this.Y = 0f;
			this.Z = 0f;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public static float Distance(Vector3 mv1, Vector3 mv2)
		{
			return (mv1 - mv2).Magnitude();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00003E03 File Offset: 0x00002003
		public static Vector3 Zero()
		{
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x040001E4 RID: 484
		public float X;

		// Token: 0x040001E5 RID: 485
		public float Y;

		// Token: 0x040001E6 RID: 486
		public float Z;
	}
}
