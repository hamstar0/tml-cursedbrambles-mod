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
		}

		public override void Unload() {
			CursedBramblesMod.Instance = null;
		}
	}
}