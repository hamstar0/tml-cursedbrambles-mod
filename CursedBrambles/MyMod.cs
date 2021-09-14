using Terraria.ModLoader;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-cursedbrambles-mod";


		////////////////

		public static CursedBramblesMod Instance { get; private set; }



		////////////////

		public bool IsSoulBarriersLoaded { get; private set; } = false;

		public bool IsWorldGatesLoaded { get; private set; } = false;



		////////////////

		public override void Load() {
			CursedBramblesMod.Instance = this;

			this.IsSoulBarriersLoaded = ModLoader.GetMod( "SoulBarriers" ) != null;
			this.IsWorldGatesLoaded = ModLoader.GetMod( "WorldGates" ) != null;

			this.LoadBrambleCreateHooks();
		}

		public override void Unload() {
			CursedBramblesMod.Instance = null;
		}
	}
}