using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModUtilityPanels {
	/// @private
	public partial class ModUtilityPanelsMod : Mod {
		public static ModUtilityPanelsMod Instance { get; private set; }



		////////////////

		private int LastSeenUPScreenWidth = -1;
		private int LastSeenUPScreenHeight = -1;

		public ModHotKey UtilityPanelsHotkey = null;



		////////////////

		public bool MouseInterface { get; private set; }


		////////////////

		public event Action OnUtilityPanelsInitialize;



		////////////////

		public override void Load() {
			ModUtilityPanelsMod.Instance = this;

			this.UtilityPanelsHotkey = this.RegisterHotKey( "Toggle Utility Panels", "O" );
		}

		////

		internal void PostInitializeUtilityPanels() {
			this.OnUtilityPanelsInitialize?.Invoke();
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );

				this.UtilityPanelsHotkey = null;
			} catch( Exception e ) {
				this.Logger.Warn( "!ModUtilityPanels.ModModUtilityPanels.UnloadFull - " + e.ToString() ); //was Error(...)
			}

			ModUtilityPanelsMod.Instance = null;
		}
	}
}
