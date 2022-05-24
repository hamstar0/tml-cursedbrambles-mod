using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial class CursedBramblesMod : Mod {
		private static void UpdateBrambleErode() {
			var config = CursedBramblesConfig.Instance;
			var myworld = ModContent.GetInstance<CursedBramblesWorld>();

			//

			float erodePercSec = config.Get<float>( nameof(config.BrambleErodePercentChancePerSecond) );
			float erodePercTick = erodePercSec / 60f;

			int brambleType = ModContent.TileType<CursedBrambleTile>();

			foreach( (int x, int y) in myworld.BramblesSnapshot.ToArray() ) {
				Tile tile = Framing.GetTileSafely( x, y );
				if( tile?.active() != true || tile.type != brambleType ) {
					myworld.BramblesSnapshot.Remove( (x, y) );

					continue;
				}

				if( Main.rand.NextFloat() < erodePercTick ) {
					if( CursedBrambleTile.ErodeBrambleAt(x, y, Main.netMode == NetmodeID.Server) ) {
						myworld.BramblesSnapshot.Remove( (x, y) );
					}
				}
			}
		}
	}
}