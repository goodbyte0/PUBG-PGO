using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace PGVisual.Overlay
{
	// Token: 0x0200001F RID: 31
	public partial class Overlay : Form
	{
		// Token: 0x06000255 RID: 597
		[DllImport("user32.dll")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		// Token: 0x06000256 RID: 598 RVA: 0x00003A83 File Offset: 0x00001C83
		public Overlay(IntPtr parentWindow)
		{
			this._parentWindow = parentWindow;
			this.InitializeComponent();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000088B0 File Offset: 0x00006AB0
		private void LoadOverlay(object sender, EventArgs e)
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this._factory = new Factory();
			this._fontFactory = new Factory();
			HwndRenderTargetProperties renderProperties = default(HwndRenderTargetProperties);
			renderProperties.Hwnd = base.Handle;
			renderProperties.PixelSize = new Size2(base.Size.Width, base.Size.Height);
			renderProperties.PresentOptions = 0;
			this._renderProperties = renderProperties;
			this._device = new WindowRenderTarget(this._factory, new RenderTargetProperties(new PixelFormat(87, 1)), this._renderProperties);
			this._brush = new SolidColorBrush(this._device, new RawColor4((float)Color.Red.R, (float)Color.Red.G, (float)Color.Red.B, (float)Color.Red.A));
			this._font = new TextFormat(this._fontFactory, "Verdana", 700, 0, 12f);
			this._fontSmall = new TextFormat(this._fontFactory, "Verdana", 700, 0, 10f);
			Console.WriteLine("Starting Render Thread");
			this._threadDx = new Thread(new ParameterizedThreadStart(this.DirectXThread))
			{
				Priority = ThreadPriority.Highest,
				IsBackground = true
			};
			this._running = true;
			this._threadDx.Start();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00003AA3 File Offset: 0x00001CA3
		private void ClosedOverlay(object sender, FormClosedEventArgs e)
		{
			this._running = false;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008A28 File Offset: 0x00006C28
		private void UpdateDimension()
		{
			this._parentRect = default(Overlay.RECT);
			Overlay.GetWindowRect(this._parentWindow, out this._parentRect);
			if (this._parentRect.Left != this._oldParentRect.Left || this._parentRect.Top != this._oldParentRect.Top || this._parentRect.Right != this._oldParentRect.Right || this._parentRect.Bottom != this._oldParentRect.Bottom)
			{
				base.Invoke(new Action(delegate
				{
					Overlay.SetWindowPos(base.Handle, 0, this._parentRect.Left, this._parentRect.Top, this._parentRect.Right - this._parentRect.Left, this._parentRect.Bottom - this._parentRect.Top, 0);
					Program.Pubg.SetDimension(this._parentRect.Right - this._parentRect.Left, this._parentRect.Bottom - this._parentRect.Top);
				}));
			}
			this._oldParentRect = this._parentRect;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public void DrawString(string text, float x, float y, Color color)
		{
			this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
			this._device.DrawText(text, this._fontSmall, new RawRectangleF(x, y, float.MaxValue, float.MaxValue), this._brush, 1, 0);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008B3C File Offset: 0x00006D3C
		public void FillRect(float x, float y, float w, float h, Color color)
		{
			this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
			this._device.FillRectangle(new RawRectangleF(x, y, x + w, y + h), this._brush);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008B98 File Offset: 0x00006D98
		public void DrawRect(float x, float y, float w, float h, Color color)
		{
			this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
			this._device.DrawRectangle(new RawRectangleF(x, y, x + w, y + h), this._brush);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00003AAC File Offset: 0x00001CAC
		public Color GetHPColor(float hp)
		{
			if (hp > 60f)
			{
				return Color.Lime;
			}
			if (hp > 40f)
			{
				return Color.Orange;
			}
			return Color.Red;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008BF4 File Offset: 0x00006DF4
		private void DirectXThread(object sender)
		{
			while (this._running)
			{
				this._device.BeginDraw();
				this._device.Clear(new RawColor4?(new RawColor4((float)Color.Transparent.R, (float)Color.Transparent.G, (float)Color.Transparent.B, (float)Color.Transparent.A)));
				if (Overlay.GetForegroundWindow() == this._parentWindow)
				{
					this.UpdateDimension();
					this._device.TextAntialiasMode = 3;
					this.DrawString("PrivateCheatz Overlay Loaded", 100f, 100f, Color.Red);
					if (Program.Pubg.LocalActor != null)
					{
						foreach (Actor actor in Program.Pubg.Players)
						{
							if (actor.ptr != Program.Pubg.LocalActor.ptr)
							{
								float num = Program.Pubg.LocalActor.position3D.Distance(actor.position3D) / 100f;
								float health = actor.health;
								if (num < 500f && health > 0f)
								{
									float num2 = actor.positionHead2D.Y - actor.position2D.Y;
									if (num2 < 0f)
									{
										num2 *= -1f;
									}
									float num3 = num2;
									string text = string.Format("{0}m ({1}%)", num.ToString("0.00"), health.ToString("0"));
									this.DrawString(text, actor.position2D.X - num3, actor.position2D.Y + num2 * 1.2f, Color.Red);
									this.DrawRect(actor.positionHead2D.X - num3 / 2f, actor.positionHead2D.Y - num2 * 2f, num3, num2 * 2f, Color.Red);
									this.DrawRect(actor.position2D.X - num3, actor.position2D.Y + num2 * 1.2f + 13f, 100f, 7f, Color.Black);
									this.FillRect(actor.position2D.X - num3, actor.position2D.Y + num2 * 1.2f + 13f, 100f, 7f, Color.DarkGray);
									this.FillRect(actor.position2D.X - num3, actor.position2D.Y + num2 * 1.2f + 13f, health, 7f, this.GetHPColor(health));
								}
							}
						}
					}
				}
				this._device.EndDraw();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00003ACF File Offset: 0x00001CCF
		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00003AD2 File Offset: 0x00001CD2
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 8;
				createParams.ExStyle |= 134217728;
				return createParams;
			}
		}

		// Token: 0x06000261 RID: 609
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(IntPtr hWnd, out Overlay.RECT lpRect);

		// Token: 0x06000262 RID: 610
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000263 RID: 611
		[DllImport("user32.dll")]
		private static extern IntPtr SetActiveWindow(IntPtr handle);

		// Token: 0x06000264 RID: 612
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x06000265 RID: 613 RVA: 0x00008EE8 File Offset: 0x000070E8
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 33)
			{
				m.Result = (IntPtr)4;
				return;
			}
			if (m.Msg == 6)
			{
				if (((int)m.WParam & 65535) != 0)
				{
					if (m.LParam != IntPtr.Zero)
					{
						Overlay.SetActiveWindow(m.LParam);
						return;
					}
					Overlay.SetActiveWindow(IntPtr.Zero);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x040001AF RID: 431
		private WindowRenderTarget _device;

		// Token: 0x040001B0 RID: 432
		private HwndRenderTargetProperties _renderProperties;

		// Token: 0x040001B1 RID: 433
		private SolidColorBrush _solidColorBrush;

		// Token: 0x040001B2 RID: 434
		private Factory _factory;

		// Token: 0x040001B3 RID: 435
		private SolidColorBrush _brush;

		// Token: 0x040001B4 RID: 436
		private TextFormat _font;

		// Token: 0x040001B5 RID: 437
		private TextFormat _fontSmall;

		// Token: 0x040001B6 RID: 438
		private Factory _fontFactory;

		// Token: 0x040001B7 RID: 439
		private const string _fontFamily = "Verdana";

		// Token: 0x040001B8 RID: 440
		private const float _fontSize = 12f;

		// Token: 0x040001B9 RID: 441
		private const float _fontSizeSmall = 10f;

		// Token: 0x040001BA RID: 442
		private IntPtr _parentWindow = IntPtr.Zero;

		// Token: 0x040001BB RID: 443
		private Thread _threadDx;

		// Token: 0x040001BC RID: 444
		private Overlay.RECT _oldParentRect;

		// Token: 0x040001BD RID: 445
		private Overlay.RECT _parentRect;

		// Token: 0x040001BE RID: 446
		private bool _running;

		// Token: 0x040001BF RID: 447
		private const int WS_EX_NOACTIVATE = 134217728;

		// Token: 0x040001C0 RID: 448
		private const int WS_EX_TOPMOST = 8;

		// Token: 0x040001C1 RID: 449
		private const short SWP_NOMOVE = 2;

		// Token: 0x040001C2 RID: 450
		private const short SWP_NOSIZE = 1;

		// Token: 0x040001C3 RID: 451
		private const short SWP_NOZORDER = 4;

		// Token: 0x040001C4 RID: 452
		private const int SWP_SHOWWINDOW = 64;

		// Token: 0x040001C5 RID: 453
		private const int WM_ACTIVATE = 6;

		// Token: 0x040001C6 RID: 454
		private const int WA_INACTIVE = 0;

		// Token: 0x040001C7 RID: 455
		private const int WM_MOUSEACTIVATE = 33;

		// Token: 0x040001C8 RID: 456
		private const int MA_NOACTIVATEANDEAT = 4;

		// Token: 0x02000020 RID: 32
		public struct RECT
		{
			// Token: 0x040001CA RID: 458
			public int Left;

			// Token: 0x040001CB RID: 459
			public int Top;

			// Token: 0x040001CC RID: 460
			public int Right;

			// Token: 0x040001CD RID: 461
			public int Bottom;
		}
	}
}
