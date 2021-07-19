using Terraria;
using Terraria.UI;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace ModControlPanel.Internals.ControlPanel {
	partial class UIControlPanel : UIState {
		public UIControlPanelTab GetTab( string name ) {
			return this.Tabs.GetOrDefault( name );
		}


		////////////////

		private void InitializeTab( string title, UIControlPanelTab tab ) {
			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

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

		public bool ChangeToTabIf( string tabName ) {
			if( tabName == this.CurrentTabName ) {
				return true;
			}

			UIControlPanelTab tab;
			if( !this.Tabs.TryGetValue(tabName, out tab) ) {
				return false;
			}

			this.ChangeToTabElement( tab );

			this.CurrentTabName = tabName;
			
			return true;
		}

		////

		public void ChangeToTabElement( UIControlPanelTab tab ) {
			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

			this.MidContainer.RemoveChild( this.InnerContainer );
			this.InnerContainer.Remove();

			this.InnerContainer = tab;
			this.InnerContainer.PaddingBottom = UIControlPanel.TabButtonHeight;
			this.MidContainer.Append( this.InnerContainer );

			if( !tab.IsInitialized ) {
				tab.Initialize();
			}

			//

			if( tab.CustomWidth.HasValue ) {
				this.OuterContainer.Width.Set( tab.CustomWidth.Value, 0f );
			} else {
				this.OuterContainer.Width.Set( UIControlPanel.ContainerWidth, 0f );
			}

			this.RecalculateContainerDimensions();

			this.Recalculate();
			this.Recalculate();
			this.OuterContainer.Recalculate();
			this.InnerContainer.Recalculate();
		}
	}
}
