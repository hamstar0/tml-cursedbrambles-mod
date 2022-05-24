using CursedBrambles.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CursedBrambles.Generators {
	public class BloomBrambleGen : BrambleGen {
		protected int StartTickRate;

		protected int CurrTileX;
		protected int CurrTileY;



		////

		public BloomBrambleGen( int tickRate, int startTileX, int startTileY ) {
			this.StartTickRate = tickRate;
			this.CurrTileX = startTileX;
			this.CurrTileY = startTileY;
		}


		////

		protected override bool Gen( out int ticksUntilNextGen ) {
			(int x, int y)? newTilePos = CursedBrambleTile.CreateBrambleNearby_If(
				worldPos: new Vector2( this.CurrTileX*16, this.CurrTileY*16 ),
				tileRadius: 2,
				validateAt: null,
				sync: true
			);f

			ticksUntilNextGen = this.StartTickRate;
			return true;
		}
	}
}
