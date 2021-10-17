using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial class CursedBramblesWorld : ModWorld {
		internal ISet<(int, int)> BramblesSnapshot = new HashSet<(int, int)>();



		////////////////

		public override void Initialize() {
			this.BramblesSnapshot.Clear();

			//

			int brambleType = ModContent.TileType<CursedBrambleTile>();

			for( int j=1; j<Main.maxTilesY; j++ ) {
				for( int i=1; i<Main.maxTilesX; i++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					this.BramblesSnapshot.Add( (i, j) );
				}
			}
		}


		////////////////

		//public override void PreUpdate() {
		//	this.UpdateSnapshotSlowScan();
		//}


		////////////////

		 private int _LastSnapshotSlowScanPosition = 0;

		private void UpdateSnapshotSlowScan() {
			int brambleType = ModContent.TileType<CursedBrambleTile>();
			int minX = this._LastSnapshotSlowScanPosition % Main.maxTilesX;
			int minY = this._LastSnapshotSlowScanPosition / Main.maxTilesX;

			int scanned = 0;

			for( int j=minY; j<Main.maxTilesY; j++ ) {
				for( int i=minX; i<Main.maxTilesX; i++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					this.BramblesSnapshot.Add( (i, j) );

					this._LastSnapshotSlowScanPosition++;

					if( scanned++ >= 100 ) {
						goto SCAN_COMPLETE;
					}
				}
			}
			
			SCAN_COMPLETE:

			if( this._LastSnapshotSlowScanPosition >= (Main.maxTilesX * Main.maxTilesY) ) {
				this._LastSnapshotSlowScanPosition = 0;
			}
		}
	}
}