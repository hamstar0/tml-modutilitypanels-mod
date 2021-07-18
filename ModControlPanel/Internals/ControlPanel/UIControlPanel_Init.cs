using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel.Internals.ControlPanel.ModControlPanel;
using ModLibsUI.Classes.UI.Elements;

namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState {
		public static float ContainerWidth = 600f;
		public static float ContainerHeight = 520f;
		
		public static Texture2D ControlPanelIcon { get; private set; }
		public static Texture2D ControlPanelIconLit { get; private set; }



		////////////////

		private void InitializeSingleton() {
			var mymod = ModControlPanelMod.Instance;

			UIControlPanel.ControlPanelIcon = mymod.GetTexture( "Internals/ControlPanel/ControlPanelIcon" );
			UIControlPanel.ControlPanelIconLit = mymod.GetTexture( "Internals/ControlPanel/ControlPanelIconLit" );

			//

			this.Theme = UITheme.Vanilla;

			this.CurrentTabName = UIControlPanel.DefaultTabName;
			this.Tabs[this.CurrentTabName] = new UIWelcomeControlPanelTab( this.Theme );
			this.TabTitleOrder[this.CurrentTabName] = this.TabTitleOrder.Count;
		}


		////////////////

		private void InitializeComponents() {
			this.OuterContainer = new UIElement();
			this.OuterContainer.Width.Set( UIControlPanel.ContainerWidth, 0f );
			this.OuterContainer.Height.Set( UIControlPanel.ContainerHeight + UIControlPanel.TabButtonHeight, 0f );
			this.OuterContainer.MaxWidth.Set( UIControlPanel.ContainerWidth, 0f );
			this.OuterContainer.MaxHeight.Set( UIControlPanel.ContainerHeight, 0f );
			this.OuterContainer.HAlign = 0f;
			//this.MainElement.BackgroundColor = ControlPanelUI.MainBgColor;
			//this.MainElement.BorderColor = ControlPanelUI.MainEdgeColor;
			this.Append( this.OuterContainer );

			this.RecalculateContainerDimensions();

			this.MidContainer = new UIThemedPanel( this.Theme, false );
			this.MidContainer.SetPadding( 0f );
			this.MidContainer.PaddingTop = UIControlPanel.TabButtonHeight;
			this.MidContainer.Width.Set( 0f, 1f );
			this.MidContainer.Height.Set( 0f, 1f );
			this.MidContainer.HAlign = 0f;
			this.OuterContainer.Append( (UIElement)this.MidContainer );

			this.InnerContainer = this.CurrentTab;
			this.InnerContainer.Width.Set( 0f, 1f );
			this.InnerContainer.Height.Set( 0f, 1f );
			this.InnerContainer.PaddingBottom = UIControlPanel.TabButtonHeight;
			this.MidContainer.Append( (UIElement)this.InnerContainer );

			this.InnerContainer.Initialize();
			
			this.IsInitialized = true;

			foreach( var kv in this.Tabs ) {
				this.InitializeTab( kv.Key, kv.Value );
			}
		}
	}
}
