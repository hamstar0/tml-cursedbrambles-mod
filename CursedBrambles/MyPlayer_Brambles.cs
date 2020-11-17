using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.Timers;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		internal void ActivateBrambleWake( int radius, int tickRate ) {
			this.BrambleWakeRadius = radius;
			this.BrambleWakeTickRate = tickRate;
			this.IsBrambleWakeManuallyEnabled = true;
		}

		internal void DeactivateBrambleWake() {
			this.IsBrambleWakeManuallyEnabled = false;
		}


		////////////////

		private void CreateCursedBrambleNearbyIf() {
			int tileY = (int)( this.player.position.Y / 16f );
			if( tileY < WorldHelpers.DirtLayerTopTileY || tileY >= WorldHelpers.UnderworldLayerTopTileY ) {
				return;
			}

			string timerName = "CursedBramblePlayerTrail_" + this.player.whoAmI;
			if( Timers.GetTimerTickDuration( timerName ) > 0 ) {
				return;
			}

			Timers.SetTimer( timerName, this.BrambleWakeTickRate, false, () => {
				if( this.OldPosition != default( Vector2 ) ) {
					CursedBrambleTile.CreateBrambleNearby( this.OldPosition, this.BrambleWakeRadius );
				}

				this.OldPosition = this.player.Center;
				return false;
			} );
		}
	}
}
