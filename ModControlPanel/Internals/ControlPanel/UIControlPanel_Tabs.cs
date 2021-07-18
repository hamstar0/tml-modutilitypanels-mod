using Terraria;
using Terraria.ID;
using Terraria.UI;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsUI.Classes.UI.Elements;


namespace ModControlPanel.Internals.ControlPanel {
	partial class UIControlPanel : UIState {
		public UIControlPanelTab GetTab( string name ) {
			return this.Tabs.GetOrDefault( name );
		}


		////////////////

		private void InitializeTab( string title, UIControlPanelTab tab ) {
			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

			this.AddTabCloseButton( title );
			this.AddTabButton( title );
		}


		////////////////

		public void AddTab( string title, UIControlPanelTab tab ) {
			this.Tabs[ title ] = tab;
			this.TabTitleOrder[ title ] = this.TabTitleOrder.Count;

			if( this.IsInitialized ) {
				this.InitializeTab( title, tab );
			}
		}


		////////////////

		private void AddTabCloseButton( string title ) {
			UIControlPanelTab tab = this.Tabs[title];
			var closeButton = new UITextPanelButton( tab.Theme, "X" );

			closeButton.Top.Set( -8f, 0f );
			closeButton.Left.Set( -16f, 1f );
			closeButton.Width.Set( 24f, 0f );
			closeButton.Height.Set( 24f, 0f );

			closeButton.OnClick += ( _, __ ) => {
				this.Close();
				Main.PlaySound( SoundID.MenuClose );
			};
			closeButton.OnMouseOver += ( _, __ ) => {
				tab.Theme.ApplyButtonLit( closeButton );
			};
			closeButton.OnMouseOut += ( _, __ ) => {
				tab.Theme.ApplyButton( closeButton );
			};

			tab.Append( closeButton );
		}

		private void AddTabButton( string title ) {
			UIControlPanelTab tab = this.Tabs[ title ];
			int idx = this.TabTitleOrder[ title ];

			int posX = UIControlPanel.TabButtonWidth * idx;

			var button = new UITextPanelButton( tab.Theme, title );
			button.Left.Set( (float)posX, 0f );
			button.Top.Set( -UIControlPanel.TabButtonHeight, 0f );
			button.Width.Set( UIControlPanel.TabButtonWidth, 0f );
			button.Height.Set( UIControlPanel.TabButtonHeight, 0f );
			button.OnClick += ( _, __ ) => {
				this.ChangeToTab( title );
			};

			this.MidContainer.Append( button );

			this.OuterContainer.Recalculate();

			//

			this.TabButtons.Add( button );
			this.TabButtonHover.Add( false );
		}


		////////////////

		public bool ChangeToTab( string tabName ) {
			if( tabName == this.CurrentTabName ) {
				return true;
			}

			UIControlPanelTab tab;
			if( !this.Tabs.TryGetValue(tabName, out tab) ) {
				return false;
			}

			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

			if( tab.CustomWidth.HasValue ) {
				this.OuterContainer.Width.Set( tab.CustomWidth.Value, 0f );
			} else {
				this.OuterContainer.Width.Set( UIControlPanel.ContainerWidth, 0f );
			}

			this.MidContainer.RemoveChild( this.InnerContainer );
			this.InnerContainer.Remove();

			this.InnerContainer = tab;
			this.InnerContainer.PaddingBottom = UIControlPanel.TabButtonHeight;
			this.MidContainer.Append( this.InnerContainer );

			if( !tab.IsInitialized ) {
				tab.Initialize();
			}

			this.Recalculate();
			this.OuterContainer.Recalculate();
			this.InnerContainer.Recalculate();

			this.CurrentTabName = tabName;
			
			return true;
		}
	}
}
