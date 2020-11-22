using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace CursedBrambles.Buffs {
	partial class TheShadowDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "The Shadow" );
			this.Description.SetDefault( "A dark force stalks you in deep places" );

			Main.debuff[this.Type] = true;
			Main.buffNoTimeDisplay[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
		}
	}
}
