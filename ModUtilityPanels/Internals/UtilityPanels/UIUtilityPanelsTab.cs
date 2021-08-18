using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	public abstract class UIUtilityPanelsTab : UIThemedPanel {
		/// @private
		public bool IsInitialized { get; private set; }

		public int? CustomWidth { get; protected set; }



		////////////////

		protected UIUtilityPanelsTab( UITheme theme, int? customWidth = null ) : base( theme, false ) {
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
