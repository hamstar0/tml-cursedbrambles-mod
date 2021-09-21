using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Libraries.Tiles;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		/// <summary>
		/// Attempts to remove a random bramble within a given (tile) radius of a given tile.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <param name="radius"></param>
		/// <param name="adjacentRadius"></param>
		/// <returns>`true` if a bramble was found and removed.</returns>
		public static bool ErodeRandomBrambleWithinRadius( int tileX, int tileY, int radius, int adjacentRadius ) {
			int randX = TmlLibraries.SafelyGetRand().Next( radius * 2 );
			int randY = TmlLibraries.SafelyGetRand().Next( radius * 2 );
			int randTileX = tileX + ( randX - radius );
			int randTileY = tileY + ( randY - radius );

			Tile tile = Framing.GetTileSafely( randTileX, randTileY );
			if( !tile.active() || tile.type != ModContent.TileType<CursedBrambleTile>() ) {
				return false;
			}

			TileLibraries.KillTile( randTileX, randTileY, false, false, true );

			CursedBrambleTile.ErodeBramblesWithinAreaRadiusRandomly( tileX, tileY, adjacentRadius );

			return true;
		}

		/// <summary>
		/// Attempts to remove a random bramble within a given (tile) area.
		/// </summary>
		/// <param name="minTileX"></param>
		/// <param name="minTileY"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="adjacentRadius"></param>
		/// <param name="sync"></param>
		/// <returns>`true` if a bramble was found and removed.</returns>
		public static bool ErodeRandomBrambleWithinArea(
					int minTileX,
					int minTileY,
					int width,
					int height,
					int adjacentRadius,
					bool sync ) {
			int randTileX = minTileX + TmlLibraries.SafelyGetRand().Next( width );
			int randTileY = minTileY + TmlLibraries.SafelyGetRand().Next( height );

			Tile tile = Framing.GetTileSafely( randTileX, randTileY );
			if( !tile.active() || tile.type != ModContent.TileType<CursedBrambleTile>() ) {
				return false;
			}

			if( !sync ) {
				WorldGen.KillTile(
					i: randTileX,
					j: randTileY,
					fail: false,
					effectOnly: false,
					noItem: true
				);
				WorldGen.SquareTileFrame( randTileX, randTileY );
			} else {
				TileLibraries.KillTile(
					tileX: randTileX,
					tileY: randTileY,
					effectOnly: false,
					dropsItem: false,
					forceSyncIfUnchanged: true
				);
			}

			CursedBrambleTile.ErodeBramblesWithinAreaRadiusRandomly( minTileX, minTileY, adjacentRadius );

			return true;
		}


		////

		private static void ErodeBramblesWithinAreaRadiusRandomly( int tileX, int tileY, int adjacentRadius ) {
			int minX = tileX - (adjacentRadius / 2);
			int maxX = tileX + (adjacentRadius / 2);
			int minY = tileY - (adjacentRadius / 2);
			int maxY = tileY + (adjacentRadius / 2);
			int brambleType = ModContent.TileType<CursedBrambleTile>();

			for( int i=minX; i<maxX; i++ ) {
				for( int j=minY; j<maxY; j++ ) {
					if( !WorldGen.InWorld(i, j) ) {
						continue;
					}

					Tile tile = Framing.GetTileSafely( i, j );
					if( !tile.active() || tile.type != brambleType ) {
						continue;
					}

					CursedBrambleTile.ErodeBramblesAtRandomly( i, j );
				}
			}
		}

		private static void ErodeBramblesAtRandomly( int tileX, int tileY ) {
			int minTicks = 3;
			int maxTicks = 60 * 60;
			int brambleType = ModContent.TileType<CursedBrambleTile>();

			Timers.SetTimer( TmlLibraries.SafelyGetRand().Next(minTicks, maxTicks), false, () => {
				Tile tile = Framing.GetTileSafely( tileX, tileY );

				if( tile.active() && tile.type == brambleType ) {
					TileLibraries.KillTile( tileX, tileY, false, false, true );
				}
				return false;
			} );
		}
	}
}