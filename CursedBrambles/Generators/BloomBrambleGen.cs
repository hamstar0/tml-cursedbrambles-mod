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

		protected float Heading = 0f;



		////

		public BloomBrambleGen( BrambleGen parent, int size, int tickRate, int startTileX, int startTileY )
				: base( parent, size ) {
			this.StartTickRate = tickRate;
			this.CurrTileX = startTileX;
			this.CurrTileY = startTileY;
		}


		////////////////

		protected override bool Gen( out int ticksUntilNextGen ) {
			Tile brambleTile = CursedBrambleTile.CreateBrambleAt_If( this.CurrTileX, this.CurrTileY, true );

			if( brambleTile == null ) {
				this.Parent?.SetSize( this.Parent.Size + (int)((float)this.Size * 0.75f) );
			} else {
				this.Size--;

				this.IterateBloom();
			}

			//

			ticksUntilNextGen = this.StartTickRate;
			return true;
		}


		////////////////

		protected void IterateBloom() {
			float growthRange = 2.5f * 16f;

			//

			float rand1 = Main.rand.NextFloat() - 0.5f;

			this.Heading += (float)Math.PI * (rand1 * rand1);
			this.Heading %= (float)Math.PI;
			if( this.Heading < 0f ) {
				this.Heading += (float)Math.PI;
			}

			//

			Vector2 heading = Vector2.One.RotatedBy( this.Heading );

			this.CurrTileX += (int)(heading.X * (16f + (Main.rand.NextFloat() * growthRange)));
			this.CurrTileY += (int)(heading.Y * (16f + (Main.rand.NextFloat() * growthRange)));
		}
	}
}
