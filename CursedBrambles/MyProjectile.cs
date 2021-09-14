using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CursedBrambles {
	partial class CursedBramblesProjectile : GlobalProjectile {
		public override bool PreAI( Projectile projectile ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return base.PreAI( projectile );
			}

			if( projectile.type == ProjectileID.PurificationPowder ) {
				var area = new Rectangle(
					(int)Math.Ceiling(projectile.position.X) / 16,
					(int)Math.Ceiling(projectile.position.Y) / 16,
					(int)Math.Floor( (float)projectile.width / 16f ),
					(int)Math.Floor( (float)projectile.width / 16f )
				);

				CursedBramblesAPI.ClearBramblesWithinArea( area, Main.netMode == NetmodeID.Server );
			}

			return base.PreAI( projectile );
		}
	}
}
