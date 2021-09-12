using System;
using Terraria;
using Terraria.ModLoader;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		private bool DetectIfNearbyWarpBlockingBrambles() {
			var config = CursedBramblesConfig.Instance;
			int rad = config.Get<int>( nameof(config.CursedBrambleWarpItemBlockingTileRange) );
			if( rad < 0 ) {
				return false;
			}

			int x = (int)(this.player.Center.X / 16f);
			int y = (int)(this.player.Center.Y / 16f);
			int minX = Math.Max( x - rad, 1 );
			int minY = Math.Max( y - rad, 1 );
			int maxX = Math.Min( x + rad, Main.maxTilesX - 1 );
			int maxY = Math.Min( y + rad, Main.maxTilesY - 1 );

			int brambleType = ModContent.TileType<CursedBrambleTile>();
			int nearbyBrambles = 0;

			for( int i=minX; i<maxX; i++ ) {
				for( int j=minY; j<maxY; j++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					if( ++nearbyBrambles >= 4 ) {
						return true;
					}
				}
			}

			return false;
		}
	}
}
