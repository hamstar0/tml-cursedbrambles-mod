using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		private static bool CheckPlayerBarrierAgainstBramble_SoulBarriers_WeakRef(
					Player player,
					int tileX,
					int tileY,
					out object rawBarrier ) {
			var barrier = SoulBarriers.SoulBarriersAPI.GetPlayerBarrier( player )
				as SoulBarriers.Barriers.BarrierTypes.Spherical.SphericalBarrier;
			rawBarrier = barrier;

			if( barrier == null || barrier.IsActive ) {
				return false;
			}

			Vector2 pos = barrier.GetBarrierWorldCenter();
			float radSqr = barrier.Radius * barrier.Radius;
			float distSqr = (pos - new Vector2(tileX*16, tileY*16)).LengthSquared();

			return distSqr < radSqr;
		}


		////////////////

		private static void LoadBrambleCreateHooks_SoulBarriers_WeakRef() {
			CursedBramblesAPI.AddBrambleAllowHook( (x, y) => {
				var config = CursedBramblesConfig.Instance;
				if( config.Get<float>(nameof(config.DamageToPBGBarriers)) == 0f ) {
					return true;
				}

				int maxPlr = Main.player.Length;
				for( int i=0; i<maxPlr; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true || plr.dead ) {
						continue;
					}

					if( !CursedBramblesMod.CheckPlayerBarrierAgainstBramble_SoulBarriers_WeakRef(plr, x, y, out object barrier) ) {
						CursedBramblesMod.ApplyBarrierBrambleCollision_SoulBarriers_WeakRef( barrier, x, y );

						return false;
					}
				}

				return true;
			} );
		}


		private static void LoadBrambleCreateHooks_WorldGates_WeakRef() {
			/*CursedBramblesAPI.AddCanBrambleCreateHook( ( x, y ) => {
			} );*/
		}



		////////////////

		private void LoadBrambleCreateHooks() {
			if( this.IsSoulBarriersLoaded ) {
				CursedBramblesMod.LoadBrambleCreateHooks_SoulBarriers_WeakRef();
			}
			if( this.IsWorldGatesLoaded ) {
				CursedBramblesMod.LoadBrambleCreateHooks_WorldGates_WeakRef();
			}
		}
	}
}