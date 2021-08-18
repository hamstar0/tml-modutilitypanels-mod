using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModUtilityPanels.Internals.ControlPanel;


namespace ModUtilityPanels {
	/// @private
	public partial class ModUtilityPanelsMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( idx == -1 ) { return; }

			//

			GameInterfaceDrawMethod upDrawCallback = () => {
				this.DrawUP( Main.spriteBatch );
				return true;
			};

			//

			if( LoadLibraries.IsCurrentPlayerInGame() ) {
				var cpLayer = new LegacyGameInterfaceLayer( "ModUtilityPanels: Utility Panels",
					upDrawCallback,
					InterfaceScaleType.UI );
				layers.Insert( idx, cpLayer );
			}
		}


		////////////////

		private void DrawUP( SpriteBatch sb ) {
			try {
				var up = ModContent.GetInstance<UIControlPanel>();

				if( !ModControlPanelConfig.Instance.DisableControlPanel ) {
					up.UpdateToggler();

					up.DrawTogglerIf( sb );
				}

				if( this.LastSeenUPScreenWidth != Main.screenWidth || this.LastSeenUPScreenHeight != Main.screenHeight ) {
					this.LastSeenUPScreenWidth = Main.screenWidth;
					this.LastSeenUPScreenHeight = Main.screenHeight;
					//this.ControlPanelUI.Recalculate();

					up.RecalculateMe();
				}
			} catch( Exception e ) {
				LogLibraries.Warn( e.ToString() );
			}
		}
	}
}
