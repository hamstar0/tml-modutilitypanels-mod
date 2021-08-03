using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModControlPanel.Internals.ControlPanel;


namespace ModControlPanel {
	/// @private
	public partial class ModControlPanelMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( idx == -1 ) { return; }

			//

			GameInterfaceDrawMethod cpDrawCallback = () => {
				this.DrawCP( Main.spriteBatch );
				return true;
			};

			//

			if( LoadLibraries.IsCurrentPlayerInGame() ) {
				var cpLayer = new LegacyGameInterfaceLayer( "ModControlPanel: Control Panel",
					cpDrawCallback,
					InterfaceScaleType.UI );
				layers.Insert( idx, cpLayer );
			}
		}


		////////////////

		private void DrawCP( SpriteBatch sb ) {
			try {
				var cp = ModContent.GetInstance<UIControlPanel>();

				if( !ModControlPanelConfig.Instance.DisableControlPanel ) {
					cp.UpdateToggler();

					cp.DrawTogglerIf( sb );
				}

				if( this.LastSeenCPScreenWidth != Main.screenWidth || this.LastSeenCPScreenHeight != Main.screenHeight ) {
					this.LastSeenCPScreenWidth = Main.screenWidth;
					this.LastSeenCPScreenHeight = Main.screenHeight;
					//this.ControlPanelUI.Recalculate();

					cp.RecalculateMe();
				}
			} catch( Exception e ) {
				LogLibraries.Warn( e.ToString() );
			}
		}
	}
}
