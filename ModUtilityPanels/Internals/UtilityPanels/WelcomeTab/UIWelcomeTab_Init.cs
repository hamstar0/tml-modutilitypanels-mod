using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Libraries.TModLoader.Mods;
using ModLibsCore.Services.TML;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;
using ModUtilityPanels.Classes.UI;


namespace ModUtilityPanels.Internals.UtilityPanels.WelcomeTab {
	/// @private
	partial class UIWelcomeUtilityPanelsTab : UIUtilityPanelsTab {
		private void InitializeComponents() {
			this.Theme.ApplyPanel( this );

			////////

			var title = new UIThemedText( this.Theme, false, "Now playing:", true );
			this.Append( (UIElement)title );

			var modListPanel = new UIThemedPanel( this.Theme, false );
			{
				modListPanel.Top.Set( 24f, 0f );
				modListPanel.Width.Set( 0f, 1f );
				modListPanel.Height.Set( -24f, 1f );
				modListPanel.HAlign = 0f;
				modListPanel.SetPadding( 4f );
				modListPanel.PaddingTop = 0.0f;
				modListPanel.BackgroundColor = this.Theme.ListBgColor;
				modListPanel.BorderColor = this.Theme.ListEdgeColor;
				this.Append( (UIElement)modListPanel );

				var modListElem = new UIList();
				{
					modListElem.Width.Set( -25, 1f );
					modListElem.Height.Set( 0f, 1f );
					modListElem.HAlign = 0f;
					modListElem.ListPadding = 4f;
					modListElem.SetPadding( 0f );
					modListPanel.Append( (UIElement)modListElem );

					var scrollbar = new UIScrollbar();
					{
						scrollbar.Top.Set( 8f, 0f );
						scrollbar.Height.Set( -16f, 1f );
						scrollbar.SetView( 100f, 1000f );
						scrollbar.HAlign = 1f;
						modListPanel.Append( (UIElement)scrollbar );
						modListElem.SetScrollbar( scrollbar );
					}
				}

				this.PopulateListOfMods( modListElem );
			}

			//

			string supportMsg = UIWelcomeUtilityPanelsTab.SupportMessages[ this.RandomSupportTextIdx ];
			this.SupportUrl = new UIWebUrl( this.Theme, supportMsg, "https://ko-fi.com/hamstar1", false );
			this.SupportUrl.Top.Set( 0f, 1f );
			this.Append( this.SupportUrl );
			//this.SupportUrl.Left.Set( -this.SupportUrl.GetDimensions().Width, 1f );
			this.SupportUrl.Left.Set( -this.SupportUrl.GetDimensions().Width * 0.5f, 0.5f );
		}


		////

		private void PopulateListOfMods( UIList list ) {
			IEnumerable<Mod> mods = ModLoader.Mods
				.Where( m => m.Name != "ModLoader" )
				.OrderBy( m => m.Name );

			//

			int i = 1;
			foreach( Mod mod in mods ) {
				var elem = new UIModData( UITheme.Vanilla, i, mod );
				i++;

				list.Add( elem );
			}
		}
	}
}
