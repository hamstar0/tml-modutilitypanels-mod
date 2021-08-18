using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Services.UI.UtilityPanels;
using ModUtilityPanels.Internals.UtilityPanels;


namespace ModUtilityPanels {
	/// @private
	partial class ModUtilityPanelsPlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			ModContent.GetInstance<UIUtilityPanels>().UpdateGlobal();
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = ModUtilityPanelsMod.Instance;
			var cp = ModContent.GetInstance<UIUtilityPanels>();

			try {
				if( mymod.UtilityPanelsHotkey != null && mymod.UtilityPanelsHotkey.JustPressed ) {
					if( cp != null ) {
						if( cp.IsOpen ) {
							UtilityPanelsTabs.CloseDialog();
						} else {
							UtilityPanelsTabs.OpenTab( UIUtilityPanels.DefaultTabName );
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
