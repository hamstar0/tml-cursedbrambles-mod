using System;
using System.Collections.Generic;
using System.Linq;
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
			return this.TicksUntilNextGen-- > 0;
		}

		protected virtual void ResetRegenTicks() {
			this.TicksUntilNextGen = this.GenTickRate;
		}


		////////////////

		public abstract bool CanGen(); //this.SubGens.Count > 0

		////

		protected virtual void Gen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) { }

		protected virtual void PostGen( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) { }


		////////////////

		internal void Update() {
			var gennedBrambles = new Dictionary<BrambleGen, HashSet<(int tileX, int tileY)>>();

			this.Update_Protected( gennedBrambles );
		}

		protected void Update_Protected( IDictionary<BrambleGen, HashSet<(int tileX, int tileY)>> gennedBrambles ) {
			if( !this.UpdateRegenTicks() ) {
				return;
			}

			this.ResetRegenTicks();

			//

			if( this.CanGen() ) {
				this.Gen( gennedBrambles );
			}

			//

			foreach( BrambleGen subGen in this.SubGens.ToArray() ) {
				if( !this.CanGen() ) {
					break;
				}

				//

				if( subGen.CanGen() ) {
					subGen.Update_Protected( gennedBrambles );
				}

				if( !subGen.CanGen() ) {
					this.SubGens.Remove( subGen );
				}
			}

			//

			if( gennedBrambles.Count > 0 ) {
				this.PostGen( gennedBrambles );
			}
		}
	}
}
