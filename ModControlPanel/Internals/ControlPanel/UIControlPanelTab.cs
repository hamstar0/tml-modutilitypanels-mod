using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	public abstract class UIControlPanelTab : UIThemedPanel {
		/// @private
		public bool IsInitialized { get; private set; }

		public int? CustomWidth { get; protected set; }



		////////////////

		protected UIControlPanelTab( UITheme theme, int? customWidth = null ) : base( theme, false ) {
			this.CustomWidth = customWidth;
		}

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
