using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace PGVisual.Overlay.Properties
{
	// Token: 0x02000025 RID: 37
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00003E54 File Offset: 0x00002054
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x040001E9 RID: 489
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
