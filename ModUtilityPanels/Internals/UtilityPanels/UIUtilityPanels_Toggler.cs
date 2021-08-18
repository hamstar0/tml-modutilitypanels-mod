using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	partial class UIUtilityPanels : UIState {
		private static Vector2 TogglerPosition {
			get {
				var config = ModUtilityPanelsConfig.Instance;
				int x = config.UtilityPanelsIconX < 0
					? Main.screenWidth + config.UtilityPanelsIconX
					: config.UtilityPanelsIconX;
				int y = config.UtilityPanelsIconY < 0
					? Main.screenHeight + config.UtilityPanelsIconY
					: config.UtilityPanelsIconY;

				if( x == 0 && y == 0 ) {
					if( Main.LocalPlayer.InfoAccMechShowWires && Main.LocalPlayer.rulerLine ) {
						y += 16;
					}
				}

				return new Vector2( x, y );
			}
		}



		////////////////

		public bool IsTogglerLit { get; private set; } = false;



		////////////////

		public bool IsTogglerShown() {
			return Main.playerInventory || this.AlertTabs.Values.Any( p=>p );
		}


		////////////////

		public void UpdateToggler() {
			this.RunTogglerMouseInteraction();

			if( this.IsTogglerLit ) {
				Main.LocalPlayer.mouseInterface = true;
			}
		}


		////////////////

		public void DrawTogglerIf( SpriteBatch sb ) {
			if( !this.IsTogglerShown() ) {
				return;
			}

			bool alertShown = this.IsTogglerUpdateAlertShown( out string _ );
			Texture2D tex;
			Color color;

			if( this.IsTogglerLit ) {
				tex = UIUtilityPanels.UtilityPanelsIconLit;
				color = new Color( 192, 192, 192, 192 );
			} else {
				tex = UIUtilityPanels.UtilityPanelsIcon;
				color = new Color( 160, 160, 160, 160 );
			}

			sb.Draw( tex, UIUtilityPanels.TogglerPosition, null, color );

			if( alertShown ) {
				this.DrawTogglerAlert( sb, (float)color.A / 255f );
			}

			if( this.IsTogglerLit ) {
				//if( alertShown ) {
				//	sb.DrawString(
				//		spriteFont: Main.fontMouseText,
				//		text: "New mod updates!",
				//		position: new Vector2( Main.mouseX + 8, Main.mouseY + 8 ),
				//		color: AnimatedColors.Alert.CurrentColor
				//	);
				//} else {
				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: "Mod Utility Panels",
					x: Main.mouseX + 8,
					y: Main.mouseY + 8,
					textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
					borderColor: Color.Black,
					origin: default
				);
				//}
			}
		}


		////////////////

		private void RunTogglerMouseInteraction() {
			bool isClick = Main.mouseLeft && Main.mouseLeftRelease && !this.HasClicked;
			Vector2 pos = UIUtilityPanels.TogglerPosition;
			Vector2 size = UIUtilityPanels.UtilityPanelsIcon.Size();

			this.IsTogglerLit = false;

			if( this.IsTogglerShown() ) {
				bool isMouseOver = Main.mouseX >= pos.X && Main.mouseX < ( pos.X + size.X )
								&& Main.mouseY >= pos.Y && Main.mouseY < ( pos.Y + size.Y );

				if( isMouseOver ) {
					if( isClick ) {
						if( this.IsOpen ) {
							this.Close();
						} else if( this.CanOpen() ) {
							this.OpenViaToggler();
						}
					}

					this.IsTogglerLit = true;
				}
			}

			this.HasClicked = isClick;
		}


		////

		private void OpenViaToggler() {
			if( !this.IsTogglerUpdateAlertShown( out string tabName ) ) {
				tabName = UIUtilityPanels.DefaultTabName;
			} else {
				this.AlertTabs.Remove( tabName );
			}

			UtilityPanelsTabs.OpenTab( tabName );

			//var mymod = ModUtilityPanelsMod.Instance;
			//Version oldVers;
			//Version newVers = UIUtilityPanels.AlertSinceVersion;
			//
			//if( Version.TryParse( mymod.Data.UtilityPanelsNewSince, out oldVers ) && oldVers != newVers ) {
			//	mymod.Data.UtilityPanelsNewSince = newVers.ToString();
			//	mymod.SaveModData();
			//}
		}
	}
}
