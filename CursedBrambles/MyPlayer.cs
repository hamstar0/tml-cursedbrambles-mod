﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CursedBrambles {
	partial class CursedBramblesPlayer : ModPlayer {
		public bool IsPlayerBrambleTrailAPIEnabled { get; private set; } = false;
		
		public bool IsPlayerDefaultBrambleTrailElevationChecked { get; private set; } = false;

		////

		public int BrambleWakeTileRadius { get; private set; } = 64;

		public CursedBramblesAPI.GetTicks BrambleWakeTickRate { get; private set; } = ( out int i ) => {
			i = 15;
			return i;
		};

		public bool IsNearWarpBlockingBrambles { get; private set; } = false;

		////

		public CursedBramblesAPI.ValidateBrambleCreateAt BrambleCreateValidator { get; private set; } = null;


		////

		public bool IsPlayerProducingBrambleWake {
			get {
				if( this.IsPlayerBrambleTrailAPIEnabled ) {
					return !this.IsPlayerDefaultBrambleTrailElevationChecked
						|| this.CanPlayerDefaultCreateCursedBramblesNearby();
				}

				var config = CursedBramblesConfig.Instance;
				bool plrCanBrambleSetting = config.Get<bool>( nameof(config.PlayersCreateDefaultBrambleTrail) );

				return plrCanBrambleSetting
					&& this.CanPlayerDefaultCreateCursedBramblesNearby();
			}
		}


		////////////////

		private Vector2 OldPosition = default( Vector2 );

		private int BrambleProximityTimer = 0;
	}
}
