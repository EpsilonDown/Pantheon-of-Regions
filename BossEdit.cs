using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions;

public sealed partial class PantheonOfRegions {

    private static GameObject? MarmuPrefab = null;
    private static GameObject? HuPrefab = null;
    private static GameObject? RingPrefab = null;
    private static GameObject? HivePrefab = null;
	
    private static GameObject? BossInstance = null;
	private static GameObject? RingInstance = null;

    public override List<(string, string)> GetPreloadNames() => new() {
		("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
        ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
        ("GG_Ghost_Hu", "Ring Holder"),
        ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
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
