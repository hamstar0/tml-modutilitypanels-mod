using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsCore.Services.Hooks.LoadHooks;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModControlPanel.Services.UI.ControlPanel;


namespace ModControlPanel.Internals.ControlPanel.ModControlPanel {
	partial class UIWelcomeControlPanelTab : UIControlPanelTab {
		private static IList<string> SupportMessages = new List<string> {
			"Buy me coffee for coding! :)",
			"Did you know I make other mods?",
			"Want more?",
			"Please support Mod Libs!"
		};



		////////////////

		private UIWebUrl SupportUrl = null;

		////

		private bool RequestClose = false;
		
		private int RandomSupportTextIdx = -1;



		////////////////

		public UIWelcomeControlPanelTab( UITheme theme ) : base( theme ) {
			this.Theme = theme;
		}

		////////////////

		public void AddCloseButton( UITextPanelButton button ) {
			this.Append( button );
		}

		////////////////

		public override void OnInitializeMe() {
			this.RandomSupportTextIdx = Main.rand.Next( UIWelcomeControlPanelTab.SupportMessages.Count );
			
			this.InitializeComponents();

			LoadHooks.AddWorldUnloadEachHook( () => {
				this.RandomSupportTextIdx = Main.rand.Next( UIWelcomeControlPanelTab.SupportMessages.Count );
				this.SupportUrl?.SetText( UIWelcomeControlPanelTab.SupportMessages[this.RandomSupportTextIdx] );
			} );
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );

			if( this.RequestClose ) {
				this.RequestClose = false;

				ControlPanelTabs.CloseDialog();

				return;
			}
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );
			
			if( this.SupportUrl.IsMouseHovering ) {
				if( !this.SupportUrl.WillDrawOwnHoverUrl ) {
					this.SupportUrl.DrawHoverEffects( sb );
				}
			}
		}
	}
}
