using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Loadable;


namespace CursedBrambles {
	public partial class CursedBramblesAPI : ILoadable {
		public static ValidateBrambleCreateAt CreatePlayerAvoidingBrambleValidator( int tileRadius ) {
			return ( x, y ) => {
				var wldPos = new Vector2( x * 16, y * 16 );
				int plrMax = Main.player.Length;
				float maxLenSqr = tileRadius * 16;
				maxLenSqr *= maxLenSqr;

				for( int i=0; i<plrMax; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true || plr.dead ) {
						continue;
					}

					if( (plr.MountedCenter - wldPos).LengthSquared() < maxLenSqr ) {
						return false;
					}
				}

				return true;
			};
		}



		////////////////

		public static bool GetPlayerBrambleWakeStatus(
					Player player,
					out bool manuallyActivated,
					out bool isElevationConsidered,
					out int radius,
					out int tickRate,
					out ValidateBrambleCreateAt validateAt ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();

			manuallyActivated = myplayer.IsPlayerBrambleTrailAPIEnabled;
			isElevationConsidered = myplayer.IsPlayerDefaultBrambleTrailElevationChecked;
			radius = myplayer.BrambleWakeTileRadius;
			tickRate = myplayer.BrambleWakeTickRate.Invoke( out _ );
			validateAt = myplayer.BrambleCreateValidator;
			return myplayer.IsPlayerProducingBrambleWake;
		}


		////

		public static bool SetPlayerToCreateBrambleWake(
					Player player,
					bool isElevationChecked,
					int tileRadius,
					int tickRate,
					ValidateBrambleCreateAt validateAt ) {
			int GetTickRate( out int blah ) {
				blah = tickRate;
				return tickRate;
			}

			//

			return CursedBramblesAPI.SetPlayerToCreateBrambleWake(
				player: player,
				isElevationChecked: isElevationChecked,
				tileRadius: tileRadius,
				tickRate: GetTickRate,
				validateAt: validateAt
			);
		}

		public static bool SetPlayerToCreateBrambleWake(
					Player player,
					bool isElevationChecked,
					int tileRadius,
					GetTicks tickRate,
					ValidateBrambleCreateAt validateAt ) {
			if( CursedBramblesConfig.Instance.DebugModeInfo ) {
				IList<string> ctx = DebugLibraries.GetContextSlice();
				LogLibraries.Log( "SetPlayerToCreateBrambleWake called from: "+string.Join("\n  ", ctx) );
			}

			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();

			myplayer.ActivateBrambleWake( isElevationChecked, tileRadius, tickRate, validateAt );

			return true;
		}

		public static bool UnsetPlayerBrambleWakeCreating( Player player ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.DeactivateBrambleWake();

			return true;
		}
	}
}
