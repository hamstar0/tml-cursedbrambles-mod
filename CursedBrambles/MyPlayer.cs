using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		private Vector2 OldPosition = default( Vector2 );



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.PreUpdateHost();
			}
		}

		private void PreUpdateHost() {
			var config = CursedBramblesConfig.Instance;
			if( config.Get<bool>( nameof(config.PlayersCreateBrambleTrail) ) ) {
				this.CreateCursedBrambleNearbyIf();
			}
		}
	}
}
