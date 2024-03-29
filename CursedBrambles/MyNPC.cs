﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	class CursedBramblesNPC : GlobalNPC {
		public override void AI( NPC npc ) {
			if( npc.boss ) {
				if( Main.netMode != NetmodeID.MultiplayerClient ) {
					var config = CursedBramblesConfig.Instance;

					if( config.Get<bool>( nameof(config.BossesCreateBrambleTrail) ) ) {
						this.CreateCursedBrambleTrail( npc );
					}
				}
			}
		}


		////

		private void CreateCursedBrambleTrail( NPC npc ) {
			int tileX = (int)npc.Center.X / 16;
			int tileY = (int)npc.Center.Y / 16;
			int thickness = npc.width > npc.height ? npc.width : npc.height;
			thickness = (int)Math.Ceiling( (double)thickness / 16d );

			CursedBrambleTile.CreateBramblePatchAt_If(
				tileX: tileX,
				tileY: tileY,
				radius: thickness,
				densityPercent: 0.05f,
				validateAt: null,
				sync: Main.netMode == NetmodeID.Server
			);
		}
	}
}
