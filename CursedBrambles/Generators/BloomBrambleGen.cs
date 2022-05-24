using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using CursedBrambles.Tiles;


namespace CursedBrambles.Generators {
	public partial class BloomBrambleGen : BrambleGen {
		protected int StartTickRate;

		protected int CurrTileX;
		protected int CurrTileY;



		////

		public BloomBrambleGen( BrambleGen parent, int size, int tickRate, int startTileX, int startTileY )
				: base( parent, size ) {
			this.StartTickRate = tickRate;
			this.CurrTileX = startTileX;
			this.CurrTileY = startTileY;
			f
		}


		////////////////

		protected override bool Gen( out int ticksUntilNextGen ) {
			ticksUntilNextGen = this.StartTickRate;f
			return true;
		}
	}
}
