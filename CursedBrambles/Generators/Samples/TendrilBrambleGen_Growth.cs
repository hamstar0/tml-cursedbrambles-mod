﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;


namespace CursedBrambles.Generators.Samples {
	public partial class TendrilBrambleGen : BrambleGen {
		protected bool IterateGrowth_If() {
			(float newHeading, int newTileX, int newTileY)? newTile;
			newTile = this.FindGrowthTile_If( this.Heading, this.CurrTileX, this.CurrTileY );

			if( newTile.HasValue ) {
				this.Heading = newTile.Value.newHeading;
				this.CurrTileX = newTile.Value.newTileX;
				this.CurrTileY = newTile.Value.newTileY;
			}

			return newTile.HasValue;
		}


		////////////////

		public (float newHeading, int newTileX, int newTileY)? FindGrowthTile_If( float heading, int tileX, int tileY ) {
			int attempts = 20;

			//

			int newTileX, newTileY;
			float newHeading;

			do {
				if( attempts-- < 0 ) {
					return null;
				}

				(newHeading, newTileX, newTileY) = this.GuessGrowthTile( heading, tileX, tileY );
			} while( Main.tile[newTileX, newTileY]?.active() == true );

			//

			return (newHeading, newTileX, newTileY);
		}

		////

		private (float newHeading, int newTileX, int newTileY) GuessGrowthTile( float heading, int currTileX, int currTileY ) {
			float growthRange = 1.5f;

			//

			float rand = Main.rand.NextFloat();
			rand = rand * rand * rand;
			rand *= Main.rand.NextBool() ? 1f : -1f;

			float newHeading = heading + ((float)Math.PI * rand);
			//if( newHeading < 0f ) {
			//	newHeading += (float)Math.PI;
			//} else if( newHeading > (float)Math.PI ) {
			//	newHeading %= (float)Math.PI;
			//}

			//

			float growthDist = 1f + (Main.rand.NextFloat() * growthRange);

			Vector2 vecHeading = Vector2.One.RotatedBy( newHeading );
			vecHeading *= growthDist;

			//

			int newTileX = currTileX + (int)vecHeading.X;
			int newTileY = currTileY + (int)vecHeading.Y;

			if( newTileX <= 0 ) {
				newTileX = 1;
			} else if( newTileX >= Main.maxTilesX ) {
				newTileX = Main.maxTilesX - 1;
			}
			if( newTileY <= 0 ) {
				newTileY = 1;
			} else if( newTileY >= Main.maxTilesY ) {
				newTileY = Main.maxTilesY - 1;
			}

			return (newHeading, newTileX, newTileY);
		}
	}
}
