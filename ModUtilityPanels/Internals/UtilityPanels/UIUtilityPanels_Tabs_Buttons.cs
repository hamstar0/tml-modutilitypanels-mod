using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Reflection;
using ModLibsCore.Services.Timers;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Classes.UI;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	partial class UIUtilityPanels : UIState, ILoadable {
		private UITextPanelButton AddTabButton( string title ) {
			UIUtilityPanelsTab tab = this.Tabs[title];
			int idx = this.TabTitleOrder[title];

			int posX = UIUtilityPanels.TabButtonWidth * idx;

			var button = new UITextPanelButton( tab.Theme, title );
			button.Left.Set( (float)posX, 0f );
			button.Top.Set( -UIUtilityPanels.TabButtonHeight, 0f );
			button.Width.Set( UIUtilityPanels.TabButtonWidth, 0f );
			button.Height.Set( UIUtilityPanels.TabButtonHeight, 0f );
			button.OnClick += ( _, __ ) => {
				UtilityPanelsTabs.OpenTab( title );
			};

			this.MidContainer.Append( button );

			this.OuterContainer.Recalculate();

			//

			this.TabButtons.Add( button );
			this.TabButtonsByName[title] = button;
			this.TabButtonHover[title] = false;

			return button;
		}


		////////////////

		private void ApplyTabButtonMouseInteractivity( string tabName ) {
			UITextPanelButton button = this.TabButtonsByName[ tabName ];

			if( button.GetOuterDimensions().ToRectangle().Contains(Main.mouseX, Main.mouseY) ) {
				this.ApplyTabButtonMouseOver( tabName );
			} else {
				this.ApplyTabButtonMouseOut( tabName );
			}
		}

		private void ApplyTabButtonMouseOver( string tabName ) {
			UITextPanelButton button = this.TabButtonsByName[tabName];
			var evt = new UIMouseEvent( button, new Vector2( Main.mouseX, Main.mouseY ) );

			if( !this.TabButtonHover[tabName] ) {
				this.TabButtonHover[tabName] = true;

				ReflectionLibraries.Set( button, "_isMouseHovering", true );

				Timers.RunNow( () => button.MouseOver(evt) );
			}
			
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				Timers.RunNow( () => {
					button.Click(evt);
				} );
			}
		}

		private void ApplyTabButtonMouseOut( string tabName ) {
			UITextPanelButton button = this.TabButtonsByName[tabName];

			if( this.TabButtonHover[tabName] ) {
				this.TabButtonHover[tabName] = false;

				Timers.RunNow( () => {
					button.MouseOut( new UIMouseEvent( button, new Vector2(Main.mouseX, Main.mouseY) ) );
				} );
			}
		}

		private void UpdateTabButtonStyling( string tabName ) {
			UITextPanelButton button = this.TabButtonsByName[tabName];

			if( this.AlertTabs.ContainsKey( tabName ) ) {
				button.TextColor = Color.Yellow;
			} else if( tabName == this.CurrentTabName ) {
				button.TextColor = Color.White;
			} else {
				button.TextColor = UIUtilityPanels.DefaultTabButtonColor;
			}
		}
	}
}
