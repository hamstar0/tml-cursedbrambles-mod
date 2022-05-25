using System;
using System.Collections.Generic;
using Terraria;


namespace CursedBrambles.Generators {
	public abstract partial class BrambleGen {
		protected ISet<BrambleGen> SubGens = new HashSet<BrambleGen>();

		public int GenTickRate { get; private set; }

		private int TicksUntilNextGen = 0;



		////////////////
		
		public BrambleGen( int tickRate ) {
			this.GenTickRate = tickRate;
		}


		////////////////

		protected virtual bool UpdateRegenTicks() {
			return this.TicksUntilNextGen-- <= 0;
		}

		protected virtual void ResetRegenTicks() {
			this.TicksUntilNextGen = this.GenTickRate;
		}


		////////////////

		public abstract bool CanGen(); //this.SubGens.Count > 0

		////

		protected virtual void Gen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) { }

		protected virtual void PostGen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) { }
	}
}
