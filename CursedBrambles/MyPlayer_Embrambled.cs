using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Timers;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsPlayerEmbrambled() {
			int brambleType = ModContent.TileType<CursedBrambleTile>();
			int begX = (int)this.player.position.X;
			int endX = begX + this.player.width;

			for( int i = begX; i < endX; i += 16 ) {
				int begY = (int)this.player.position.Y;
				int endY = begY + this.player.height;

				for( int j = begY; j < endY; j += 16 ) {
					if( Framing.GetTileSafely( i >> 4, j >> 4 ).type == brambleType ) {
						return true;
					}
				}
			}

			return false;
		}


		////////////////

		private void ApplyBrambleEffects() {
			var config = CursedBramblesConfig.Instance;
			string timerName = "CursedBrambleHurt_" + this.player.whoAmI;

			if( this.player.velocity.LengthSquared() > 0.1f ) {
				this.player.velocity *= 1f - config.Get<float>( nameof(config.BrambleStickiness) );
			}
			
			if( Timers.GetTimerTickDuration( timerName ) <= 0 ) {
				Timers.SetTimer( timerName, config.Get<int>( nameof(config.BrambleTicksPerDamage) ), false, () => {
					PlayerHelpers.RawHurt(
						player: this.player,
						deathReason: PlayerDeathReason.ByCustomReason( " was devoured by cursed brambles" ),
						damage: config.Get<int>( nameof(config.BrambleDamage) ),
						direction: 0,
						pvp: false,
						quiet: true,
						crit: false
					);
					return false;
				} );

				this.player.AddBuff( BuffID.Venom, 60 );
			}
		}
	}
}
