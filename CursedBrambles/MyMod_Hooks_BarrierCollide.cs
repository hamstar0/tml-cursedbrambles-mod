using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		private static void ApplyBarrierBrambleCollision_SoulBarriers_WeakRef(
					object rawBarrier,
					int tileX,
					int tileY ) {
			var barrier = rawBarrier as SoulBarriers.Barriers.BarrierTypes.Spherical.SphericalBarrier;
			var hitAt = new Vector2( tileX*16, tileY*16 );

			var config = CursedBramblesConfig.Instance;
			double damage = config.Get<float>( nameof(config.DamageToPBGBarriers) );

			barrier.ApplyMetaphysicalHit( hitAt, damage, true );
		}
	}
}