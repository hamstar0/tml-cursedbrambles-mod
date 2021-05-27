using System;
using Terraria;
using Terraria.ModLoader;


namespace CursedBrambles.Buffs {
	partial class TheShadowDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "The Shadow" );
			this.Description.SetDefault( "Evil growths appear around you" );

			Main.debuff[this.Type] = true;
			Main.buffNoTimeDisplay[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
		}
	}
}
