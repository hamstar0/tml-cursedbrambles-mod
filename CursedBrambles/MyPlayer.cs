using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Buffs;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsBrambleWakeManuallyEnabled { get; private set; } = false;

		public int BrambleWakeRadius { get; private set; } = 64;

		public int BrambleWakeTickRate { get; private set; } = 15;

		public bool IsNearBrambles { get; private set; } = false;

		////

		public bool IsPlayerProducingBrambleWake {
			get {
				var config = CursedBramblesConfig.Instance;
				if( !this.IsBrambleWakeManuallyEnabled && !config.Get<bool>(nameof(config.PlayersCreateBrambleTrail)) ) {
					return false;
				}
				return this.CanCreateCursedBramblesNearby(out bool _);
			}
		}


		////////////////

		private Vector2 OldPosition = default( Vector2 );

		private int BrambleProximityTimer = 0;



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.PreUpdateHost();
			}

			if( this.player.whoAmI == Main.myPlayer ) {
				if( this.BrambleProximityTimer-- <= 0 ) {
					this.BrambleProximityTimer = 30;

					this.IsNearBrambles = this.DetectIfNearbyBrambles();
				}
			}
		}

		private void PreUpdateHost() {
			if( this.IsPlayerProducingBrambleWake ) {
				this.CreateCursedBrambleNearbyIf();
			}
		}


		public override void PreUpdateBuffs() {
			int theShadowDeBuffType = ModContent.BuffType<TheShadowDeBuff>();

			if( !this.player.HasBuff(theShadowDeBuffType) ) {
				if( this.IsPlayerProducingBrambleWake ) {
					this.player.AddBuff( theShadowDeBuffType, 2 );
				}
			} else {
				if( this.IsPlayerProducingBrambleWake ) {
					this.player.ClearBuff( theShadowDeBuffType );
				}
			}
		}
	}
}
