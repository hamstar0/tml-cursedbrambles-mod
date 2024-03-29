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
		/// <param name="validateAt"></param>
		/// <param name="sync"></param>
		public static void CreateShuffledBramblePatchesSuccessively(
					ref (int TileX, int TileY)[] tilePositions,
					int radius,
					float densityPercent,
					CursedBramblesAPI.ValidateBrambleCreateAt validateAt,
					bool sync ) {
			UnifiedRandom rand = TmlLibraries.SafelyGetRand();

			// Shuffle positions
			for( int i = tilePositions.Length - 1; i > 0; i-- ) {
				int randPos = rand.Next( i );
				(int, int) tmp = tilePositions[i];

				tilePositions[i] = tilePositions[randPos];
				tilePositions[randPos] = tmp;
			}

			CursedBrambleTile.CreateBramblePatchesSuccessively(
				tilePositions,
				tilePositions.Length - 1,
				radius,
				densityPercent,
				validateAt,
				sync
			);
		}

		////

		private static void CreateBramblePatchesSuccessively(
					(int TileX, int TileY)[] randTilePositions,
					int lastIdx,
					int radius,
					float densityPercent,
					CursedBramblesAPI.ValidateBrambleCreateAt validateAt,
					bool sync ) {
			(int tileX, int tileY) tilePos = randTilePositions[lastIdx];

			int bramblesPlaced = CursedBrambleTile.CreateBramblePatchAt_If(
				tilePos.tileX,
				tilePos.tileY,
				radius,
				densityPercent,
				validateAt,
				sync
			);

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
					CursedBrambleTile.CreateBramblePatchesSuccessively(
						randTilePositions,
						lastIdx,
						radius,
						densityPercent,
						validateAt,
						sync
					);
					return false;
				} );
			}
		}


		////////////////

		/// <summary>
		/// Creates a patch of cursed brambles at the given location with the given settings.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <param name="radius"></param>
		/// <param name="densityPercent"></param>
		/// <param name="validateAt"></param>
		/// <param name="sync"></param>
		/// <returns></returns>
		public static int CreateBramblePatchAt_If(
					int tileX,
					int tileY,
					int radius,
					float densityPercent,
					CursedBramblesAPI.ValidateBrambleCreateAt validateAt,
					bool sync ) {
			int brambleTileType = ModContent.TileType<CursedBrambleTile>();
			var rand = TmlLibraries.SafelyGetRand();

			//Tile tileAt = Main.tile[tileX, tileY];
			//if( tileAt != null && tileAt.active() && tileAt.type == brambleTileType ) {
			//	return 0;
			//}

			//

			int bramblesPlaced = 0;

			int max = radius / 2;
			int min = -max;
			for( int i = min; i < max; i++ ) {
				for( int j = min; j < max; j++ ) {
					if( (1f - rand.NextFloat()) > densityPercent ) {
						continue;
					}

					int newX = tileX + i;
					int newY = tileY + j;

					if( !CursedBrambleTile.CanPlaceBrambleAt(newX, newY) ) {
						continue;
					}
					if( !(validateAt?.Invoke(newX, newY) ?? true) ) {
						continue;
					}

					//

					Tile tile = CursedBrambleTile.CreateBrambleAt_If( newX, newY, sync );
					if( tile != null ) {
						bramblesPlaced++;
					}
				}
			}

			return bramblesPlaced;
		}
	}
}