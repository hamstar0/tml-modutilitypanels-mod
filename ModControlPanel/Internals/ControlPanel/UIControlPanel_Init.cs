using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Internals.ControlPanel.ModControlPanel;


namespace ModUtilityPanels.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState {
		public static float ContainerWidth = 600f;
		public static float ContainerHeight = 520f;
		
		public static Texture2D ControlPanelIcon { get; private set; }
		public static Texture2D ControlPanelIconLit { get; private set; }
		public static Texture2D AlertBorder1 { get; private set; }
		public static Texture2D AlertBorder2 { get; private set; }
		public static Texture2D AlertBorder3 { get; private set; }



		////////////////

		private void InitializeSingleton() {
			var mymod = ModUtilityPanelsMod.Instance;

			UIControlPanel.ControlPanelIcon = mymod.GetTexture( "Internals/ControlPanel/ControlPanelIcon" );
			UIControlPanel.ControlPanelIconLit = mymod.GetTexture( "Internals/ControlPanel/ControlPanelIconLit" );
			UIControlPanel.AlertBorder1 = mymod.GetTexture( "Internals/ControlPanel/AlertBorder1" );
			UIControlPanel.AlertBorder2 = mymod.GetTexture( "Internals/ControlPanel/AlertBorder2" );
			UIControlPanel.AlertBorder3 = mymod.GetTexture( "Internals/ControlPanel/AlertBorder3" );

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
			//this.OuterContainer.MaxWidth.Set( UIControlPanel.ContainerWidth, 0f );
			//this.OuterContainer.MaxHeight.Set( UIControlPanel.ContainerHeight, 0f );
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

			var closeButton = new UITextPanelButton( this.Theme, "X" );
			closeButton.Top.Set( -24f, 0f );
			closeButton.Left.Set( -24f, 1f );
			closeButton.Width.Set( 24f, 0f );
			closeButton.Height.Set( 24f, 0f );
			closeButton.OnMouseOver += ( _, __ ) => this.Theme.ApplyButtonLit( closeButton );
			closeButton.OnMouseOut += ( _, __ ) => this.Theme.ApplyButton( closeButton );
			closeButton.OnClick += ( _, __ ) => {
				this.Close();
				Main.PlaySound( SoundID.MenuClose );
			};
			this.MidContainer.Append( closeButton );

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

			UIControlPanel.DefaultTabButtonColor = Color.Gray;	//this.Theme.ButtonTextColor;
		}
	}
}
