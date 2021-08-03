using System;
using Terraria.ModLoader;
using ModControlPanel.Internals.ControlPanel;


namespace ModControlPanel.Services.UI.ControlPanel {
	/// <summary>
	/// Supplies an interface to add to and manage control panel tabs. The control panel is accessible in the top left
	/// corner (by default) when the player's inventory is displayed.
	/// </summary>
	public class ControlPanelTabs {
		/// <summary>
		/// Adds a tab to the control panel UI.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="tab"></param>
		public static void AddTab( string title, UIControlPanelTab tab ) {
			var cp = ModContent.GetInstance<UIControlPanel>();

			cp?.AddTab( title, tab );
		}

		////////////////
		
		/// <summary>
		/// Gets the currently active tab (by name).
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentTab() {
			var cp = ModContent.GetInstance<UIControlPanel>();

			return cp?.CurrentTabName;
		}

		/// <summary>
		/// Opens a given tab (by name).
		/// </summary>
		/// <param name="tabName"></param>
		public static void OpenTab( string tabName ) {
			var cp = ModContent.GetInstance<UIControlPanel>();
			if( cp == null ) {
				return;
			}

			if( !cp.IsOpen ) {
				cp.Open();
			}

			cp.ChangeToTabIf( tabName );
		}


		////////////////

		/// <summary>
		/// Indicates if the control panel dialog is open.
		/// </summary>
		/// <returns></returns>
		public static bool IsDialogOpen() {
			var cp = ModContent.GetInstance<UIControlPanel>();
			return cp?.IsOpen ?? false;
		}

		/// <summary>
		/// Closes the control panel dialog.
		/// </summary>
		public static void CloseDialog() {
			var cp = ModContent.GetInstance<UIControlPanel>();

			cp?.Close();
			//this.SetDialogToClose = false;
			//this.Close();
		}


		////////////////

		/// <summary>
		/// Indicates that a given tab has important new information to be seen immediate.y
		/// </summary>
		/// <param name="tabName"></param>
		/// <param name="isPriority"></param>
		public static void AddTabAlert( string tabName, bool isPriority ) {
			var cp = ModContent.GetInstance<UIControlPanel>();
			cp?.AddTabAlert( tabName, isPriority );
		}

		/// <summary></summary>
		/// <param name="tabName"></param>
		public static void ClearTabAlert( string tabName ) {
			var cp = ModContent.GetInstance<UIControlPanel>();
			cp?.ClearTabAlert( tabName );
		}
	}
}
