﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		/// <summary>
		/// Creates a cluster of brambles randomly from a given set of points in succession.
		/// </summary>
		/// <param name="tilePositions">Center positions of bramble patches. Array will be shuffled.</param>
		/// <param name="radius"></param>
		/// <param name="densityPercent"></param>
		/// <param name="sync"></param>
		public static void CreateShuffledBramblePatchesSuccessively(
					ref (int TileX, int TileY)[] tilePositions,
					int radius,
					float densityPercent,
					bool sync ) {
			UnifiedRandom rand = TmlLibraries.SafelyGetRand();

			// Shuffle positions
			for( int i = tilePositions.Length - 1; i > 0; i-- ) {
				int randPos = rand.Next( i );
				(int, int) tmp = tilePositions[i];

				tilePositions[i] = tilePositions[randPos];
				tilePositions[randPos] = tmp;
			}

			CursedBrambleTile.CreateBramblePatchesSuccessively( tilePositions, tilePositions.Length - 1, radius, densityPercent, sync );
		}

		////

		private static void CreateBramblePatchesSuccessively(
					(int TileX, int TileY)[] randTilePositions,
					int lastIdx,
					int radius,
					float densityPercent,
					bool sync ) {
			(int tileX, int tileY) tilePos = randTilePositions[lastIdx];

			int bramblesPlaced = CursedBrambleTile.CreateBramblePatchAt( tilePos.tileX, tilePos.tileY, radius, densityPercent, sync );

			/*if( ModHelpersConfig.Instance.DebugModeMiscInfo ) {
				LogLibraries.Log(
					"Created " + bramblesPlaced
					+ " brambles in patch " + lastIdx
					+ " of " + randTilePositions.Length
					+ " (thickness: " + thickness + ", density: " + density + ")" );
			}*/

			if( lastIdx > 0 ) {
				lastIdx--;

				string timerName = "CursedBramblesPathAsync_" + tilePos.tileX + "_" + tilePos.tileY;
				Timers.SetTimer( timerName, 2, false, () => {
					CursedBrambleTile.CreateBramblePatchesSuccessively( randTilePositions, lastIdx, radius, densityPercent, sync );
					return false;
				} );
			}
		}


		////

		/// <summary>
		/// Creates a patch of cursed brambles at the given location with the given settings.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <param name="radius"></param>
		/// <param name="densityPercent"></param>
		/// <param name="sync"></param>
		/// <returns></returns>
		public static int CreateBramblePatchAt( int tileX, int tileY, int radius, float densityPercent, bool sync ) {
			int brambleTileType = ModContent.TileType<CursedBrambleTile>();
			var rand = TmlLibraries.SafelyGetRand();

			Tile tileAt = Main.tile[tileX, tileY];
			if( tileAt != null && tileAt.active() && tileAt.type == brambleTileType ) {
				return 0;
			}

			int bramblesPlaced = 0;

			int max = radius / 2;
			int min = -max;
			for( int i = min; i < max; i++ ) {
				for( int j = min; j < max; j++ ) {
					if( ( 1f - rand.NextFloat() ) > densityPercent ) {
						continue;
					}

					Tile tile = CursedBrambleTile.CreateBrambleAt( tileX + i, tileY + j, sync );
					if( tile != null ) {
						bramblesPlaced++;
					}
				}
			}

			return bramblesPlaced;
		}


		/// <summary>
		/// Creates a specific bramble tile. Preserves existing wire, wall, and liquid.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <returns>`null` if bramble is blocked.</returns>
		public static Tile CreateBrambleAt( int tileX, int tileY, bool sync ) {
			int brambleTileType = ModContent.TileType<CursedBrambleTile>();

			Tile tileAt = Main.tile[tileX, tileY];
			if( tileAt != null && tileAt.active() && tileAt.type == brambleTileType ) {
				return null;
			}

			Tile tile = Framing.GetTileSafely( tileX, tileY );
			if( tile.active() || (tile.liquid != 0 && (tile.honey() || tile.lava())) ) {
				return null;
			}

			if( !WorldGen.PlaceTile( tileX, tileY, brambleTileType ) ) {
				return null;
			}

			Tile newTile = Main.tile[tileX, tileY];
			newTile.wall = tile.wall;
			newTile.wallFrameNumber( tile.wallFrameNumber() );
			newTile.wallFrameX( tile.wallFrameX() );
			newTile.wallFrameY( tile.wallFrameY() );
			newTile.wallColor( tile.wallColor() );
			newTile.wire( tile.wire() );
			newTile.wire2( tile.wire2() );
			newTile.wire3( tile.wire3() );
			newTile.wire4( tile.wire4() );
			newTile.liquid = tile.liquid;
			newTile.liquidType( tile.liquidType() );

			if( sync && Main.netMode != NetmodeID.SinglePlayer ) {
				NetMessage.SendData( MessageID.TileChange, -1, -1, null, 1, (float)tileX, (float)tileY, (float)brambleTileType, 0, 0, 0 );
			}

			return newTile;
		}
	}
}