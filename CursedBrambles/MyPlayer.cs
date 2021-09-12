using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Buffs;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsPlayerBrambleTrailAPIEnabled { get; private set; } = false;
		
		public bool IsPlayerDefaultBrambleTrailElevationChecked { get; private set; } = false;

		////

		public int BrambleWakeRadius { get; private set; } = 64;

		public int BrambleWakeTickRate { get; private set; } = 15;

		public bool IsNearWarpBlockingBrambles { get; private set; } = false;

		////

		public CursedBramblesAPI.ValidateBrambleCreateAt BrambleCreateValidator { get; private set; } = null;


		////

		public bool IsPlayerProducingBrambleWake {
			get {
				if( this.IsPlayerBrambleTrailAPIEnabled ) {
					return !this.IsPlayerDefaultBrambleTrailElevationChecked
						|| this.CanPlayerDefaultCreateCursedBramblesNearby();
				}

				var config = CursedBramblesConfig.Instance;
				bool plrCanBrambleSetting = config.Get<bool>( nameof(config.PlayersCreateDefaultBrambleTrail) );

				return plrCanBrambleSetting
					&& this.CanPlayerDefaultCreateCursedBramblesNearby();
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

					this.IsNearWarpBlockingBrambles = this.DetectIfNearbyWarpBlockingBrambles();
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
			this.CreateCursedBrambleNearbyIf( null );
		}


		////
		
		public int _FlickerFxTimer = 0;

		public override void PreUpdateBuffs() {
			int theShadowDeBuffType = ModContent.BuffType<TheShadowDeBuff>();

			if( this.IsPlayerProducingBrambleWake ) {
				if( this._FlickerFxTimer-- <= 0 ) {
					this._FlickerFxTimer = 3;
					this.player.AddBuff( theShadowDeBuffType, 2 );
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
