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

		public bool IsTogglerHovered { get; private set; } = false;



		////////////////

		public bool IsTogglerShown() {
			return Main.playerInventory || this.AlertTabs.Values.Any( p=>p );
		}


		////////////////

		public void UpdateToggler() {
			this.RunTogglerMouseInteraction();

			if( this.IsTogglerHovered ) {
				Main.LocalPlayer.mouseInterface = true;
			}
		}


		////////////////

		public void DrawTogglerIf( SpriteBatch sb ) {
			if( !this.IsTogglerShown() ) {
				return;
			}

			bool alertShown = this.IsTogglerUpdateAlertShown( out string alertTab, out bool isPriority );
			Texture2D tex = alertShown
				? isPriority
					? UIUtilityPanels.UtilityPanelsIconOnAlert
					: UIUtilityPanels.UtilityPanelsIconOn
				: this.IsTogglerHovered
					? UIUtilityPanels.UtilityPanelsIconHover
					: UIUtilityPanels.UtilityPanelsIcon;

			sb.Draw( tex, UIUtilityPanels.TogglerPosition, null, Color.White );

			if( this.IsTogglerHovered ) {
				string text = alertShown
					? "New "+alertTab+" Content!"
					: "Mod Utility Panels";
				Color color = new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor );

				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: text,
					x: Main.mouseX + 8,
					y: Main.mouseY + 8,
					textColor: color,
					borderColor: Color.Black,
					origin: default
				);
			}
		}


		////////////////

		private void RunTogglerMouseInteraction() {
			bool isClick = Main.mouseLeft && Main.mouseLeftRelease && !this.HasClicked;
			Vector2 pos = UIUtilityPanels.TogglerPosition;
			Vector2 size = UIUtilityPanels.UtilityPanelsIcon.Size();

			this.IsTogglerHovered = false;

			if( this.IsTogglerShown() ) {
				this.IsTogglerHovered = Main.mouseX >= pos.X && Main.mouseX < ( pos.X + size.X )
									 && Main.mouseY >= pos.Y && Main.mouseY < ( pos.Y + size.Y );

				if( this.IsTogglerHovered ) {
					if( isClick ) {
						if( this.IsOpen ) {
							this.Close();
						} else if( this.CanOpen() ) {
							this.OpenViaToggler();
						}
					}
				}
			}

			this.HasClicked = isClick;
		}


		////

		private void OpenViaToggler() {
			if( !this.IsTogglerUpdateAlertShown(out string tabName, out _) ) {
				tabName = UIUtilityPanels.DefaultTabName;
			} else {
				this.AlertTabs.Remove( tabName );
			}

			UtilityPanelsTabs.OpenTab( tabName );
		}
	}
}
