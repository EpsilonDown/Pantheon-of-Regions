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
			OsmiHooks.SceneChangeHook += BossAdder.EditScene;
        public override string GetVersion() => "v0.1";
	
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += AddDoor;
            ModHooks.GetPlayerVariableHook += ChangeCustomDoorVar;
            ModHooks.LanguageGetHook += ChangeText;
            On.BossSequenceController.SetupNewSequence += BossSequenceController_SetupNewSequence;
            On.BossSceneController.Start += CheckHUD;

            Dictionary<string, GameObject> gameObjects = new();
            foreach (KeyValuePair<string, GameObject> pair in GameObjects)
            {
                string goName = pair.Key;
                if (_preloadDictionary.Keys.Contains(goName))
                {
                    (string sceneName, string enemyPath) = _preloadDictionary[goName];
                    GameObject gameObject = preloadedObjects[sceneName][enemyPath];
                    gameObjects.Add(goName, gameObject);
                }
            }

            foreach (KeyValuePair<string, GameObject> pair in gameObjects)
                GameObjects[pair.Key] = pair.Value;

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
        public string ChangeText(string key, string sheetTitle, string orig) => key switch
        {
            "CustomBossDoorSuper" => "Pantheon of",
            "CustomBossDoorTitle" => "Regions",
            "CustomBossDoorDesc" => "Fight Gods Attuned through the Region",
            "VENGEFLY_MAIN" => "Howling",
            "VENGEFLY_SUPER" => "Ascenders",
            "MEGA_MOSS_MAIN" => "Ambushers",
            "MEGA_MOSS_SUPER" => "Green",
            "FALSE_KNIGHT_DREAM_MAIN" => "Guardians of",
            "FALSE_KNIGHT_DREAM_SUB" => "Crossroads",
            "SISTERS_MAIN" => "Alliance",
            "SISTERS_SUB" => "of Battle",
            "ENRAGED_GUARDIAN_SUPER" => "Restless",
            "ENRAGED_GUARDIAN_MAIN" => "Guardians",
            "MAGE_LORD_DREAM_SUPER" => "",
            "MAGE_LORD_DREAM_MAIN" => "Soul Masters",
            "TRAITOR_LORD_MAIN" => "Queen's",
            "TRAITOR_LORD_SUB" => "Tributes",
            "NM_ORO_SUPER" => "Family",
            "NM_ORO_MAIN" => "Nailmasters",
            "MEGA_JELLY_MAIN" => "Blind Protectors",
            "MIMIC_SPIDER_MAIN" => "Stalking Warriors",
            "WHITE_DEFENDER_MAIN" => "Guardians of ",
            "WHITE_DEFENDER_SUB" => "Waterways",
            "HORNET_MAIN" => "Stinger Knights",
            "LOBSTER_LANCER_C_SUPER" => "Champions of",
            "LOBSTER_LANCER_C_MAIN" => "Colosseum",
            "BIGFLY_MAIN" => "Lord of Flies",
            "BIGFLY_SUB" => "",
            "GRIMM_NIGHTMARE_SUPER" => "Reapers of",
            "GRIMM_NIGHTMARE_MAIN" => "Dreams",
            "HK_PRIME_MAIN" => "Void Vessels",
            "ABSOLUTE_RADIANCE_MAIN" => "RADIANCE",
            "ABOLUTE_RADIANCE_SUPER" => "Mother Of Moths",

            _ => orig

        };


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
			ModHooks.GetPlayerVariableHook -= ChangeCustomDoorVar;
            ModHooks.LanguageGetHook -= ChangeText;
            On.BossSequenceController.SetupNewSequence -= BossSequenceController_SetupNewSequence;
			On.BossSceneController.Start -= CheckHUD;
			Log("Unloaded");
        }
	}
}
