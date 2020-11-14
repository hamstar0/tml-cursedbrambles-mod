using Terraria.ModLoader;


namespace CursedBrambles {
	public class CursedBramblesMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-cursedbrambles-mod";


		////////////////

		public static CursedBramblesMod Instance { get; private set; }



		////////////////

		public CursedBramblesMod() {
			CursedBramblesMod.Instance = this;
		}

		////////////////

		public override void Load() {
			CursedBramblesConfig.Instance = ModContent.GetInstance<CursedBramblesConfig>();
		}

		public override void Unload() {
			CursedBramblesConfig.Instance = null;
			CursedBramblesMod.Instance = null;
		}
	}
}