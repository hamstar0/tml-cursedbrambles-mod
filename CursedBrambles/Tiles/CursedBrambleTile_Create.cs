using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		public static bool CanPlaceBrambleAt( int tileX, int tileY ) {
			Tile tileAt = Main.tile[tileX, tileY];
			if( tileAt == null || tileAt.active() ) {
				return false;
			}

			if( tileAt.liquid != 0 && (tileAt.honey() || tileAt.lava()) ) {
				return false;
			}

			return CursedBramblesAPI.IsBrambleAllowedByHooks( tileX, tileY );
		}


		////////////////

		/// <summary>
		/// Creates a specific bramble tile. Preserves existing wire, wall, and liquid.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <returns>`null` if bramble is blocked.</returns>
		public static Tile CreateBrambleAtIf( int tileX, int tileY, bool sync ) {
			int brambleTileType = ModContent.TileType<CursedBrambleTile>();
			if( !WorldGen.PlaceTile( tileX, tileY, brambleTileType ) ) {
				return null;
			}

			Tile tile = Framing.GetTileSafely( tileX, tileY );
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