using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Libraries.DotNET.Reflection;
using ModLibsCore.Services.Timers;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState, ILoadable {
		public const string DefaultTabName = "Welcome";

		////////////////

		public static int TabButtonWidth => 160;
		public static int TabButtonHeight => 24;



		////////////////

		private UserInterface Backend = null;

		////

		private IDictionary<string, UIControlPanelTab> Tabs = new Dictionary<string, UIControlPanelTab>();
		private IDictionary<string, int> TabTitleOrder = new Dictionary<string, int>();

		private IList<UITextPanelButton> TabButtons = new List<UITextPanelButton>();
		private IList<bool> TabButtonHover = new List<bool>();

		////

		private UITheme Theme = null;
		private UIElement OuterContainer = null;
		private UIPanel InnerContainer = null;

		private bool IsInitialized = false;
		private bool HasClicked = false;


		////////////////

		public bool IsOpen { get; private set; } = false;

		public string CurrentTabName { get; private set; } = "";

		////

		public UIControlPanelTab CurrentTab => this.Tabs.GetOrDefault( this.CurrentTabName );



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.InitializeSingleton();
			}
		}

		void ILoadable.OnModsUnload() { }


		////////////////

		public override void OnInitialize() {
			this.InitializeComponents();
		}


		////////////////

		public void UpdateGlobal() {
			if( !Main.inFancyUI ) {
				this.IsOpen = false;
			}
		}

		public override void Update( GameTime gameTime ) {
			if( !this.IsOpen ) { return; }

			if( Main.playerInventory || Main.npcChatText != "" ) {
				this.Close();
				return;
			}

			if( this.OuterContainer.IsMouseHovering ) {
				Main.LocalPlayer.mouseInterface = true;
			}

			base.Update( gameTime );
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			if( !this.IsOpen ) { return; }

			for( int i=0; i<this.TabButtons.Count; i++ ) {
				this.ApplyTabButtonMouseInteractivity( i );
			}

			base.Draw( sb );
		}


		////////////////

		private void ApplyTabButtonMouseInteractivity( int idx ) {
			UITextPanelButton button = this.TabButtons[idx];

			if( button.GetOuterDimensions().ToRectangle().Contains(Main.mouseX, Main.mouseY) ) {
				this.ApplyTabButtonMouseOver( idx );
			} else {
				this.ApplyTabButtonMouseOut( idx );
			}
		}

		private void ApplyTabButtonMouseOver( int idx ) {
			UITextPanelButton button = this.TabButtons[idx];
			var evt = new UIMouseEvent( button, new Vector2( Main.mouseX, Main.mouseY ) );

			if( !this.TabButtonHover[idx] ) {
				this.TabButtonHover[idx] = true;

				ReflectionLibraries.Set( button, "_isMouseHovering", true );

				Timers.RunNow( () => button.MouseOver(evt) );
			}
			
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				Timers.RunNow( () => {
					button.Click(evt);
				} );
			}
		}

		private void ApplyTabButtonMouseOut( int idx ) {
			UITextPanelButton button = this.TabButtons[idx];

			if( this.TabButtonHover[idx] ) {
				this.TabButtonHover[idx] = false;

				Timers.RunNow( () => {
					button.MouseOut( new UIMouseEvent( button, new Vector2(Main.mouseX, Main.mouseY) ) );
				} );
			}
		}
	}
}
