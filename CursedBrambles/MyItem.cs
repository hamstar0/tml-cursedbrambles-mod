using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace CursedBrambles {
	partial class CursedBramblesItem : GlobalItem {
		public override bool CanUseItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.MagicMirror:
			case ItemID.IceMirror:
			case ItemID.RecallPotion:
			case ItemID.CellPhone:
				if( player.GetModPlayer<CursedBramblesPlayer>().IsNearWarpBlockingBrambles ) {
					if( player.whoAmI == Main.myPlayer ) {
						Timers.SetTimer( "CursedBramblesWarpBlock", 2, false, () => {
							Main.NewText( "Too many cursed brambles close by to warp.", Color.Yellow );
							return false;
						} );
					}
					return false;
				}
				break;
			}

			return base.CanUseItem( item, player );
		}


		////

		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( item.type != ItemID.PurificationPowder ) {
				return;
			}

			var config = CursedBramblesConfig.Instance;
			if( config.Get<bool>( nameof(config.PurificationPowderRemovesBrambles) ) ) {
				tooltips.Add(
					new TooltipLine(
						this.mod,
						"CursedBrambles_PurificationPowder",
						"Removes cursed brambles."
					)
				);
			}
		}
	}
}
