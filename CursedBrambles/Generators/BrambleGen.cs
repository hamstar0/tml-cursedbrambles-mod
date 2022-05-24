using System;
using System.Collections.Generic;
using System.Linq;


namespace CursedBrambles.Generators {
	public abstract class BrambleGen {
		public BrambleGen Parent { get; }

		protected ISet<BrambleGen> Branches = new HashSet<BrambleGen>();

		public int TicksUntilNextGen { get; protected set; } = 0;

		public int Size { get; protected set; } = 0;



		////////////////
		
		public BrambleGen( BrambleGen parent, int size ) {
			this.Parent = parent;
			this.Size = size;
		}


		////////////////
		
		public void SetSize( int newSize ) {
			this.Size = newSize;
		}


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
