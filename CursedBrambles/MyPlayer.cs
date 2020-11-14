using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.World;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	class CursedBramblesPlayer : ModPlayer {
		private Vector2 OldPosition = default( Vector2 );



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.PreUpdateHost();
			}
		}

		private void PreUpdateHost() {
			var config = CursedBramblesConfig.Instance;
			if( config.Get<bool>( nameof( config.PlayersCreateBrambleTrail ) ) ) {
				this.UpdatePlayerBrambleTrail();
			}
		}

		////

		private void UpdatePlayerBrambleTrail() {
			if( (int)(this.player.position.Y / 16f) < WorldHelpers.UnderworldLayerTopTileY ) {
				return;
			}

			string timerName = "CursedBramblePlayerTrail_" + this.player.whoAmI;
			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return;
			}

			Timers.SetTimer( timerName, 60 * 5, false, () => {
				if( this.OldPosition != default( Vector2 ) ) {
					this.CreateCursedBrambleTrail( this.OldPosition );
				}

				this.OldPosition = this.player.Center;
				return false;
			} );
		}


		////

		private void CreateCursedBrambleTrail( Vector2 pos ) {
			Player plr = this.player;
			int tileX = (int)pos.X / 16;
			int tileY = (int)pos.Y / 16;

			CursedBrambleTile.CreateBramblePatchAt( tileX, tileY, 16, 0.05f );
		}
	}
}
