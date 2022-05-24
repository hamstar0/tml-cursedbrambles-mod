using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CursedBrambles.Generators;


namespace CursedBrambles {
	public partial class CursedBramblesMod : Mod {
		public override void MidUpdateTimeWorld() { // <- use instead of ModWorld.PreUpdate
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				CursedBramblesMod.UpdateBrambleErode();

				ModContent.GetInstance<BrambleGenManager>().Update();
			}
		}
	}
}