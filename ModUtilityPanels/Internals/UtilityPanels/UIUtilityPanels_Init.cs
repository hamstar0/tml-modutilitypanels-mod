using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Internals.UtilityPanels.WelcomeTab;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	partial class UIUtilityPanels : UIState {
		public static float ContainerWidth = 600f;
		public static float ContainerHeight = 520f;
		
		public static Texture2D UtilityPanelsIcon { get; private set; }
		public static Texture2D UtilityPanelsIconHover { get; private set; }
		public static Texture2D UtilityPanelsIconOn { get; private set; }
		public static Texture2D UtilityPanelsIconOnAlert { get; private set; }



		////////////////

		private void InitializeSingleton() {
			var mymod = ModUtilityPanelsMod.Instance;

			UIUtilityPanels.UtilityPanelsIcon = mymod.GetTexture( "Internals/UtilityPanels/UtilityPanelsIcon" );
			UIUtilityPanels.UtilityPanelsIconHover = mymod.GetTexture( "Internals/UtilityPanels/UtilityPanelsIconHover" );
			UIUtilityPanels.UtilityPanelsIconOn = mymod.GetTexture( "Internals/UtilityPanels/UtilityPanelsIconOn" );
			UIUtilityPanels.UtilityPanelsIconOnAlert = mymod.GetTexture( "Internals/UtilityPanels/UtilityPanelsIconOnAlert" );

			//

			this.Theme = UITheme.Vanilla;

			this.CurrentTabName = UIUtilityPanels.DefaultTabName;
			this.Tabs[this.CurrentTabName] = new UIWelcomeUtilityPanelsTab( this.Theme );
			this.TabTitleOrder[this.CurrentTabName] = this.TabTitleOrder.Count;
		}


		////////////////

		private void InitializeComponents() {
			this.OuterContainer = new UIElement();
			this.OuterContainer.Width.Set( UIUtilityPanels.ContainerWidth, 0f );
			this.OuterContainer.Height.Set( UIUtilityPanels.ContainerHeight + UIUtilityPanels.TabButtonHeight, 0f );
			//this.OuterContainer.MaxWidth.Set( UIUtilityPanels.ContainerWidth, 0f );
			//this.OuterContainer.MaxHeight.Set( UIUtilityPanels.ContainerHeight, 0f );
			this.OuterContainer.HAlign = 0f;
			//this.MainElement.BackgroundColor = UtilityPanelsUI.MainBgColor;
			//this.MainElement.BorderColor = UtilityPanelsUI.MainEdgeColor;
			this.Append( this.OuterContainer );

			this.RecalculateContainerDimensions();

			this.MidContainer = new UIThemedPanel( this.Theme, false );
			this.MidContainer.SetPadding( 0f );
			this.MidContainer.PaddingTop = UIUtilityPanels.TabButtonHeight;
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
			this.InnerContainer.PaddingBottom = UIUtilityPanels.TabButtonHeight;
			this.MidContainer.Append( (UIElement)this.InnerContainer );

			this.InnerContainer.Initialize();
			
			this.IsInitialized = true;

			foreach( var kv in this.Tabs ) {
				this.InitializeTab( kv.Key, kv.Value );
			}

			UIUtilityPanels.DefaultTabButtonColor = Color.Gray;	//this.Theme.ButtonTextColor;
		}
	}
}
