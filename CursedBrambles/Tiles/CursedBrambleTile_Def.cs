using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace CursedBrambles.Tiles {
	/// <summary>
	/// Represents a tile that works similar to a standard corruption/crimson/jungle bramble, but cannot be removed by melee
	/// weapons (except via. manual pickaxing), and entangles and poisons players. May support additional custom behavior.
	/// </summary>
	public partial class CursedBrambleTile : ModTile {
		/// @private
		public override void SetDefaults() {
			//Main.tileSolid[this.Type] = true;
			//Main.tileMergeDirt[this.Type] = true;
			//Main.tileBlockLight[this.Type] = true;
			Main.tileLighted[this.Type] = true;
			Main.tileNoAttach[this.Type] = true;
			Main.tileLavaDeath[this.Type] = true;
			this.dustType = DustID.Granite;
			this.AddMapEntry( new Color(128, 64, 128) );
		}

		/// @private
		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}


		public override void DrawEffects( int i, int j, SpriteBatch sb, ref Color drawColor, ref int nextSpecialDrawIndex ) {
			if( Main.rand.NextFloat() > 0.05f ) {
				return;
			}

			int wldX = (i * 16) + Main.rand.Next( 0, 16 );
			int wldY = (j * 16) + Main.rand.Next( 0, 16 );

			Dust.NewDust(
				Position: new Vector2(wldX, wldY),
				Width: 2,
				Height: 2,
				Type: 7,
				SpeedX: 0f,
				SpeedY: 0f,
				Alpha: 0,
				newColor: Color.Purple,
				Scale: 1f
			);
		}


		/*/// @private
		public override void RandomUpdate( int i, int j ) {
			var config = CursedBramblesConfig.Instance;
			int attempts = config.Get<int>( nameof(config.BrambleErodeRandomAttemptsPerTickPerSmallWorldArea) );

			if( attempts >= 1 ) {
				TileLibraries.KillTileSynced( i, j, false, false, true );
			}
		}*/
	}
}