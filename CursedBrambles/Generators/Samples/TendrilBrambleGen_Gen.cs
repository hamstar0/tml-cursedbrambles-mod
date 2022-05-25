using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;
using CursedBrambles.Tiles;


namespace CursedBrambles.Generators.Samples {
	public partial class TendrilBrambleGen : BrambleGen {
		public override bool CanGen() {
			if( this.Parent.GetSize() <= 0 ) {
				return false;
			}

			Tile currTile = Main.tile[this.CurrTileX, this.CurrTileY];

			return currTile?.active() != true;
		}


		////////////////

		protected override void Gen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) {
			int tileX = this.CurrTileX;
			int tileY = this.CurrTileY;

			Tile brambleTile = CursedBrambleTile.CreateBrambleAt_If( tileX, tileY, true );

			if( brambleTile != null ) {
				this.Parent.AddSize( -1 );

				this.IterateGrowth_If();

				//

				gennedBrambles[ this ] = new HashSet<(int, int)> { (tileX, tileY) };
			}
		}
	}
}
