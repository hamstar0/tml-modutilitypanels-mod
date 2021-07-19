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
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState, ILoadable {
		public const string DefaultTabName = "Welcome";

		////////////////

		public static int TabButtonWidth => 160;
		public static int TabButtonHeight => 24;

		public static Color DefaultTabButtonColor { get; private set; }



		////////////////

		private UserInterface Backend = null;

		////

		private IDictionary<string, UIControlPanelTab> Tabs = new Dictionary<string, UIControlPanelTab>();
		private IDictionary<string, int> TabTitleOrder = new Dictionary<string, int>();

		private IList<UITextPanelButton> TabButtons = new List<UITextPanelButton>();
		private IDictionary<string, UITextPanelButton> TabButtonsByName = new Dictionary<string, UITextPanelButton>();
		private IDictionary<string, bool> TabButtonHover = new Dictionary<string, bool>();

		////

		private UITheme Theme = null;

		private UIElement OuterContainer = null;
		private UIPanel MidContainer = null;
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

				ModControlPanelMod.Instance.PostInitializeControlPanel();
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
			

			foreach( string tabName in this.TabButtonsByName.Keys ) {
				this.ApplyTabButtonMouseInteractivity( tabName );
			}

			base.Draw( sb );
		}
	}
}
