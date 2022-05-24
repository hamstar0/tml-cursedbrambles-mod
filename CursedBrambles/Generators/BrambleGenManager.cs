using System;
using System.Collections.Generic;
using System.Linq;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Services.Hooks.LoadHooks;


namespace CursedBrambles.Generators {
	class BrambleGenManager : ILoadable {
		private ISet<BrambleGen> ActiveGens = new HashSet<BrambleGen>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			LoadHooks.AddWorldUnloadEachHook( () => {
				this.ActiveGens.Clear();
			} );
		}


		////////////////
		
		public void AddGen( BrambleGen gen ) {
			this.ActiveGens.Add( gen );
		}


		////////////////

		public void Update() {
			foreach( BrambleGen gen in this.ActiveGens.ToArray() ) {
				if( gen.CanGen() ) {
					gen.Update();
				}

				if( !gen.CanGen() ) {
					this.ActiveGens.Remove( gen );
				}
			}
		}
	}
}
