﻿using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.Tiles;


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
		/// <returns>`true` if a bramble was found and removed.</returns>
		public static bool ErodeRandomBrambleWithinRadius( int tileX, int tileY, int radius ) {
			int randX = TmlHelpers.SafelyGetRand().Next( radius * 2 );
			int randY = TmlHelpers.SafelyGetRand().Next( radius * 2 );
			int randTileX = tileX + ( randX - radius );
			int randTileY = tileY + ( randY - radius );

			Tile tile = Framing.GetTileSafely( randTileX, randTileY );
			if( !tile.active() || tile.type != ModContent.TileType<CursedBrambleTile>() ) {
				return false;
			}

			TileHelpers.KillTileSynced( randTileX, randTileY, false, false, true );
			return true;
		}

		/// <summary>
		/// Attempts to remove a random bramble within a given (tile) area.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <param name="radius"></param>
		/// <returns>`true` if a bramble was found and removed.</returns>
		public static bool ErodeRandomBrambleWithinArea( int tileX, int tileY, int width, int height ) {
			int randTileX = tileX + TmlHelpers.SafelyGetRand().Next( width );
			int randTileY = tileY + TmlHelpers.SafelyGetRand().Next( height );

			Tile tile = Framing.GetTileSafely( randTileX, randTileY );
			if( !tile.active() || tile.type != ModContent.TileType<CursedBrambleTile>() ) {
				return false;
			}

			TileHelpers.KillTileSynced( randTileX, randTileY, false, false, true );
			return true;
		}
	}
}