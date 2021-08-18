using System;
using Terraria.ModLoader;
using ModUtilityPanels.Internals.UtilityPanels;


namespace ModUtilityPanels.Services.UI.UtilityPanels {
	/// <summary>
	/// Supplies an interface to add to and manage utility panel tabs. The control panel is accessible in the top
	/// left corner (by default) when the player's inventory is displayed.
	/// </summary>
	public class UtilityPanelsTabs {
		/// <summary>
		/// Adds a tab to the utility panel UI.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="tab"></param>
		public static void AddTab( string title, UIUtilityPanelsTab tab ) {
			var up = ModContent.GetInstance<UIUtilityPanels>();

			up?.AddTab( title, tab );
		}

		////////////////
		
		/// <summary>
		/// Gets the currently active tab (by name).
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentTab() {
			var up = ModContent.GetInstance<UIUtilityPanels>();

			return up?.CurrentTabName;
		}

		/// <summary>
		/// Opens a given tab (by name).
		/// </summary>
		/// <param name="tabName"></param>
		public static void OpenTab( string tabName ) {
			var up = ModContent.GetInstance<UIUtilityPanels>();
			if( up == null ) {
				return;
			}

			if( !up.IsOpen ) {
				up.Open();
			}

			up.ChangeToTabIf( tabName );

			up.ClearTabAlert( tabName );
		}


		////////////////

		/// <summary>
		/// Indicates if the utility panel dialog is open.
		/// </summary>
		/// <returns></returns>
		public static bool IsDialogOpen() {
			var up = ModContent.GetInstance<UIUtilityPanels>();
			return up?.IsOpen ?? false;
		}

		/// <summary>
		/// Closes the utility panel dialog.
		/// </summary>
		public static void CloseDialog() {
			var up = ModContent.GetInstance<UIUtilityPanels>();

			up?.Close();
			//this.SetDialogToClose = false;
			//this.Close();
		}


		////////////////

		/// <summary>
		/// Indicates that a given tab has important new information to be seen immediate.
		/// </summary>
		/// <param name="tabName"></param>
		/// <param name="isPriority"></param>
		public static void AddTabAlert( string tabName, bool isPriority ) {
			var up = ModContent.GetInstance<UIUtilityPanels>();
			up?.AddTabAlert( tabName, isPriority );
		}

		/// <summary></summary>
		/// <param name="tabName"></param>
		public static void ClearTabAlert( string tabName ) {
			var up = ModContent.GetInstance<UIUtilityPanels>();
			up?.ClearTabAlert( tabName );
		}
	}
}
