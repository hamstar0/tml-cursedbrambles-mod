using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;
using CursedBrambles.Tiles;


namespace CursedBrambles.Generators.Samples {
	public partial class TendrilBrambleGen : BrambleGen {
		public override bool CanGen() {
			if( this.Info.Size <= 0 ) {
				return false;
			}

			//Tile currTile = Main.tile[this.CurrTileX, this.CurrTileY];
			//return currTile?.active() != true;
			return this.IsGrowthUnimpeded;
		}


		////////////////

		protected override void Gen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) {
			int tileX = this.CurrTileX;
			int tileY = this.CurrTileY;

			Tile brambleTile = CursedBrambleTile.CreateBrambleAt_If( tileX, tileY, true );

			if( brambleTile != null ) {
				this.Info.Size--;

				gennedBrambles[ this ] = new HashSet<(int, int)> { (tileX, tileY) };
			}

			//

			this.IsGrowthUnimpeded = this.IterateGrowth_If();
		}
	}
}
