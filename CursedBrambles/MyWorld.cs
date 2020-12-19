﻿using System;
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
				attempts *= 2;
				break;
			case WorldSize.Large:
				attempts *= 4;
				break;
			case WorldSize.SuperLarge:
				attempts *= 5;
				break;
			}

			for( int i=0; i<attempts; i++ ) {
				CursedBrambleTile.ErodeRandomBrambleWithinArea( 1, 1, Main.maxTilesX-2, Main.maxTilesY-2 );
			}
		}
	}
}