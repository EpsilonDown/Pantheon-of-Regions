using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions;

public sealed partial class PantheonOfRegions {

    private static GameObject? MarmuPrefab = null;
    private static GameObject? HuPrefab = null;
    private static GameObject? RingPrefab = null;
    private static GameObject? HivePrefab = null;
	
    private static GameObject? BossInstance = null;
	private static GameObject? RingInstance = null;


	public List<(string, string)> preloads = new();
    private Dictionary<string, (string, string)> _preloadDictionary = new()
        {
            #region
            ["brokenvessel"] = ("GG_Broken_Vessel", "Infected Knight"),
            ["broodingmawlek"] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            ["thecollector"] = ("GG_Collector_V", "Battle Scene/Jar Collector"),
            ["crystalguardian"] = ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
            ["turretcg1"] = ("GG_Crystal_Guardian", "Laser Turret Mega (1)"),
            ["enragedguardian"] = ("GG_Crystal_Guardian_2", "Battle Scene/Zombie Beam Miner Rematch"),
            ["turretcg2"] = ("GG_Crystal_Guardian_2", "Laser Turret Mega"),
            ["dungdefender"] = ("GG_Dung_Defender", "Dung Defender"),
            ["failedchampion"] = ("GG_Failed_Champion", "False Knight Dream"),
            ["falseknight"] = ("GG_False_Knight", "Battle Scene/False Knight New"),
            ["flukemarm"] = ("GG_Flukemarm", "Fluke Mother"),
            ["galien"] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ["hammer"] = ("GG_Ghost_Galien", "Warrior/Galien Hammer"),
            ["gorb"] = ("Cliffs_02_boss", "Warrior/Ghost Warrior Slug"),
            ["elderhu"] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ["ringholder"] = ("GG_Ghost_Hu", "Ring Holder"),
            ["markoth"] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            ["marmu"] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            ["noeyes"] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            ["xero"] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            ["greyprincezote"] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            ["troupemastergrimm"] = ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            ["grimmspikeholder"] = ("GG_Grimm", "Grimm Spike Holder"),
            ["nightmarekinggrimm"] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
            ["nightmaregrimmspikeholder"] = ("GG_Grimm_Nightmare", "Grimm Spike Holder"),
            ["grimmbats"] = ("GG_Grimm", "Grimm Bats"),
            ["nightmaregrimmbats"] = ("GG_Grimm_Nightmare", "Grimm Control/Grimm Bats"),
            ["gruzmother"] = ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
            ["hiveknight"] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            ["purevessel"] = ("GG_Hollow_Knight", "Battle Scene/HK Prime"),
            ["hornetprotector"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ["hornetsentinel"] = ("GG_Hornet_2", "Boss Holder/Hornet Boss 2"),
            ["barbregion"] = ("GG_Hornet_2", "Barb Region"),
            ["lostkin"] = ("GG_Lost_Kin", "Lost Kin"),
            ["palelurker"] = ("GG_Lurker", "Lurker Control/Pale Lurker"),
            ["soulwarrior"] = ("GG_Mage_Knight", "Mage Knight"),
            ["mantislord"] = ("GG_Mantis_Lords", "Mantis Battle/Battle Main/Mantis Lord"),
            ["massivemosscharger"] = ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
            ["mato"] = ("GG_Nailmasters", "Brothers/Mato"),
            ["oro"] = ("GG_Nailmasters", "Brothers/Oro"),
            ["nosk"] = ("GG_Nosk", "Mimic Spider"),
            ["wingednosk"] = ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
            ["globdropper"] = ("GG_Nosk_Hornet", "Battle Scene/Glob Dropper"),
            ["roofdust"] = ("GG_Nosk_Hornet", "Battle Scene/Roof Dust"),
            ["oblobble"] = ("GG_Oblobbles", "Mega Fat Bee"),
            ["paintmastersheo"] = ("GG_Painter", "Battle Scene/Sheo Boss"),
            ["absoluteradiance"] = ("GG_Radiance", "Boss Control/Absolute Radiance"),
            ["greatnailsagesly"] = ("GG_Sly", "Battle Scene/Sly Boss"),
            ["soulmaster"] = ("GG_Soul_Master", "Mage Lord"),
            ["soultyrant"] = ("GG_Soul_Tyrant", "Dream Mage Lord"),
            ["traitorlord"] = ("GG_Traitor_Lord", "Battle Scene/Wave 3/Mantis Traitor Lord"),
            ["uumuu"] = ("GG_Uumuu", "Mega Jellyfish GG"),
            ["jellyfishspawner"] = ("GG_Uumuu", "Jellyfish Spawner"),
            ["megajellyfishmultizaps"] = ("GG_Uumuu", "Mega Jellyfish Multizaps"),
            ["hatchercage"] = ("GG_Flukemarm", "Hatcher Cage (2)"),
            ["vengeflyking"] = ("GG_Vengefly", "Giant Buzzer Col"),
            ["watcherknight"] = ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
            ["whitedefender"] = ("GG_White_Defender", "White Defender"),
            ["thehollowknight"] = ("Room_Final_Boss_Core", "Boss Control/Hollow Knight Boss"),
            ["theradiance"] = ("Dream_Final_Boss", "Boss Control/Radiance"),
            ["sibling"] = ("Abyss_15", "Shade Sibling (14)"),
            #endregion
        };

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void SavepreloadedObjects(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {

        MarmuPrefab = CreatePrefab(preloadedObjects["GG_Ghost_Marmu"]["Warrior/Ghost Warrior Marmu"]);
        HuPrefab = CreatePrefab(preloadedObjects["GG_Ghost_Hu"]["Warrior/Ghost Warrior Hu"]);
        RingPrefab = CreatePrefab(preloadedObjects["GG_Ghost_Hu"]["Ring Holder"]);
        HivePrefab = CreatePrefab(preloadedObjects["GG_Hive_Knight"]["Battle Scene/Hive Knight"]);
    }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static GameObject CreatePrefab(GameObject preload) {
		var prefab = GameObject.Instantiate(preload);
		GameObject.DontDestroyOnLoad(prefab);
        return prefab;
	}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InstantiateMarmu(GameObject parent)
    {
		preloads.Add(_preloadDictionary[marmu]);
        GameObjects.Add(marmu, null);

		
        BossInstance = GameObject.Instantiate(MarmuPrefab)!;
        BossInstance!.transform.parent = parent.transform;

        HealthManager hm = BossInstance.GetComponent<HealthManager>();
        PlayMakerFSM fsm = BossInstance.LocateMyFSM("Control");

        #region FSM Changes

        #region Intro
        fsm.GetAction<Wait>("Start Pause", 0).time = 2.0f;
        #endregion
        #endregion
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InstantiateHu(GameObject parent)
    {
        BossInstance = GameObject.Instantiate(HuPrefab)!;
        RingInstance = GameObject.Instantiate(RingPrefab)!;

        BossInstance!.transform.parent = parent.transform;

        HealthManager hm = BossInstance.GetComponent<HealthManager>();
        PlayMakerFSM fsm = HuInstance.LocateMyFSM("Attacking");
        PlayMakerFSM move = HuInstance.LocateMyFSM("Movement");

        void SetRingPositions(Vector2 ringRootPos)
        {
            RingInstance.transform.position = ringRootPos;
            foreach (var ring in RingInstance.GetComponentsInChildren<PlayMakerFSM>(true))
            {
                ring.ChangeTransition("Init", "FINISHED", "Antic");
                var ringpos = ring.transform.position;
                var down = ring.GetState("Down");
                var fc = down.GetFirstActionOfType<FloatCompare>();
                fc.float2 = ringpos.y - 7.7f;

                var land = ring.GetState("Land");
                var spos = land.GetFirstActionOfType<SetPosition>();
                spos.y = ringpos.y - 8f;

                var reset = ring.GetState("Reset");
                var spos2 = reset.GetFirstActionOfType<SetPosition>();
                spos2.y = ringRootPos.y;
            }
        }
        var init = fsm.GetState("Init");
        init.DisableAction(1);
        init.InsertCustomAction(() =>
        {
            fsm.FsmVariables.GetFsmGameObject("Ring Holder").Value = RingInstance;
        }, 0);
        init.AddCustomAction(() => { fsm.SendEvent("READY"); });

        var ringAntic = fsm.GetState("Ring Antic");
        ringAntic.GetAction<Wait>(0).time = 2f;

        var wait = fsm.GetState("Wait");
        wait.AddAction(new Wait() { time = 1.5f });
        wait.AddCustomAction(() => { fsm.SendEvent("FINISHED"); });

        var placeRings = fsm.GetState("Place Rings");
        placeRings.AddCustomAction(() =>
        {
            SetRingPositions(new Vector3(30f, 18f));
        });

        fsm.RemoveAction("Mega Warp Out", 0);
        /*fsm.ChangeTransition("Choice 2", "MEGA", "Choice"); */


        move.GetAction<SetPosition>("Set 1", 1).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Set 2", 1).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Set 3", 0).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Set 4", 0).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Set 5", 0).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Set 6", 0).vector = new Vector3(30f, 20f, 0f);
        move.GetAction<SetPosition>("Warp", 2).vector = new Vector3(30f, 20f, 0f);

    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InstantiateHive()
    {
        HornetInstance = GameObject.Instantiate(HivePrefab)!;

        HealthManager hm = HiveInstance.GetComponent<HealthManager>();
        /*PlayMakerFSM fsm = HornetInstance.LocateMyFSM("Control");
        var inert = fsm.GetState("Inert");
        inert.RemoveTransition("GG BOSS");
        inert.RemoveTransition("BATTLE START");
        inert.RemoveTransition("REFIGHT");
        inert.AddCustomAction(() => { fsm.SendEvent("Refight Ready"); });*/
    }
}
