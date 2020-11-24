using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace CursedBrambles {
	partial class CursedBramblesItem : GlobalItem {
		public override bool CanUseItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.MagicMirror:
			case ItemID.IceMirror:
			case ItemID.RecallPotion:
			case ItemID.CellPhone:
				if( !this.CanUseWarpItem(player) ) {
					if( player.whoAmI == Main.myPlayer ) {
						Timers.SetTimer( "CursedBramblesWarpBlock", 2, false, () => {
							Main.NewText( "Cannot use warp items near brambles.", Color.Yellow );
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

		private bool CanUseWarpItem( Player player ) {
			return !player.GetModPlayer<CursedBramblesPlayer>().IsNearBrambles;
		}
	}
}
