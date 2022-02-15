using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;


namespace ModUtilityPanels.Classes.UI {
	/// <summary>
	/// Contains the contents of a utility panel tab.
	/// </summary>
	public abstract class UIUtilityPanelsTab : UIThemedPanel {
		/// <summary>Runs on tab open.</summary>
		protected IList<Action<UIUtilityPanelsTab>> OnOpenTab { get; } = new List<Action<UIUtilityPanelsTab>>();

		 internal IList<Action<UIUtilityPanelsTab>> _OnOpenTab => this.OnOpenTab;


		////////////////

		/// @private
		public bool IsInitialized { get; private set; }

		/// <summary>True width.</summary>
		public int? CustomWidth { get; protected set; }



		////////////////

		/// <summary></summary>
		/// <param name="theme"></param>
		/// <param name="customWidth">True width.</param>
		protected UIUtilityPanelsTab( UITheme theme, int? customWidth = null ) : base( theme, false ) {
			this.CustomWidth = customWidth;
		}

		////////////////

		/// @private
		public sealed override void OnInitialize() {
			this.OnInitializeMe();
			this.IsInitialized = true;
		}


		/// <summary></summary>
		public abstract void OnInitializeMe();
	}
}
