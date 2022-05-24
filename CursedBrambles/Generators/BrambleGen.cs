using System;
using System.Collections.Generic;
using System.Linq;


namespace CursedBrambles.Generators {
	public abstract class BrambleGen {
		public BrambleGen Parent { get; protected set; }

		protected ISet<BrambleGen> Branches { get; } = new HashSet<BrambleGen>();

		public int TicksUntilNextGen { get; private set; } = 0;



		////////////////

		internal bool Update() {
			foreach( BrambleGen branch in this.Branches.ToArray() ) {
				if( !branch.Update() ) {
					this.Branches.Remove( branch );
				}
			}

			//

			if( this.TicksUntilNextGen-- <= 0 ) {
				bool canRegen = this.Gen( out int ticks );
				this.TicksUntilNextGen = ticks;

				return canRegen;
			}

			return true;
		}


		////////////////

		protected abstract bool Gen( out int ticksUntilNextGen );
	}
}
