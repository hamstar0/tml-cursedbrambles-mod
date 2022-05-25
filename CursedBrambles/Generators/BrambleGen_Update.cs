using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace CursedBrambles.Generators {
	public abstract partial class BrambleGen {
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
