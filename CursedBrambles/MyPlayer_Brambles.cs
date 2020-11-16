using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.Timers;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		private void CreateCursedBrambleNearbyIf() {
			if( (int)( this.player.position.Y / 16f ) < WorldHelpers.UnderworldLayerTopTileY ) {
				return;
			}

			string timerName = "CursedBramblePlayerTrail_" + this.player.whoAmI;
			if( Timers.GetTimerTickDuration( timerName ) > 0 ) {
				return;
			}

			Timers.SetTimer( timerName, 15, false, () => {
				if( this.OldPosition != default( Vector2 ) ) {
					CursedBrambleTile.CreateBrambleNearby( this.OldPosition, 64 );
				}

				this.OldPosition = this.player.Center;
				return false;
			} );
		}
	}
}
