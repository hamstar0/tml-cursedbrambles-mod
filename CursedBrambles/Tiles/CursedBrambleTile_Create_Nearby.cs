using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		/// <summary></summary>
		/// <param name="worldPos"></param>
		/// <param name="radius"></param>
		/// <param name="sync"></param>
		public static void CreateBrambleNearby( Vector2 worldPos, int radius, bool sync ) {
			int numChecks = radius / 4;

			int tileX = (int)worldPos.X / 16;
			int tileY = (int)worldPos.Y / 16;

			int minX = Math.Max( tileX - radius, 0 );
			int maxX = Math.Min( tileX + radius, Main.maxTilesX - 1 );
			int minY = Math.Max( tileY - radius, 0 );
			int maxY = Math.Min( tileY + radius, Main.maxTilesY - 1 );

			(int x, int y, double weight) point = (Main.rand.Next(minX, maxX), Main.rand.Next(minY, maxY), 0f);

			Tile tile = Framing.GetTileSafely( point.x, point.y );
			if( tile.active() == true ) {
				return;
			}

			point.weight = CursedBrambleTile.GaugeProspectiveBrambleTile( radius, tileX, tileY, point.x, point.y );

			for( int i = 1; i < numChecks; i++ ) {
				(int x, int y) newPoint = (Main.rand.Next(minX, maxX), Main.rand.Next(minY, maxY));

				tile = Framing.GetTileSafely( point.x, point.y );
				if( tile?.active() == true ) {
					continue;
				}

				double weight = CursedBrambleTile.GaugeProspectiveBrambleTile( radius, tileX, tileY, newPoint.x, newPoint.y );
				if( weight > point.weight ) {
					point = (newPoint.x, newPoint.y, weight);
				}
			}

			CursedBrambleTile.CreateBrambleAt( point.x, point.y, sync );
		}


		private static double GaugeProspectiveBrambleTile( int radius, int froTileX, int froTileY, int toTileX, int toTileY ) {
			int diffX = toTileX - froTileX;
			int diffY = toTileY - froTileY;

			double dist = Math.Sqrt( (diffX * diffX) + (diffY * diffY) );
			double firstPartRad = (double)radius * 0.25d;
			double lastPartRad = (double)radius - firstPartRad;
			double lastPartDist = dist - firstPartRad;

			double weight = lastPartDist <= 0d
				? dist / firstPartRad
				: 0d;
			weight += lastPartDist > 0d
				? Math.Max( 1d - (lastPartDist / lastPartRad), 0d )
				: 0d;

			int brambleType = ModContent.TileType<CursedBrambleTile>();
			int neighboringSolids = 0, neighboringBrambles = 0;

			for( int i=toTileX-1; i<toTileX+1; i++ ) {
				for( int j=toTileY-1; j<toTileY+1; j++ ) {
					Tile tile = Framing.GetTileSafely( i, j );
					if( tile.active() ) {
						if( tile.type == brambleType ) {
							neighboringBrambles++;
						} else {
							neighboringSolids++;
						}
					}
				}
			}

			if( neighboringSolids >= 1 ) {
				if( neighboringSolids <= 3 ) {
					if( neighboringBrambles == 0 ) {
						weight += 0.1d; // Edges favored
					}
				} else {
					weight -= 0.1d;	// Unfavor tight spaces
				}
			}

			if( neighboringBrambles >= 1 ) {
				if( neighboringBrambles <= 2 ) {
					weight += 0.2d;	// Good for veins and forks
				} else {
					weight -= 0.2d;	// Not too crowded
				}
			}

			return weight;
		}
	}
}
