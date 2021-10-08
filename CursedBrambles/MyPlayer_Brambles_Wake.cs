using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModLibsGeneral.Libraries.World;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		private const string TimerNameBase = "CursedBramblePlayerTrail";



		////////////////

		internal void ActivateBrambleWake(
					bool isElevationChecked,
					int radius,
					CursedBramblesAPI.GetTicks tickRate,
					CursedBramblesAPI.ValidateBrambleCreateAt validateAt ) {
			this.IsPlayerDefaultBrambleTrailElevationChecked = isElevationChecked;
			this.BrambleWakeRadius = radius;
			this.BrambleWakeTickRate = tickRate;
			this.IsPlayerBrambleTrailAPIEnabled = true;
			this.BrambleCreateValidator = validateAt;
		}

		internal void DeactivateBrambleWake() {
			this.IsPlayerBrambleTrailAPIEnabled = false;
		}


		////////////////

		private bool CanPlayerDefaultCreateCursedBramblesNearby() {
			int tileY = (int)( this.player.position.Y / 16f );

			// Player in range?
			return tileY >= WorldLocationLibraries.DirtLayerTopTileY && tileY < WorldLocationLibraries.UnderworldLayerTopTileY;
		}
		
		private bool CanCreateCursedBramblesThisTick() {
			string timerName = CursedBramblesPlayer.TimerNameBase+"_"+this.player.whoAmI;
			return Timers.GetTimerTickDuration(timerName) == 0;
		}


		////////////////

		private void CreateCursedBrambleNearbyIf( Func<int, int, bool> validateAt ) {
			if( !this.IsPlayerProducingBrambleWake ) {
				return;
			}
			if( !this.CanCreateCursedBramblesThisTick() ) {
				return;
			}

			string timerName = CursedBramblesPlayer.TimerNameBase+"_"+this.player.whoAmI;

			Timers.SetTimer( timerName, this.BrambleWakeTickRate.Invoke(out _), false, () => {
				if( this.OldPosition != default ) {
					CursedBrambleTile.CreateNearbyIf(
						worldPos: this.OldPosition,
						radius: this.BrambleWakeRadius,
						validateAt: validateAt,
						sync: true
					);
				}

				this.OldPosition = this.player.Center;
				return false;
			} );
		}
	}
}
