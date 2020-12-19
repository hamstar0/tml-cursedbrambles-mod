using System;
using Terraria;


namespace CursedBrambles {
	public static class CursedBramblesAPI {
		public static bool SetPlayerToCreateBrambleWake( Player player, bool isElevationChecked, int radius, int tickRate ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.ActivateBrambleWake( isElevationChecked, radius, tickRate );

			return true;
		}

		public static bool UnsetPlayerBrambleWakeCreating( Player player ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.DeactivateBrambleWake();

			return true;
		}
	}
}
