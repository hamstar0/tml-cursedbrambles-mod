using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		/// <summary>
		/// Attempts to locate brambles within a given area.
		/// </summary>
		/// <param name="leftTile"></param>
		/// <param name="topTile"></param>
		/// <param name="rightTile"></param>
		/// <param name="bottomTile"></param>
		/// <returns></returns>
		public static IList<(int tileX, int tileY)> FindBrambles( int leftTile, int topTile, int rightTile, int bottomTile ) {
			var tiles = new List<(int tileX, int tileY)>();
			int brambleType = ModContent.TileType<CursedBrambleTile>();

			for( int i = leftTile; i < rightTile; i++ ) {
				for( int j = topTile; j < bottomTile; j++ ) {
					Tile tile = Framing.GetTileSafely( i, j );
					if( !tile.active() || tile.type != brambleType ) {
						continue;
					}

					tiles.Add( (i, j) );
				}
			}

			return tiles;
		}
	}
}