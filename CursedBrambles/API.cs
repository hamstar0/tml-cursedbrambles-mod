using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Loadable;
using ModLibsGeneral.Libraries.Tiles;
using CursedBrambles.Tiles;


namespace CursedBrambles {
	public partial class CursedBramblesAPI : ILoadable {
		public delegate bool ValidateBrambleCreateAt( int tileX, int tileY );



		////////////////
		
		public static ValidateBrambleCreateAt CreatePlayerAvoidingBrambleValidator( int tileRadius ) {
			return ( x, y ) => {
				var wldPos = new Vector2( x * 16, y * 16 );
				int plrMax = Main.player.Length;
				float maxLenSqr = tileRadius * 16;
				maxLenSqr *= maxLenSqr;

				for( int i=0; i<plrMax; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true || plr.dead ) {
						continue;
					}

					if( (plr.MountedCenter - wldPos).LengthSquared() < maxLenSqr ) {
						return false;
					}
				}

				return true;
			};
		}



		////////////////

		public static bool GetPlayerBrambleWakeStatus(
					Player player,
					out bool manuallyActivated,
					out bool isElevationConsidered,
					out int radius,
					out int tickRate,
					out ValidateBrambleCreateAt validateAt ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();

			manuallyActivated = myplayer.IsPlayerBrambleTrailAPIEnabled;
			isElevationConsidered = myplayer.IsPlayerDefaultBrambleTrailElevationChecked;
			radius = myplayer.BrambleWakeRadius;
			tickRate = myplayer.BrambleWakeTickRate;
			validateAt = myplayer.BrambleCreateValidator;
			return myplayer.IsPlayerProducingBrambleWake;
		}


		////

		public static bool SetPlayerToCreateBrambleWake(
					Player player,
					bool isElevationChecked,
					int radius,
					int tickRate,
					ValidateBrambleCreateAt validateAt ) {
			if( CursedBramblesConfig.Instance.DebugModeInfo ) {
				IList<string> ctx = DebugLibraries.GetContextSlice();
				LogLibraries.Log( "SetPlayerToCreateBrambleWake called from: "+string.Join("\n  ", ctx) );
			}

			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.ActivateBrambleWake( isElevationChecked, radius, tickRate, validateAt );

			return true;
		}

		public static bool UnsetPlayerBrambleWakeCreating( Player player ) {
			var myplayer = player.GetModPlayer<CursedBramblesPlayer>();
			myplayer.DeactivateBrambleWake();

			return true;
		}


		////////////////

		public static void ClearBramblesWithinArea( Rectangle area, bool syncFromServer ) {
			if( syncFromServer && Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			int brambleType = ModContent.TileType<CursedBrambleTile>();

			for( int i = area.Left; i < area.Right; i++ ) {
				for( int j = area.Top; j < area.Bottom; j++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					if( syncFromServer ) {
						TileLibraries.KillTileSynced( i, j, false, false, true );
					} else {
						WorldGen.KillTile( i, j );
						WorldGen.SquareTileFrame( i, j );
					}
				}
			}
		}
	}
}
