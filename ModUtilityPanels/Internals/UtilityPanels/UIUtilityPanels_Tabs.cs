using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace ModUtilityPanels.Internals.UtilityPanels {
	partial class UIUtilityPanels : UIState {
		public UIUtilityPanelsTab GetTab( string name ) {
			return this.Tabs.GetOrDefault( name );
		}


		////////////////

		private void InitializeTab( string title, UIUtilityPanelsTab tab ) {
			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

			this.AddTabButton( title );
		}


		////////////////

		public void AddTab( string title, UIUtilityPanelsTab tab ) {
			this.Tabs[ title ] = tab;
			this.TabTitleOrder[ title ] = this.TabTitleOrder.Count;

			if( this.IsInitialized ) {
				this.InitializeTab( title, tab );
			}
		}


		////////////////

		public bool ChangeToTabIf( string tabName ) {
			UIUtilityPanelsTab tab;
			if( !this.Tabs.TryGetValue(tabName, out tab) ) {
				return false;
			}

			foreach( Action<UIUtilityPanelsTab> openHook in tab._OnOpenTab ) {
				openHook.Invoke( this.CurrentTab );
			}

			if( tabName == this.CurrentTabName ) {
				return true;
			}

			this.ChangeToTabElement( tab );

			this.CurrentTabName = tabName;
			
			return true;
		}

		////

		private void ChangeToTabElement( UIUtilityPanelsTab tab ) {
			tab.Width.Set( 0f, 1f );
			tab.Height.Set( 0f, 1f );

			this.MidContainer.RemoveChild( this.InnerContainer );
			this.InnerContainer.Remove();

			this.InnerContainer = tab;
			this.InnerContainer.PaddingBottom = UIUtilityPanels.TabButtonHeight;
			this.MidContainer.Append( this.InnerContainer );

			if( !tab.IsInitialized ) {
				tab.Initialize();
			}

			//

			if( tab.CustomWidth.HasValue ) {
				this.OuterContainer.Width.Set( tab.CustomWidth.Value, 0f );
			} else {
				this.OuterContainer.Width.Set( UIUtilityPanels.ContainerWidth, 0f );
			}

			this.RecalculateContainerDimensions();

			this.Recalculate();
			this.Recalculate();
			this.OuterContainer.Recalculate();
			this.InnerContainer.Recalculate();
		}
	}
}
