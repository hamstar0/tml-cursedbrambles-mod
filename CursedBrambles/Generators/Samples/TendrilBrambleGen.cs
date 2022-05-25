using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Errors;


namespace CursedBrambles.Generators.Samples {
	public partial class TendrilBrambleGen : BrambleGen {
		public BloomBrambleGen Parent { get; }

		protected int CurrTileX;
		protected int CurrTileY;

		public float Heading { get; protected set; } = 0f;



		////////////////

		public TendrilBrambleGen( BloomBrambleGen parent, int tickRate, int tileX, int tileY )
					: base( tickRate ) {
			this.Parent = parent;
			this.CurrTileX = tileX;
			this.CurrTileY = tileY;

			//

			this.Heading = (float)Math.PI * Main.rand.NextFloat();
		}
	}
}
