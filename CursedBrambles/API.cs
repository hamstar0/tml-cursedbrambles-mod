using System;
using Microsoft.Xna.Framework;
using Terraria;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public static class CursedBramblesAPI {
		public static bool SetPlayerToCreateBrambleWake( Vector2 position, int radius ) {
			CursedBrambleTile.CreateBrambleNearby( position, radius );

			return true;
		}
	}
}
