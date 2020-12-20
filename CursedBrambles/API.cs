using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace CursedBrambles {
	public static class CursedBramblesAPI {
		public static bool GetPlayerBrambleWakeStatus(
					Player player,
					out bool manuallyActivated,
					out bool isElevationConsidered,
					out int radius,
					out int tickRate ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();

			manuallyActivated = myplayer.IsPlayerBrambleTrailAPIEnabled;
			isElevationConsidered = myplayer.IsPlayerDefaultBrambleTrailElevationChecked;
			radius = myplayer.BrambleWakeRadius;
			tickRate = myplayer.BrambleWakeTickRate;
			return myplayer.IsPlayerProducingBrambleWake;
		}


		////

		public static bool SetPlayerToCreateBrambleWake( Player player, bool isElevationChecked, int radius, int tickRate ) {
			if( CursedBramblesConfig.Instance.DebugModeInfo ) {
				IList<string> ctx = DebugHelpers.GetContextSlice();
				LogHelpers.Log( "SetPlayerToCreateBrambleWake called from: "+string.Join("\n  ", ctx) );
			}

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
