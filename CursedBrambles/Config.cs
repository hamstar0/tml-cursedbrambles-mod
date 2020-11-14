using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Helpers.Debug;


namespace CursedBrambles {
	class MyFloatInputElement : FloatInputElement { }




	public partial class CursedBramblesConfig : ModConfig {
		public static CursedBramblesConfig Instance { get; internal set; }



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[DefaultValue( true )]
		public bool BossesCreateBrambleTrail { get; set; } = true;

		[DefaultValue( true )]
		public bool PlayersCreateBrambleTrail { get; set; } = true;
	}
}
