using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Services.AnimatedColor;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState {
		internal ISet<string> AlertTabs = new HashSet<string>();



		////////////////

		public void AddTabAlert( string tabName ) {
			this.AlertTabs.Add( tabName );
		}


		////////////////

		public bool IsTogglerUpdateAlertShown( out string tabName ) {
			tabName = this.AlertTabs.FirstOrDefault();

			return this.AlertTabs.Count > 0;
		}


		////////////////

		private void DrawTogglerAlert( SpriteBatch sb ) {
			Color color = AnimatedColors.Alert?.CurrentColor ?? Color.White;
			Vector2 pos = UIControlPanel.TogglerPosition;
			pos.Y += 6f;
			//pos.X += 56f - (Main.fontMouseText.MeasureString("New!").X * 0.5f);
			//pos.Y -= 4f;

			//sb.DrawString( Main.fontMouseText, "New!", pos, color );
			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: "New!",
				position: pos+new Vector2(-0.35f,-0.35f),
				color: Color.Black,
				rotation: 0f,
				origin: default( Vector2 ),
				scale: 0.64f,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: "New!",
				position: pos,
				color: color,
				rotation: 0f,
				origin: default( Vector2),
				scale: 0.6f,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}
	}
}
