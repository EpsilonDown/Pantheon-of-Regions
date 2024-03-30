using InControl;
using Osmi;

namespace PantheonOfRegions
{
    public partial class PantheonOfRegions:Mod,ITogglableMod
    {
		private List<string> scenes = new List<string> { "GG_Hollow_Knight", "GG_Radiance", "GG_Grimm_Nightmare" };
		public static Setting gs = new Setting();
		public bool isCustom = false;
        public PantheonOfRegions() =>
			OsmiHooks.SceneChangeHook += EditScene;
        public override string GetVersion() => "v0.1";
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            SavepreloadedObjects(preloadedObjects);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += AddDoor;
            ModHooks.LanguageGetHook += ChangeText;
            ModHooks.GetPlayerVariableHook += ChangeCustomDoorVar;
            On.BossSequenceController.SetupNewSequence += BossSequenceController_SetupNewSequence;
            On.BossSceneController.Start += CheckHUD;
        }


        private IEnumerator CheckHUD(On.BossSceneController.orig_Start orig, BossSceneController self)
        {
			//from HUDInChecker
			yield return orig(self);
			if(BossSequenceController.IsInSequence&&!scenes.Contains(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
            {
				yield return new WaitUntil(() => GameManager.instance.gameState == GameState.PLAYING);
				GameCameras.instance.hudCanvas.LocateMyFSM("Slide Out").SendEvent("IN");
			}
        }

        private void BossSequenceController_SetupNewSequence(On.BossSequenceController.orig_SetupNewSequence orig, BossSequence sequence, BossSequenceController.ChallengeBindings bindings, string playerData)
        {
            orig(sequence, bindings, playerData);
			if (sequence.achievementKey == "")
			{
				isCustom = true;
			}
			else
			{
				isCustom = false;
			}
		}


        private object ChangeCustomDoorVar(Type type, string name, object value)
        {
            if(name== "CustomBossDoor")
            {
				return new BossSequenceDoor.Completion
				{
					viewedBossSceneCompletions=gs.PantheonRooms
				};
            }
			return value;
        }

		
  
        private void AddDoor(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
			bool flag = arg1.name == "GG_Atrium_Roof";
			if (flag)
			{
				GameManager.instance.StartCoroutine(SetPantheon());
			}
			if(arg0.name== "GG_End_Sequence")
            {
				HeroController.instance.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
			Log("Set Custom Door");
		}
		public void Unload()
        {
			UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= AddDoor;
			ModHooks.LanguageGetHook -= ChangeText;
			ModHooks.GetPlayerVariableHook -= ChangeCustomDoorVar;
			On.BossSequenceController.SetupNewSequence -= BossSequenceController_SetupNewSequence;
			On.BossSceneController.Start -= CheckHUD;
        }
	}
}
