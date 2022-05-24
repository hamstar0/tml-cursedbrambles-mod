using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;


namespace CursedBrambles.Generators {
	public partial class BloomBrambleGen : BrambleGen {
		protected int Size;



		////////////////

		public BloomBrambleGen( int size, int tickRate, int tileX, int tileY )
					: base( tickRate ) {
			this.Size = size;

			this.SubGens.Add( new TendrilBrambleGen(this, tickRate, tileX, tileY) );
			this.SubGens.Add( new TendrilBrambleGen(this, tickRate, tileX, tileY) );
			this.SubGens.Add( new TendrilBrambleGen(this, tickRate, tileX, tileY) );
		}


		////////////////
		
		public virtual int GetSize() {
			return this.Size;
		}

		public virtual void AddSize( int size ) {
			this.Size += size;
		}

		////////////////

		public override bool CanGen() {
			return this.SubGens.Count > 0;
		}
	}
}
