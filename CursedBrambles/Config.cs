using System;
using System.ComponentModel;
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


		[Range( -1, 40000) ]
		[DefaultValue( 24 )]
		public int CursedBrambleWarpItemBlockingTileRange { get; set; } = 24;


		[Range( 1, 60 * 60 * 60 * 24 )]
		[DefaultValue( 6 )]
		public int BrambleTicksPerDamage { get; set; } = 6;

		[Range( 1, 9999999 )]
		[DefaultValue( 20 )]
		public int BrambleDamage { get; set; } = 10;

		[Range( 0f, 1f )]
		[DefaultValue( 0.2f )]
		public float BrambleStickiness { get; set; } = 0.2f;

		[Range( 1, 128 )]
		[DefaultValue( 4 )]
		public int BrambleThickness { get; set; } = 4;

		[Range( 0f, 1f )]
		[DefaultValue( 0.15f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BrambleDensity { get; set; } = 0.15f;
	}
}
