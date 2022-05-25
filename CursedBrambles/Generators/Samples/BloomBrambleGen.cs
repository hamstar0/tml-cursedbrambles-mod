using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;


namespace CursedBrambles.Generators.Samples {
	public partial class BloomBrambleGen : BrambleGen {
		public BramblePatchInfo Info { get; }

		protected int AddedTickRateVariation;



		////////////////

		public BloomBrambleGen( int size, int minTickRate, int addedTickRateVariation, int tileX, int tileY )
					: base( minTickRate ) {
			this.Info = new BramblePatchInfo( size );
			this.AddedTickRateVariation = addedTickRateVariation;

			//

			for( int i=0; i<3; i++ ) {
				this.AddTendril( tileX, tileY );
			}
		}


		////////////////
		
		public virtual int GetSize() {
			return this.Info.Size;
		}

		public virtual void AddSize( int size ) {
			this.Info.Size += size;
		}


		////////////////
		
		protected void AddTendril( int tileX, int tileY ) {
			int newTickRate = this.GenTickRate;
			if( this.AddedTickRateVariation > 0 ) {
				newTickRate += Main.rand.Next( 0, this.AddedTickRateVariation + 1 );
			}

			this.SubGens.Add( new TendrilBrambleGen(this.Info, newTickRate, tileX, tileY) );
		}
	}
}
