using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModControlPanel.Services.UI.ControlPanel;


namespace ModControlPanel.Commands {
	/// @private
	public class ControlPanelOpenCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "ml-cp-open";
		/// @private
		public override string Usage => "/" +this.Command;
		/// @private
		public override string Description => "Opens the mod Control Panel.";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( Main.netMode == NetmodeID.Server ) {
				caller.Reply( "Command not available for server.", Color.Red );
				return;
			}

			if( ModControlPanelConfig.Instance.DisableControlPanel ) {
				caller.Reply( "Control panel disabled.", Color.Red );
			} else {
				ControlPanelTabs.OpenTab( ControlPanelTabs.GetCurrentTab() );
			}
		}
	}
}
