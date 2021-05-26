using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModControlPanel {
	/// <summary>
	/// Defines Mod Control Panel config settings.
	/// </summary>
	[Label( "Mod Control Panel Settings" )]
	public class ModControlPanelConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModControlPanelConfig Instance => ModContent.GetInstance<ModControlPanelConfig>();



		////////////////

		/// @private
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		/// <summary>
		/// Disables control panel outright.
		/// </summary>
		[Header( "Control panel settings" )]
		[Label( "Disable Control Panel" )]
		[Tooltip( "Disables control panel outright." )]
		public bool DisableControlPanel { get; set; } = false;

		/// <summary>
		/// Control panel icon's X coordinate on screen. Negative values align the button from the right edge.
		/// </summary>
		[Label( "Control Panel Icon X" )]
		[Tooltip( "Control panel icon's X coordinate on screen. Negative values align the button from the right edge." )]
		[Increment( 10 )]
		[Range( -4096, 4096 )]
		public int ControlPanelIconX { get; set; } = 0;

		/// <summary>
		/// Control panel icon's Y coordinate on screen. Negative values align the button from the bottom edge.
		/// </summary>
		[Label( "Control Panel Icon Y" )]
		[Tooltip( "Control panel icon's Y coordinate on screen. Negative values align the button from the bottom edge." )]
		[Increment( 10 )]
		[Range( -2160, 2160 )]
		public int ControlPanelIconY { get; set; } = 0;
	}
}
