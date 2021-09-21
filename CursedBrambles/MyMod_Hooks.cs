using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Tiles;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		private static void ApplyBarrierBrambleCollision_WeakRef(
					object rawBarrier,
					int tileX,
					int tileY,
					double damage ) {
			var hitAt = new Vector2( tileX*16, tileY*16 );

			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				var barrier = rawBarrier as SoulBarriers.Barriers.BarrierTypes.Barrier;
				barrier.ApplyMetaphysicalHit( hitAt, damage, true );
			}

			TileLibraries.KillTile( tileX, tileY, false, false, true );
		}


		////////////////

		private static void UpdateForBarriers_WeakRef() {
			var config = CursedBramblesConfig.Instance;
			float barrierDmg = config.Get<float>( nameof(config.DamageToPBGBarriers) );
			if( barrierDmg == 0f ) {
				return;
			}

			int brambleType = ModContent.TileType<CursedBrambleTile>();

			int maxPlr = Main.player.Length;
			for( int i = 0; i < maxPlr; i++ ) {
				Player plr = Main.player[i];
				if( plr?.active != true || plr.dead ) {
					continue;
				}

				SoulBarriers.Barriers.BarrierTypes.Barrier barrier = SoulBarriers.SoulBarriersAPI.GetPlayerBarrier( plr );
				if( barrier == null || !barrier.IsActive ) {
					continue;
				}

				ISet<(int, int)> tiles = SoulBarriers.SoulBarriersAPI.GetTilesUponBarrier( barrier );
				foreach( (int x, int y) in tiles ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					CursedBramblesMod.ApplyBarrierBrambleCollision_WeakRef( barrier, x, y, barrierDmg );

					if( !barrier.IsActive ) {
						break;
					}
				}
			}
		}
	}
}