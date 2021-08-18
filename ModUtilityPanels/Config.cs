using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModUtilityPanels {
	/// <summary>
	/// Defines Mod Utility Panels config settings.
	/// </summary>
	[Label( "Mod Utility Panels Settings" )]
	public class ModUtilityPanelsConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModUtilityPanelsConfig Instance => ModContent.GetInstance<ModUtilityPanelsConfig>();



		////////////////

		/// @private
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		/// <summary>
		/// Disables Utility Panels outright.
		/// </summary>
		[Header( "Utility panels settings" )]
		[Label( "Disable Utility Panels" )]
		[Tooltip( "Disables utility panels outright." )]
		public bool DisableUtilityPanels { get; set; } = false;

		/// <summary>
		/// Utility panel icon's X coordinate on screen. Negative values align the button from the right edge.
		/// </summary>
		[Label( "Utility Panels Icon X" )]
		[Tooltip( "Utility panels icon's X coordinate on screen. Negative values align the button from the right edge." )]
		[Increment( 10 )]
		[Range( -4096, 4096 )]
		[DefaultValue( 2 )]
		public int UtilityPanelsIconX { get; set; } = 2;

		/// <summary>
		/// Utility panel icon's Y coordinate on screen. Negative values align the button from the bottom edge.
		/// </summary>
		[Label( "Utility Panels Icon Y" )]
		[Tooltip( "Utility panel icon's Y coordinate on screen. Negative values align the button from the bottom edge." )]
		[Increment( 10 )]
		[Range( -2160, 2160 )]
		[DefaultValue( 2 )]
		public int UtilityPanelsIconY { get; set; } = 2;
	}
}
