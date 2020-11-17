using System;
using Terraria;


namespace CursedBrambles {
	public static class CursedBramblesAPI {
		public static bool SetPlayerToCreateBrambleWake( Player player, int radius, int tickRate ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.ActivateBrambleWake( radius, tickRate );

			return true;
		}

		public static bool UnsetPlayerToCreateBrambleWake( Player player ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.DeactivateBrambleWake();

			return true;
		}
	}
}
