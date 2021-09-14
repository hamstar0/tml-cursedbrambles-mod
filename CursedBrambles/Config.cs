using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;
using ModLibsCore.Libraries.Debug;


namespace CursedBrambles {
	class MyFloatInputElement : FloatInputElement { }




	public partial class CursedBramblesConfig : ModConfig {
		public static CursedBramblesConfig Instance => ModContent.GetInstance<CursedBramblesConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[DefaultValue( true )]
		public bool BossesCreateBrambleTrail { get; set; } = true;

		[DefaultValue( true )]
		public bool PlayersCreateDefaultBrambleTrail  { get; set; } = true;


		////

		[Range( -1, 40000) ]
		[DefaultValue( 24 )]
		public int CursedBrambleWarpItemBlockingTileRange { get; set; } = 24;


		////

		[Range( 1, 60 * 60 * 60 * 24 )]
		[DefaultValue( 30 )]
		public int BrambleTicksPerDamage { get; set; } = 30;	//6

		[Range( 1, 9999999 )]
		[DefaultValue( 12 )]
		public int BrambleDamage { get; set; } = 12;

		[Range( 0f, 1f )]
		[DefaultValue( 0.2f )]
		public float BrambleStickiness { get; set; } = 0.2f;

		//[Range( 1, 128 )]
		//[DefaultValue( 4 )]
		//public int BrambleThickness { get; set; } = 4;

		//[Range( 0f, 1f )]
		//[DefaultValue( 0.15f )]
		//[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		//public float BrambleDensity { get; set; } = 0.15f;


		////

		[Range( 0, 10000 )]
		[DefaultValue( 64 )]
		public int BrambleErodeRandomAttemptsPerTickPerSmallWorldArea { get; set; } = 64;


		////

		[DefaultValue( true )]
		public bool PurificationPowderRemovesBrambles { get; set; } = true;


		////

		[Range( 0f, 9999f )]
		[DefaultValue( 32f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float DamageToPBGBarriers { get; set; } = 32f;
	}
}
