using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;
using CursedBrambles.Tiles;


namespace CursedBrambles.Generators {
	public partial class TendrilBrambleGen : BrambleGen {
		public BloomBrambleGen Parent { get; }

		protected int CurrTileX;
		protected int CurrTileY;

		protected float Heading = 0f;



		////////////////

		public TendrilBrambleGen( BloomBrambleGen parent, int tickRate, int tileX, int tileY )
					: base( tickRate ) {
			this.Parent = parent;
			this.CurrTileX = tileX;
			this.CurrTileY = tileY;

			//

			this.Heading = (float)Math.PI * Main.rand.NextFloat();
		}

		////////////////

		public override bool CanGen() {
			return this.Parent.GetSize() > 0;
		}


		////////////////

		protected override void Gen() {
			int tileX = this.CurrTileX;
			int tileY = this.CurrTileY;

			Tile brambleTile = CursedBrambleTile.CreateBrambleAt_If( tileX, tileY, true );

			if( brambleTile != null ) {
				this.Parent.AddSize( -1 );

				this.IterateGrowth();
			}
		}


		////////////////
		
		protected void IterateGrowth() {
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
