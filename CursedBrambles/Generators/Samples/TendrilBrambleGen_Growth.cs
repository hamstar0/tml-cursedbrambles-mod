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
			int attempts = 10;

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
			float growthRange = 2.5f * 16f;

			//

			float rand = Main.rand.NextFloat();
			rand *= rand;
			rand *= Main.rand.NextBool() ? 1f : -1f;

			heading += (float)Math.PI * rand;
			heading %= (float)Math.PI;
			if( heading < 0f ) {
				heading += (float)Math.PI;
			}

			Vector2 vecHeading = Vector2.One.RotatedBy( heading );

			//
			
			currTileX += (int)( vecHeading.X * (16f + (Main.rand.NextFloat() * growthRange)) );
			currTileY += (int)( vecHeading.Y * (16f + (Main.rand.NextFloat() * growthRange)) );

			if( currTileX <= 0 ) {
				currTileX = 1;
			} else if( currTileX >= Main.maxTilesX ) {
				currTileX = Main.maxTilesX - 1;
			}
			if( currTileY <= 0 ) {
				currTileY = 1;
			} else if( currTileY >= Main.maxTilesY ) {
				currTileY = Main.maxTilesY - 1;
			}

			return (heading, currTileX, currTileY);
		}
	}
}