using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.World;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		private static void UpdateBrambleErode() {
			var config = CursedBramblesConfig.Instance;

			int attempts = config.Get<int>( nameof( config.BrambleErodeRandomAttemptsPerTickPerSmallWorldArea ) );
			switch( WorldLibraries.GetSize() ) {
			case WorldSize.Medium:
				attempts *= 3;
				break;
			case WorldSize.Large:
				attempts *= 6;
				break;
			case WorldSize.SuperLarge:
				attempts *= 8;
				break;
			}

			for( int i = 0; i < attempts; i++ ) {
				CursedBrambleTile.ErodeRandomBrambleWithinArea(
					minTileX: 1,
					minTileY: 1,
					width: Main.maxTilesX - 2,
					height: Main.maxTilesY - 2,
					adjacentRadius: 8,
					sync: Main.netMode == NetmodeID.Server
				);
			}
		}



		////////////////

		public override void MidUpdateTimeWorld() { // <- use instead of ModWorld.PreUpdate
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				CursedBramblesMod.UpdateBrambleErode();
			}
		}
	}
}