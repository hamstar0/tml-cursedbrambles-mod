using System;
using Terraria;
using Terraria.ModLoader;
using CursedBrambles.Tiles;
using HamstarHelpers.Helpers.World;


namespace CursedBrambles {
	class CursedBramblesWorld : ModWorld {
		public override void PreUpdate() {
			var config = CursedBramblesConfig.Instance;

			int attempts = config.Get<int>( nameof(config.BrambleErodeRandomAttemptsPerTickPerSmallWorldArea) );
			switch( WorldHelpers.GetSize() ) {
			case WorldSize.Medium:
				attempts *= 3;
				break;
			case WorldSize.Large:
				attempts *= 6;
				break;
			case WorldSize.SuperLarge:
				attempts *= 8;
				break;
			}

			for( int i=0; i<attempts; i++ ) {
				CursedBrambleTile.ErodeRandomBrambleWithinArea(
					minTileX: 1,
					minTileY: 1,
					width: Main.maxTilesX-2,
					height: Main.maxTilesY-2,
					adjacentRadius: 8
				);
			}
		}
	}
}
