using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	public abstract class UIControlPanelTab : UIPanel {
		/// @private
		public UITheme Theme { get; protected set; }
		/// @private
		public bool IsInitialized { get; private set; }



		////////////////

		/// @private
		public sealed override void OnInitialize() {
			this.OnInitializeMe();
			this.IsInitialized = true;
		}


		/// @private
		public abstract void OnInitializeMe();
	}
}
