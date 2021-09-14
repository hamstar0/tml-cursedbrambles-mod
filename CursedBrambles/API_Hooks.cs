using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Loadable;


namespace CursedBrambles {
	public partial class CursedBramblesAPI : ILoadable {
		public static bool AddBrambleAllowHook( ValidateBrambleCreateAt hook ) {
			return ModContent.GetInstance<CursedBramblesAPI>()
				.CanCreateBrambleAtHooks.Add( hook );
		}



		////////////////

		private ISet<ValidateBrambleCreateAt> CanCreateBrambleAtHooks = new HashSet<ValidateBrambleCreateAt>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }
	}
}
