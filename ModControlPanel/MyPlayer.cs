using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Services.UI.ControlPanel;
using ModControlPanel.Internals.ControlPanel;


namespace ModControlPanel {
	/// @private
	partial class ModControlPanelPlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			ModContent.GetInstance<UIControlPanel>().UpdateGlobal();
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = ModControlPanelMod.Instance;
			var cp = ModContent.GetInstance<UIControlPanel>();

			try {
				if( mymod.ControlPanelHotkey != null && mymod.ControlPanelHotkey.JustPressed ) {
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
