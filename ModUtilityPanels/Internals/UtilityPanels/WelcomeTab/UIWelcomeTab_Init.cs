using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Classes.UI;


namespace ModUtilityPanels.Internals.UtilityPanels.WelcomeTab {
	/// @private
	partial class UIWelcomeUtilityPanelsTab : UIUtilityPanelsTab {
		private void InitializeComponents() {
			float top = 0;
			
			this.Theme.ApplyPanel( this );

			////////

			string supportMsg = UIWelcomeUtilityPanelsTab.SupportMessages[ this.RandomSupportTextIdx ];
			this.SupportUrl = new UIWebUrl( this.Theme, supportMsg, "https://www.patreon.com/hamstar0", false );
			this.SupportUrl.Top.Set( top, 0f );
			this.Append( this.SupportUrl );
			//this.SupportUrl.Left.Set( -this.SupportUrl.GetDimensions().Width, 1f );
			this.SupportUrl.Left.Set( -this.SupportUrl.GetDimensions().Width * 0.5f, 0.5f );
		}
	}
}
