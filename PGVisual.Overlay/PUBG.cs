using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace PGVisual.Overlay
{
	// Token: 0x02000022 RID: 34
	internal class PUBG
	{
		// Token: 0x0600026D RID: 621
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003B45 File Offset: 0x00001D45
		public ConcurrentBag<Actor> Players
		{
			get
			{
				return this._actors;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003B4D File Offset: 0x00001D4D
		public LocalActor LocalActor
		{
			get
			{
				return this._localActor;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00003B55 File Offset: 0x00001D55
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00003B5D File Offset: 0x00001D5D
		public ulong BaseAddress
		{
			get
			{
				return this._baseAddress;
			}
			set
			{
				this._baseAddress = value;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000092C8 File Offset: 0x000074C8
		public PUBG(ulong processId, HSClient client)
		{
			this._processId = processId;
			this._actors = new ConcurrentBag<Actor>();
			this._running = true;
			this._client = client;
			this._updateThread = new Thread(new ThreadStart(this.UpdateThread));
			this._playerClasses = new int[2];
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00003B66 File Offset: 0x00001D66
		public void SetDimension(int width, int heigth)
		{
			this._width = width;
			this._heigth = heigth;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000932C File Offset: 0x0000752C
		public bool worldToScreen(Vector3 world, out Vector3 screen)
		{
			screen.Z = 0f;
			FMinimalViewInfo pov = this._cameraCache.POV;
			Vector3 rotation = pov.Rotation;
			D3DMatrix d3DMatrix = new D3DMatrix(rotation, new Vector3(0f, 0f, 0f));
			Vector3 mv = new Vector3(d3DMatrix._11, d3DMatrix._12, d3DMatrix._13);
			Vector3 mv2 = new Vector3(d3DMatrix._21, d3DMatrix._22, d3DMatrix._23);
			Vector3 mv3 = new Vector3(d3DMatrix._31, d3DMatrix._32, d3DMatrix._33);
			Vector3 vector = world - pov.Location;
			Vector3 vector2 = new Vector3(vector.DotProduct(mv2), vector.DotProduct(mv3), vector.DotProduct(mv));
			if (vector2.Z < 1f)
			{
				vector2.Z = 1f;
			}
			float fov = pov.FOV;
			float num = (float)this._width / 2f;
			float num2 = (float)this._heigth / 2f;
			screen.X = num + vector2.X * (num / (float)Math.Tan((double)(fov * 3.14159274f / 360f))) / vector2.Z;
			screen.Y = num2 - vector2.Y * (num / (float)Math.Tan((double)(fov * 3.14159274f / 360f))) / vector2.Z;
			return screen.X >= 0f && screen.Y <= (float)this._heigth && screen.Y >= 0f && screen.Y <= (float)this._heigth;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000094D0 File Offset: 0x000076D0
		private bool InitClassIds()
		{
			for (int i = 0; i < 200000; i++)
			{
				string globalName = this.getGlobalName(i);
				if (globalName.GetHashCode() == -1818364965)
				{
					this._playerClasses[0] = i;
				}
				else if (globalName.GetHashCode() == 736643556)
				{
					this._playerClasses[1] = i;
				}
				if (this._playerClasses[0] != 0 && this._playerClasses[1] != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000953C File Offset: 0x0000773C
		private string getGlobalName(int Index)
		{
			if (Index > 3145728)
			{
				return "NULL";
			}
			int num = Index / 16384;
			int num2 = Index % 16384;
			if (this.GNamesPtr != 0UL)
			{
				ulong num3 = this._client.ReadPtr(this.GNamesPtr + (ulong)((long)(8 * num)));
				if (num3 != 0UL)
				{
					ulong num4 = this._client.ReadPtr(num3 + (ulong)((long)(8 * num2)));
					if (num4 != 0UL)
					{
						byte[] array = new byte[1000];
						this._client.ReadMemory(num4 + 16UL, array);
						int num5 = Array.IndexOf<byte>(array, 0);
						if (num5 < 1)
						{
							return "NULL";
						}
						Array.Resize<byte>(ref array, num5);
						return Encoding.ASCII.GetString(array);
					}
				}
			}
			return "NULL";
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000095FC File Offset: 0x000077FC
		public bool Start()
		{
			Console.WriteLine("Initializing Engine...");
			this.BaseAddress = this._client.GetModuleBase("TslGame.exe");
			if (this.BaseAddress <= 0UL)
			{
				return false;
			}
			this.GNamesPtr = this._client.ReadPtr(this.BaseAddress + 64958280UL);
			if (!this.InitClassIds())
			{
				return false;
			}
			this._updateThread.Start();
			return true;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00003B76 File Offset: 0x00001D76
		private ulong getActor(int index, ulong entityList)
		{
			return this._client.ReadPtr(entityList + (ulong)(8L * (long)index));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009674 File Offset: 0x00007874
		private void UpdateThread()
		{
			while (PUBG.FindWindow("UnrealWindow", null) != IntPtr.Zero)
			{
				try
				{
					ulong num;
					for (num = 0UL; num == 0UL; num = this._client.ReadPtr(this.BaseAddress + 64524048UL))
					{
					}
					num = this._client.ReadPtr(num);
					if (num != 0UL)
					{
						ulong num2 = this._client.ReadPtr(num + 320UL);
						if (num2 != 0UL)
						{
							ulong num3 = this._client.ReadPtr(num2 + 56UL);
							if (num3 != 0UL)
							{
								ulong num4 = this._client.ReadPtr(num3);
								if (num4 != 0UL)
								{
									ulong num5 = this._client.ReadPtr(num4 + 48UL);
									if (num5 != 0UL)
									{
										ulong num6 = this._client.ReadPtr(num5 + 1096UL);
										if (num6 != 0UL)
										{
											this._cameraCache = this._client.ReadMemory<FCameraCacheEntry>(num6 + 1056UL);
											ulong num7 = this._client.ReadPtr(num4 + 88UL);
											if (num7 != 0UL)
											{
												ulong num8 = this._client.ReadPtr(num7 + 128UL);
												if (num8 != 0UL)
												{
													ulong num9 = this._client.ReadPtr(num8 + 48UL);
													if (num9 != 0UL)
													{
														ulong num10 = this._client.ReadPtr(num5 + 1064UL);
														if (num10 != 0UL)
														{
															this._localActor = new LocalActor(num10, num4);
															ulong num11 = this._client.ReadPtr(num9 + 160UL);
															int num12 = this._client.ReadMemory<int>(num9 + 168UL);
															if (num11 != 0UL && num12 > 0)
															{
																ConcurrentBag<Actor> concurrentBag = new ConcurrentBag<Actor>();
																for (int i = 0; i < num12; i++)
																{
																	ulong actor = this.getActor(i, num11);
																	if (actor != 0UL)
																	{
																		int num13 = this._client.ReadMemory<int>(actor + 24UL);
																		if (num13 == this._playerClasses[0] || num13 == this._playerClasses[1])
																		{
																			Actor actor2 = new Actor(actor);
																			if (actor2 != null && actor2.IsValid())
																			{
																				concurrentBag.Add(actor2);
																			}
																		}
																	}
																}
																this._actors = concurrentBag;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception)
				{
				}
				Thread.Sleep(5);
			}
		}

		// Token: 0x040001D4 RID: 468
		private const ulong _ofsUWorld = 64524048UL;

		// Token: 0x040001D5 RID: 469
		private const ulong _ofsGNames = 64958280UL;

		// Token: 0x040001D6 RID: 470
		private ulong GNamesPtr;

		// Token: 0x040001D7 RID: 471
		private ulong UWorldPtr;

		// Token: 0x040001D8 RID: 472
		private ulong _processId;

		// Token: 0x040001D9 RID: 473
		private ConcurrentBag<Actor> _actors;

		// Token: 0x040001DA RID: 474
		private Thread _updateThread;

		// Token: 0x040001DB RID: 475
		private bool _running;

		// Token: 0x040001DC RID: 476
		private int _width;

		// Token: 0x040001DD RID: 477
		private int _heigth;

		// Token: 0x040001DE RID: 478
		private HSClient _client;

		// Token: 0x040001DF RID: 479
		private ulong _baseAddress;

		// Token: 0x040001E0 RID: 480
		private int[] _playerClasses;

		// Token: 0x040001E1 RID: 481
		private FCameraCacheEntry _cameraCache;

		// Token: 0x040001E2 RID: 482
		private LocalActor _localActor;

		// Token: 0x040001E3 RID: 483
		private ActorEncrypt encrypt = new ActorEncrypt();
	}
}
