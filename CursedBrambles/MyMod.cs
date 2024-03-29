using Terraria.ModLoader;


namespace CursedBrambles {
	public partial  class CursedBramblesMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-cursedbrambles-mod";


		////////////////

		public static CursedBramblesMod Instance { get; private set; }



		////////////////

		public bool IsSoulBarriersLoaded { get; private set; } = false;



		////////////////

		public override void Load() {
			CursedBramblesMod.Instance = this;

			this.IsSoulBarriersLoaded = ModLoader.GetMod( "SoulBarriers" ) != null;
		}

		public override void Unload() {
			CursedBramblesMod.Instance = null;
		}


		////////////////

		public override void MidUpdateDustTime() {
			if( this.IsSoulBarriersLoaded ) {
				CursedBramblesMod.UpdateBarrierCollisionsIf_SoulBarriers_WeakRef();
			}
		}
	}
}