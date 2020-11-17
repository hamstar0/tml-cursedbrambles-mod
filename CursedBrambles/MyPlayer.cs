using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsBrambleWakeManuallyEnabled { get; private set; } = false;

		public int BrambleWakeRadius { get; private set; } = 64;

		public int BrambleWakeTickRate { get; private set; } = 15;


		////////////////

		private Vector2 OldPosition = default( Vector2 );



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.PreUpdateHost();
			}
		}

		private void PreUpdateHost() {
			var config = CursedBramblesConfig.Instance;
			if( this.IsBrambleWakeManuallyEnabled || config.Get<bool>( nameof(config.PlayersCreateBrambleTrail) ) ) {
				this.CreateCursedBrambleNearbyIf();
			}
		}
	}
}
