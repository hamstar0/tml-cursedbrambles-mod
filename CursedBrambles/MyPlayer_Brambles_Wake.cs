using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.Timers;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		internal void ActivateBrambleWake( bool isElevationChecked, int radius, int tickRate ) {
			this.IsPlayerDefaultBrambleTrailElevationChecked = isElevationChecked;
			this.BrambleWakeRadius = radius;
			this.BrambleWakeTickRate = tickRate;
			this.IsPlayerBrambleTrailAPIEnabled = true;
		}

		internal void DeactivateBrambleWake() {
			this.IsPlayerBrambleTrailAPIEnabled = false;
		}


		////////////////

		 private const string TimerNameBase = "CursedBramblePlayerTrail";

		private bool CanPlayerDefaultCreateCursedBramblesNearby() {
			int tileY = (int)( this.player.position.Y / 16f );

			// Player in range?
			return tileY >= WorldHelpers.DirtLayerTopTileY && tileY < WorldHelpers.UnderworldLayerTopTileY;
		}
		
		private bool CanCreateCursedBramblesThisTick() {
			string timerName = CursedBramblesPlayer.TimerNameBase+"_"+this.player.whoAmI;
			return Timers.GetTimerTickDuration(timerName) == 0;
		}


		////////////////

		private void CreateCursedBrambleNearbyIf() {
			if( !this.IsPlayerProducingBrambleWake ) {
				return;
			}
			if( !this.CanCreateCursedBramblesThisTick() ) {
				return;
			}

			string timerName = CursedBramblesPlayer.TimerNameBase+"_"+this.player.whoAmI;
			Timers.SetTimer( timerName, this.BrambleWakeTickRate, false, () => {
				if( this.OldPosition != default( Vector2 ) ) {
					CursedBrambleTile.CreateBrambleNearby( this.OldPosition, this.BrambleWakeRadius, true );
				}

				this.OldPosition = this.player.Center;
				return false;
			} );
		}
	}
}
