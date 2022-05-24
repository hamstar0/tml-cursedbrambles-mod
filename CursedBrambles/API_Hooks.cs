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

		public static bool IsBrambleAllowedByHooks( int tileX, int tileY ) {
			ISet<ValidateBrambleCreateAt> hooks = ModContent.GetInstance<CursedBramblesAPI>().CanCreateBrambleAtHooks;

			foreach( ValidateBrambleCreateAt hook in hooks ) {
				if( !hook.Invoke(tileX, tileY) ) {
					return false;
				}
			}

			return true;
		}
	}
}
