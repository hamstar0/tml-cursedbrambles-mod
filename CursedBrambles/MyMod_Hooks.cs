using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Tiles;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		private static void UpdateBarrierCollisionsIf_SoulBarriers_WeakRef() {
			var config = CursedBramblesConfig.Instance;
			float barrierDmg = config.Get<float>( nameof(config.DamageToPBGBarriers) );

			if( barrierDmg == 0f ) {
				return;
			}

			if( Main.netMode == NetmodeID.MultiplayerClient ) { // Let the server handle other players
				CursedBramblesMod.UpdatePlayerBarrierCollisionsIf_SoulBarriers_WeakRef( Main.LocalPlayer, barrierDmg );
			} else {
				int maxPlr = Main.player.Length;
				for( int i = 0; i < maxPlr; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active == true ) {
						CursedBramblesMod.UpdatePlayerBarrierCollisionsIf_SoulBarriers_WeakRef( plr, barrierDmg );
					}
				}
			}
		}


		private static void UpdatePlayerBarrierCollisionsIf_SoulBarriers_WeakRef( Player plr, float barrierDmg ) {
			SoulBarriers.Barriers.BarrierTypes.Barrier barrier = SoulBarriers.SoulBarriersAPI.GetPlayerBarrier( plr );
			if( barrier == null || !barrier.IsActive ) {
				return;
			}

			int brambleType = ModContent.TileType<CursedBrambleTile>();
			ISet<(int, int)> tiles = SoulBarriers.SoulBarriersAPI.GetTilesUponBarrier( barrier, 8f );

			foreach( (int x, int y) in tiles ) {
				Tile tile = Main.tile[x, y];
				if( tile?.active() != true || tile.type != brambleType ) {
					continue;
				}

				if( !CursedBramblesMod.ApplyBarrierCollision_SoulBarriers_WeakRef(barrier, x, y, barrierDmg) ) {
					break;
				}
			}
		}


		////////////////

		private static bool ApplyBarrierCollision_SoulBarriers_WeakRef(
					object rawBarrier,
					int tileX,
					int tileY,
					double damage ) {
			var wldHitAt = new Vector2( tileX * 16, tileY * 16 );
			var barrier = rawBarrier as SoulBarriers.Barriers.BarrierTypes.Barrier;

			if( !barrier.IsActive ) {
				return false;
			}

			barrier.ApplyMetaphysicalHit( wldHitAt, damage, true );

			TileLibraries.KillTile( tileX, tileY, false, false, true );

			return true;
		}
	}
}