using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Loadable;
using ModLibsGeneral.Libraries.Tiles;
using CursedBrambles.Tiles;
using CursedBrambles.Generators;


namespace CursedBrambles {
	public partial class CursedBramblesAPI : ILoadable {
		public delegate bool ValidateBrambleCreateAt( int tileX, int tileY );

		public delegate int GetTicks( out int averageTicks );



		////////////////
		
		public static void AddBrambleGenerator( BrambleGen gen ) {
			ModContent.GetInstance<BrambleGenManager>().AddGen( gen );
		}


		////////////////
		
		public static void ClearBramblesWithinArea( Rectangle area, bool syncFromServer ) {
			if( syncFromServer && Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			int brambleType = ModContent.TileType<CursedBrambleTile>();

			for( int i = area.Left; i < area.Right; i++ ) {
				for( int j = area.Top; j < area.Bottom; j++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					TileLibraries.KillTile( i, j, false, false, true, false, syncFromServer );
					//if( syncFromServer ) {
					//} else {
					//	WorldGen.KillTile( i, j );
					//	WorldGen.SquareTileFrame( i, j );
					//}
				}
			}
		}



		////////////////

		private ISet<ValidateBrambleCreateAt> CanCreateBrambleAtHooks = new HashSet<ValidateBrambleCreateAt>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }
	}
}
