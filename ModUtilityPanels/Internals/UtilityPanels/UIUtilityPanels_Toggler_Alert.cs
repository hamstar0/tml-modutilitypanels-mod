using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	partial class UIUtilityPanels : UIState {
		private IDictionary<string, bool> AlertTabs = new Dictionary<string, bool>();



		////////////////

		public void AddTabAlert( string tabName, bool isPriority ) {
			this.AlertTabs[ tabName ] = isPriority;
		}

		public void ClearTabAlert( string tabName ) {
			this.AlertTabs.Remove( tabName );
		}


		////////////////

		public bool IsTogglerUpdateAlertShown( out string tabName, out bool isPriority ) {
			tabName = this.AlertTabs
					.Where( kv => kv.Value )
					.Select( kv => kv.Key )
					.FirstOrDefault();

			isPriority = tabName != null;

			if( !isPriority ) {
				tabName = this.AlertTabs
					.Select( kv => kv.Key )
					.FirstOrDefault();
			}

			return this.AlertTabs.Count > 0;
		}
	}
}
