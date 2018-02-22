namespace PGVisual.Overlay
{
	// Token: 0x0200001F RID: 31
	public partial class Overlay : global::System.Windows.Forms.Form
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00003AFA File Offset: 0x00001CFA
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008F5C File Offset: 0x0000715C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = new global::System.Drawing.Size(284, 261);
			base.ControlBox = false;
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Overlay";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Overlay";
			base.TopMost = true;
			base.TransparencyKey = global::System.Drawing.Color.Black;
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.ClosedOverlay);
			base.Load += new global::System.EventHandler(this.LoadOverlay);
			base.ResumeLayout(false);
		}

		// Token: 0x040001C9 RID: 457
		private global::System.ComponentModel.IContainer components;
	}
}
