using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace ModUtilityPanels.Commands {
	/// @private
	public class UtilityPanelsOpenCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "ml-up-open";
		/// @private
		public override string Usage => "/" +this.Command;
		/// @private
		public override string Description => "Opens the mod Utility Panels.";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( Main.netMode == NetmodeID.Server ) {
				caller.Reply( "Command not available for server.", Color.Red );
				return;
			}

			if( ModUtilityPanelsConfig.Instance.DisableUtilityPanels ) {
				caller.Reply( "Utility panels disabled.", Color.Red );
			} else {
				UtilityPanelsTabs.OpenTab( UtilityPanelsTabs.GetCurrentTab() );
			}
		}
	}
}
