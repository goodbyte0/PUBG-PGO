using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PGVisual.Overlay.Properties
{
	// Token: 0x02000024 RID: 36
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		// Token: 0x06000296 RID: 662 RVA: 0x000037D9 File Offset: 0x000019D9
		internal Resources()
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00003E19 File Offset: 0x00002019
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("PGVisual.Overlay.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00003E45 File Offset: 0x00002045
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00003E4C File Offset: 0x0000204C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040001E7 RID: 487
		private static ResourceManager resourceMan;

		// Token: 0x040001E8 RID: 488
		private static CultureInfo resourceCulture;
	}
}
