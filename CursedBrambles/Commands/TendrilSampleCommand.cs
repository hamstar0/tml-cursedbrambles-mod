﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Generators;
using CursedBrambles.Generators.Samples;


namespace CursedBrambles.Commands {
	class TendrilSampleCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.World;
		/// @private
		public override string Command => "cb-brambletendril";
		/// @private
		public override string Usage => $"/{this.Command} <size>";
		/// @private
		public override string Description => "Create a sample bramble tendril at mouse cursor"
			+ "\n   Parameters: <size>";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( Main.netMode != NetmodeID.SinglePlayer ) {
				caller.Reply( "Not available in MP.", Color.Yellow );
				return;
			}

			if( args.Length != 1 ) {
				caller.Reply( "Invalid parameters.", Color.Yellow );
				return;
			}

			if( !Int32.TryParse(args[0], out int size) ) {
				caller.Reply( "Invalid size parameter.", Color.Yellow );
				return;
			}

			//

			var gen = new TendrilBrambleGen(
				info: new BramblePatchInfo( size ),
				tickRate: 4,
				tileX: (int)Main.MouseWorld.X / 16,
				tileY: (int)Main.MouseWorld.Y / 16
			);

			CursedBramblesAPI.AddBrambleGenerator( gen );

			caller.Reply( "Bramble tendril created.", Color.Lime );
		}
	}
}
