using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PGVisual.Overlay
{
	// Token: 0x02000021 RID: 33
	internal static class Program
	{
		// Token: 0x06000269 RID: 617
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x0600026A RID: 618 RVA: 0x000090BC File Offset: 0x000072BC
		private static void CheckHWID()
		{
			int num = 0;
			for (;;)
			{
				if (new WebClient
				{
					Proxy = null
				}.DownloadString(string.Concat(new string[]
				{
					"http://5.196.102.147/xpg/xpg.php?username=",
					Program.Username,
					"&password=",
					Program.Password,
					"&hwid=",
					Program.HWID,
					"&session=1336"
				})).Length != 32)
				{
					num++;
				}
				if (num == 3)
				{
					Environment.Exit(0);
				}
				Thread.Sleep(60000);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009148 File Offset: 0x00007348
		[STAThread]
		private static void Main(string[] args)
		{
			Console.Title = "PGO";
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Console.WriteLine("Looking for PUBG Process...");
			do
			{
				Program.PubgProcess = Process.GetProcessesByName("TslGame").FirstOrDefault<Process>();
				Thread.Sleep(100);
			}
			while (Program.PubgProcess == null);
			Console.WriteLine("PUBG Process Found {0}", Program.PubgProcess.Id);
			Console.WriteLine("Initializing Bypass");
			Program.Client = new HSClient();
			if (!Program.Client.connect())
			{
				Console.WriteLine("Bypass step 1 failed");
				return;
			}
			Console.WriteLine("Bypass step 1/2 OK");
			Console.WriteLine("Loading Bypass, this might take a few seconds...");
			Thread.Sleep(2000);
			if (Program.Client.Handshake(Program.PubgProcess.Id, 10))
			{
				Console.WriteLine("Bypass step 2/2 OK");
				Console.WriteLine("Bypass Completed");
				IntPtr intPtr = IntPtr.Zero;
				Console.WriteLine("Looking for PUBG Window...");
				do
				{
					intPtr = Program.FindWindow("UnrealWindow", null);
					Thread.Sleep(100);
				}
				while (intPtr == IntPtr.Zero);
				Console.WriteLine("PUBG Window Found {0}", intPtr.ToInt32());
				Thread.Sleep(1000);
				Program.Pubg = new PUBG((ulong)((long)Program.PubgProcess.Id), Program.Client);
				if (!Program.Pubg.Start())
				{
					Console.WriteLine("Failed to load CSPUBGEngine");
					Environment.Exit(0);
				}
				Application.Run(new Overlay(intPtr));
				return;
			}
			Console.WriteLine("Bypass step 2 failed");
		}

		// Token: 0x040001CE RID: 462
		public static HSClient Client;

		// Token: 0x040001CF RID: 463
		public static PUBG Pubg = null;

		// Token: 0x040001D0 RID: 464
		public static Process PubgProcess = null;

		// Token: 0x040001D1 RID: 465
		public static string HWID = "";

		// Token: 0x040001D2 RID: 466
		public static string Username = "";

		// Token: 0x040001D3 RID: 467
		public static string Password = "";
	}
}
