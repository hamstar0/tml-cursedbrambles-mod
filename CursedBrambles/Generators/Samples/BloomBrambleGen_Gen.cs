using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace CursedBrambles.Generators.Samples {
	public partial class BloomBrambleGen : BrambleGen {
		public override bool CanGen() {
			return this.SubGens.Count > 0;
		}


		////////////////
		
		protected override void PostGen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) {
			foreach( (BrambleGen srcGen, HashSet<(int, int)> brambles) in gennedBrambles ) {
				var srcTendrilGen = srcGen as TendrilBrambleGen;
				if( srcTendrilGen == null ) {
					continue;
				}

				foreach( (int x, int y) in brambles ) {
					if( this.IsTooOpen(x, y, gennedBrambles.Keys) ) {
						(float _, int newX, int newY)? newTile;
						newTile = srcTendrilGen.FindGrowthTile_If( srcTendrilGen.Heading, x, y );

						if( newTile.HasValue ) {
							continue;
						}

						//

						var gen = new TendrilBrambleGen( this.Info, this.GenTickRate, newTile.Value.newX, newTile.Value.newY );

						this.SubGens.Add( gen );
					}
				}
			}
		}


		////////////////

		private bool IsTooOpen( int tileX, int tileY, IEnumerable<BrambleGen> existingGens ) {
			int scanRadius = 4;
			int nearbySolidsMax = 8;

			//

			int minX = Math.Max( tileX - scanRadius, 1 );
			int maxX = Math.Max( tileX + scanRadius, Main.maxTilesX - 1 );
			int minY = Math.Max( tileY - scanRadius, 1 );
			int maxY = Math.Max( tileY + scanRadius, Main.maxTilesY - 1 );

			int solidCount = 0;

			for( int x=minX; x<maxX; x++ ) {
				for( int y=minY; y<maxY; y++ ) {
					if( Main.tile[x, y]?.active() == true ) {
						solidCount++;
					}
				}
			}

			//

			return solidCount < nearbySolidsMax;
		}
	}
}
