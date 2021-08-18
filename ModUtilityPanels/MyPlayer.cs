using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Services.UI.ControlPanel;
using ModUtilityPanels.Internals.ControlPanel;


namespace ModUtilityPanels {
	/// @private
	partial class ModControlPanelPlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			ModContent.GetInstance<UIControlPanel>().UpdateGlobal();
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = ModUtilityPanelsMod.Instance;
			var cp = ModContent.GetInstance<UIControlPanel>();

			try {
				if( mymod.UtilityPanelsHotkey != null && mymod.UtilityPanelsHotkey.JustPressed ) {
					if( cp != null ) {
						if( cp.IsOpen ) {
							ControlPanelTabs.CloseDialog();
						} else {
							ControlPanelTabs.OpenTab( UIControlPanel.DefaultTabName );
						}
					}
				}
			} catch( Exception e ) {
				LogLibraries.Warn( "(1) - " + e.ToString() );
				return;
			}
		}
	}
}
