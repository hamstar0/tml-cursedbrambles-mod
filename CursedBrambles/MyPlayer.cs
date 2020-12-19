using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Buffs;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsDefaultBrambleTrailAPIEnabled { get; private set; } = false;
		
		public bool IsDefaultBrambleTrailElevationChecked { get; private set; } = false;

		////

		public int BrambleWakeRadius { get; private set; } = 64;

		public int BrambleWakeTickRate { get; private set; } = 15;

		public bool IsNearBrambles { get; private set; } = false;

		////

		public bool IsPlayerProducingBrambleWake {
			get {
				if( this.IsDefaultBrambleTrailAPIEnabled ) {
					return true;
				}
				if( !this.IsDefaultBrambleTrailElevationChecked ) {
					return true;
				}

				var config = CursedBramblesConfig.Instance;
				bool plrCanBrambleSetting = config.Get<bool>( nameof(config.PlayersCreateDefaultBrambleTrail ) );

				if( !plrCanBrambleSetting ) {
					return false;
				}

				return this.CanPlayerDefaultCreateCursedBramblesNearby();
			}
		}


		////////////////

		private Vector2 OldPosition = default( Vector2 );

		private int BrambleProximityTimer = 0;



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.PreUpdateHost();
			} else {
				this.PreUpdateLocal();
			}

			if( this.player.whoAmI == Main.myPlayer ) {
				if( this.BrambleProximityTimer-- <= 0 ) {
					this.BrambleProximityTimer = 30;

					this.IsNearBrambles = this.DetectIfNearbyBrambles();
				}
			}
		}

		////

		private void PreUpdateLocal() {
			/*if( this.IsNearBrambles ) {
				this.player.AddBuff( BuffID.ChaosState, 2 );
			}*/
		}

		private void PreUpdateHost() {
			if( this.IsPlayerProducingBrambleWake ) {
				this.CreateCursedBrambleNearbyIf();
			}
		}


		////
		
		public int _FlickerFxTimer = 0;

		public override void PreUpdateBuffs() {
			int theShadowDeBuffType = ModContent.BuffType<TheShadowDeBuff>();

			if( this.IsPlayerProducingBrambleWake ) {
				if( this._FlickerFxTimer-- <= 0 ) {
					this._FlickerFxTimer = 2;
					this.player.AddBuff( theShadowDeBuffType, 1 );
				}
			} else {
				if( this.player.HasBuff(theShadowDeBuffType) ) {
					this.player.ClearBuff( theShadowDeBuffType );
				}
			}
		}


		////

		public override void PreUpdateMovement() {
			if( this.IsPlayerEmbrambled() ) {
				this.ApplyBrambleEffects();
			}
		}
	}
}
