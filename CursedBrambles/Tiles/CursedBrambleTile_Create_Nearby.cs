using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed
	/// by melee weapons (except via. manual pickaxing), and entangles and poisons players. May support additional
	/// custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		public delegate bool CanTileBeBramble( int tileX, int tileY );



		////////////////

		/// <summary></summary>
		/// <param name="worldPos"></param>
		/// <param name="tileRadius"></param>
		/// <param name="sync"></param>
		/// <returns></returns>
		public static (int tileX, int tileY)? CreateBrambleNearby_If(
					Vector2 worldPos,
					int tileRadius,
					CanTileBeBramble validateAt,
					bool sync ) {
			int numChecks = tileRadius / 4;

			int tileX = (int)worldPos.X / 16;
			int tileY = (int)worldPos.Y / 16;

			int minX = Math.Max( tileX - tileRadius, 0 );
			int maxX = Math.Min( tileX + tileRadius, Main.maxTilesX - 1 );
			int minY = Math.Max( tileY - tileRadius, 0 );
			int maxY = Math.Min( tileY + tileRadius, Main.maxTilesY - 1 );

			(int x, int y, double weight) randPoint = default;

			for( int i = 1; i < numChecks; i++ ) {
				(int x, int y) newPoint = ( Main.rand.Next(minX, maxX), Main.rand.Next(minY, maxY) );

				if( !CursedBrambleTile.CanPlaceBrambleAt(newPoint.x, newPoint.y) ) {
					continue;
				}
				if( !validateAt?.Invoke(newPoint.x, newPoint.y) ?? false ) {
					continue;
				}

				//

				double weight = CursedBrambleTile.GaugeProspectiveBrambleTile(
					tileRadius: tileRadius,
					plrTileX: tileX,
					plrTileY: tileY,
					testTileX: newPoint.x,
					testTileY: newPoint.y
				);
				if( weight > randPoint.weight ) {
					randPoint = (newPoint.x, newPoint.y, weight);
				}
			}

			//

			Tile tile = null;

			if( randPoint != default ) {
				tile = CursedBrambleTile.CreateBrambleAt_If( randPoint.x, randPoint.y, sync );
			}

			return tile != null
				? (randPoint.x, randPoint.y)
				: ((int, int)?)null;
		}


		////////////////

		private static double GaugeProspectiveBrambleTile(
					int tileRadius,
					int plrTileX,
					int plrTileY,
					int testTileX,
					int testTileY ) {
			int diffX = testTileX - plrTileX;
			int diffY = testTileY - plrTileY;

			double dist = Math.Sqrt( (diffX * diffX) + (diffY * diffY) );

			double radiusFirstDist = (double)tileRadius * 0.25d;
			double radiusLastDist = (double)tileRadius - radiusFirstDist;
			double distAfterFirstRadius = dist - radiusFirstDist;

			// distance before the radius's 1/4 mark give diminishing returns
			double weight = dist <= radiusFirstDist
				? dist / radiusFirstDist
				: 0d;

			// distance after the radius's 1/4 mark give diminishing returns
			weight += dist > radiusFirstDist
				? Math.Max( 1d - (distAfterFirstRadius / radiusLastDist), 0d )
				: 0d;

			(int solids, int brambles) neighbors = CursedBrambleTile.CountAdjacentBrambles( testTileX, testTileY );

			if( neighbors.solids >= 1 ) {
				if( neighbors.solids <= 3 ) {
					weight += 0.35d; // Edges favored
				} else {
					weight -= 0.35d;	// Unfavor tight spaces
				}
			}

			if( neighbors.brambles >= 1 ) {
				if( neighbors.brambles <= 2 ) {
					weight += 0.5d;	// Good for veins and forks
				} else {
					weight -= 0.5d;	// Not too crowded
				}
			}

			return weight;
		}


		////

		private static (int solids, int brambles) CountAdjacentBrambles( int tileX, int tileY ) {
			int brambleType = ModContent.TileType<CursedBrambleTile>();
			int neighboringSolids = 0, neighboringBrambles = 0;

			for( int i = tileX - 1; i < tileX + 1; i++ ) {
				for( int j = tileY - 1; j < tileY + 1; j++ ) {
					Tile tile = Framing.GetTileSafely( i, j );
					if( !tile.active() ) { continue; }

					if( tile.type == brambleType ) {
						neighboringBrambles++;
					} else {
						neighboringSolids++;
					}
				}
			}

			return ( neighboringSolids, neighboringBrambles );
		}
	}
}
