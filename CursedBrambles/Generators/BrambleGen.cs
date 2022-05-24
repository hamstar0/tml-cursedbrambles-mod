using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace CursedBrambles.Generators {
	public abstract partial class BrambleGen {
		protected ISet<BrambleGen> SubGens = new HashSet<BrambleGen>();

		private int GenTickRate;
		private int TicksUntilNextGen = 0;



		////////////////
		
		public BrambleGen( int tickRate ) {
			this.GenTickRate = tickRate;
		}


		////////////////

		protected virtual bool UpdateRegenTicks() {
			return this.TicksUntilNextGen-- > 0;
		}

		protected virtual void ResetRegenTicks() {
			this.TicksUntilNextGen = this.GenTickRate;
		}


		////////////////

		public abstract bool CanGen(); //this.SubGens.Count > 0

		////

		protected virtual void Gen() { }    //List<(int tileX, int tileY)> gennedBrambles


		////////////////

		internal void Update() {
			if( !this.UpdateRegenTicks() ) {
				return;
			}

			this.ResetRegenTicks();

			//

			if( this.CanGen() ) {
				this.Gen();
			}

			//

			foreach( BrambleGen subGen in this.SubGens.ToArray() ) {
				if( !this.CanGen() ) {
					break;
				}

				//

				if( subGen.CanGen() ) {
					subGen.Update();
				}

				if( !subGen.CanGen() ) {
					this.SubGens.Remove( subGen );
				}
			}
		}
	}
}
