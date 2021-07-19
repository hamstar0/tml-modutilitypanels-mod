﻿using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModControlPanel {
	/// @private
	public partial class ModControlPanelMod : Mod {
		public static ModControlPanelMod Instance { get; private set; }



		////////////////

		private int LastSeenCPScreenWidth = -1;
		private int LastSeenCPScreenHeight = -1;

		public ModHotKey ControlPanelHotkey = null;



		////////////////

		public bool MouseInterface { get; private set; }


		////////////////

		public event Action OnControlPanelInitialize;



		////////////////

		public override void Load() {
			ModControlPanelMod.Instance = this;

			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Control Panel", "O" );
		}

		////

		internal void PostInitializeControlPanel() {
			this.OnControlPanelInitialize?.Invoke();
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );

				this.ControlPanelHotkey = null;
			} catch( Exception e ) {
				this.Logger.Warn( "!ModControlPanel.ModControlPanelMod.UnloadFull - " + e.ToString() ); //was Error(...)
			}

			ModControlPanelMod.Instance = null;
		}
	}
}
