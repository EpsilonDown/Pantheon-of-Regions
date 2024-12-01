using PantheonOfRegions.Behaviours;
using HutongGames.PlayMaker.Actions;
namespace PantheonOfRegions;
public sealed partial class BossAdder : MonoBehaviour
{
    private static bool running = false;
    private static GameObject[] loadedboss = null;
    public static void EditScene(Scene prev, Scene next)
    {


        GameObject SpawnBoss(string Boss, Vector2 spawnPoint)
        {
            GameObject boss = Instantiate(PantheonOfRegions.GameObjects[Boss], spawnPoint, Quaternion.identity);
            GameObject.DontDestroyOnLoad(boss);
            boss.AddComponent<EnemyTracker>();
            boss.SetActive(false);
            boss.tag = "custom";
            var hm = boss.GetComponent<HealthManager>();
            //hm.SetGeoSmall(0);
            //hm.SetGeoMedium(0);
            //hm.SetGeoLarge(0);

            return boss;
        }

        switch (next.name)
        {
			case "GG_Vengefly_V":
        		//Vengefly Kings + Gorb -> Minor Fix Needed
                running = true;
				GameObject Gorb = SpawnBoss("gorb", new Vector2 (43.0f,20.0f));

                GameObject.Find("Giant Buzzer Col")
                    .LocateMyFSM("Big Buzzer")
                    .InsertCustomAction("Intro Roar Left", () =>
                    {
                        Gorb!.SetActive(true);
                        new[] { "Giant Buzzer Col", "Giant Buzzer Col (1)" }
                        .Select(s => GameObject.Find(s))
                        .Append(Gorb)
                        .ShareHealth(name: "Howlers").HP = 1200;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Mega_Moss_Charger":
        	//Moss Charger+Hornet 1 -> DONE
                running = true;
				GameObject Hornet = SpawnBoss("hornetprotector", new Vector2 (60.0f,10.0f));
				GameObject MassiveMossCharger = GameObject.Find("Mega Moss Charger");
                MassiveMossCharger
                    .LocateMyFSM("Mossy Control")
                    .InsertCustomAction("Roar", () =>
                    {
                        Hornet.SetActive(true);
                        new[] { "Mega Moss Charger" }
                        .Map(s => GameObject.Find(s)).Append(Hornet)
                        .ShareHealth(name: "Ambushers").HP = 1000;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Failed_Champion":
        	//Failed Champion + Mawlek - Minor Fix Needed
                running = true;
				GameObject Mawlek = SpawnBoss("broodingmawlek", new Vector2 (60.0f,50.0f));
				GameObject FailedChampion = GameObject.Find("False Knight Dream");
                FailedChampion.AddComponent<EnemyTracker>();
                
                FailedChampion
                    .LocateMyFSM("FalseyControl")
                    .InsertCustomAction("Start Fall", () =>
                    {
                        Mawlek.SetActive(true);
                        new[] { FailedChampion, Mawlek }
                        .ShareHealth(name: "Crossroads").HP = 1430;
                    }, 2);


                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Mantis_Lords_V":
		//Sisters of Battle + Hu - Minor Fix?
                running = true;
				GameObject ElderHu = SpawnBoss("elderhu", new Vector2 (30.0f,15.0f));
                GameObject battle = next.GetRootGameObjects().First(go => go.name == "Mantis Battle");
                ElderHu.Child("Target").transform.position = new Vector2(30f,12f);
                battle.Child("Mantis Lord Throne 2")
                    .LocateMyFSM("Mantis Throne Main")
                    .InsertCustomAction("Roar 2", () => {
                        ElderHu!.SetActive(true);
 
                        new[] { 1, 2, 3 }
                            .Map(i => "Battle Sub/Mantis Lord S" + i)
                            .Map(path => battle.Child(path)!)
                            .Append(ElderHu!)
                            .ShareHealth(name: "Alliance Of Battle").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 2500 : 3500;
                    }, 4);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Crystal_Guardian_2":
        		//Crystal Guardian + Xero
				//Different region but close enough -> Done?
                running = true;
				GameObject Xero = SpawnBoss("xero", new Vector2 (30.0f,17.0f));
				GameObject EnragedGuardian = GameObject.Find("Battle Scene/Zombie Beam Miner Rematch");
                
                EnragedGuardian
                    .LocateMyFSM("Beam Miner") 
                    .InsertCustomAction("Battle Init", () =>
                    {
                        Xero!.SetActive(true);
                        new[] { "Battle Scene/Zombie Beam Miner Rematch" }
                        .Map(s => GameObject.Find(s)).Append(Xero)
                        .ShareHealth(name: "restless").HP = 1000;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Soul_Tyrant":
        	//Soul Warrior + Knight
                running = true;
				GameObject SoulWarrior = SpawnBoss("soulwarrior", new Vector2 (30.0f,30.0f));
				GameObject SoulTyrant = GameObject.Find("Dream Mage Lord");
                SoulTyrant
                    .LocateMyFSM("Mage Lord")
                    .InsertCustomAction("Roar", () =>
                    {
                        SoulWarrior.SetActive(true);
                        new[] { SoulWarrior, SoulTyrant }
                        .ShareHealth(name: "soulmasters").HP = 1500;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Traitor_Lord":
                //Traitor Lord + Marmu
                running = true;
                GameObject Marmu = SpawnBoss("marmu", new Vector2 (40.0f,36.0f));
                GameObject TraitorLord = GameObject.Find("Battle Scene/Wave 3/Mantis Traitor Lord");

                TraitorLord.LocateMyFSM("Mantis").RemoveAction("Slam?", 2);
				
                TraitorLord
                    .LocateMyFSM("Mantis")
                    .InsertCustomAction("Roar", () =>
                    {
                        Marmu.SetActive(true);
                        new[] { TraitorLord, Marmu }
                        .ShareHealth(name: "Queen's Tributes").HP = 1200;
					}, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Nailmasters":
        		//ALL Nailmasters!!!!
        		running = true;
				GameObject Oro = GameObject.Find("Brothers/Oro");
                GameObject Sheo = SpawnBoss("sheo", new Vector2(45.0f, 6.9f));

                Oro
                    .LocateMyFSM("nailmaster")
                    .InsertCustomAction("Reactivate", () =>
                    {
                       Sheo!.SetActive(true);
                       Sheo!.LocateMyFSM("nailmaster_sheo").SetState("Look");
                        new[] { "Brothers/Oro", "Brothers/Mato" }
                        .Map(s => GameObject.Find(s)).Append(Sheo)
                        .ShareHealth(name: "nailmasters").HP = 1500;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_Uumuu_V":
				//Uuwuu + No eyes
        		running = true;
				GameObject NoEyes = SpawnBoss("noeyes", new Vector2 (55.0f,120.0f));
				GameObject Uumuu = GameObject.Find("Mega Jellyfish GG");
                Uumuu
                    .LocateMyFSM("Mega Jellyfish")
                    .InsertCustomAction("Init", () =>
                    {
                        NoEyes!.SetActive(true);
                        new[] { Uumuu, NoEyes }
                        .ShareHealth(name: "blinders").HP = 1000;
                    }, 0);
                break;
		
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Nosk_V":
        	//Nosk + Galien - Almost end
                running = true;
				GameObject Galien = SpawnBoss("galien", new Vector2 (110.0f,10.0f));
				GameObject Nosk = GameObject.Find("Mimic Spider");
                Nosk
                    .LocateMyFSM("Mimic Spider")
                    .InsertCustomAction("GG Activate", () =>
                    {
                        Galien!.SetActive(true);
                        new[] { Nosk, Galien }
                        .ShareHealth(name: "blinders").HP = 1200;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_White_Defender":
        	//Flukemarm + White Defender (Not the other meaning)
                running = true;
				GameObject Flukemarm = SpawnBoss("flukemarm", new Vector2 (75.0f,20.0f));
                GameObject WhiteDefender = GameObject.Find("White Defender");
                WhiteDefender
                    .LocateMyFSM("Dung Defender")
                    .InsertCustomAction("Intro Roar", () =>
                    {
                        Flukemarm.SetActive(true);
                        new[] { WhiteDefender, Flukemarm }
                        .ShareHealth(name: "waterways").HP = 2000;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_Hornet_2":
                //Hive Knight + Hornet - Done
                running = true;
				
				GameObject HiveKnight = SpawnBoss("hiveknight", new Vector2 (30.0f,35.0f));
                GameObject HornetSentinel = GameObject.Find("Boss Holder/Hornet Boss 2");
                HornetSentinel.LocateMyFSM("Control").Fsm.GetFsmBool("Can Barb").Value = true;
                HornetSentinel
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        HiveKnight.SetActive(true);
                        new[] { HornetSentinel, HiveKnight }
                        .ShareHealth(name: "stinger knights").HP = 1500;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_God_Tamer":
        	//God Tamer + Obblelobles - Done??
                running = true;
				GameObject Oblobble1 = SpawnBoss("oblobble", new Vector2 (90.0f,10.0f));
                GameObject Lobster = GameObject.Find("Entry Object/Lobster");
                GameObject GodTamer = GameObject.Find("Entry Object/Lancer");
                Lobster
                    .LocateMyFSM("Control")
                    .AddCustomAction("Init", () =>
                    {
                        Oblobble1.SetActive(true);
                        new[] { Lobster, GodTamer, Oblobble1 }
                        .ShareHealth(name: "colosseum champions").HP = 1200;
                    });
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_Collector":
        	//Collector + Watcher knights
                running = true;
                GameObject Collector = GameObject.Find("Battle Scene/Jar Collector");
                Collector.AddComponent<EnemyTracker>();
                break;

////////////////////////////////////////////////////////////////////////////////////////////////////
				
			case "GG_Gruz_Mother":
        	//Gruz + Sly
                running = true;
				GameObject Sly = SpawnBoss("greatnailsagesly", new Vector2 (98.0f,15.0f));
                Sly.SetActive(true);
                GameObject GruzMother = GameObject.Find("_Enemies/Giant Fly");
                GruzMother.LocateMyFSM("Big Fly Control").GetAction<Wait>("Fly", 3).time.Value = 12f;
                GruzMother
                    .LocateMyFSM("Big Fly Control")
                    .InsertCustomAction("Init", () =>
                    {
                        
                        new[] { "_Enemies/Giant Fly" }
                        .Map(s => GameObject.Find(s)).Append(Sly)
                        .ShareHealth(name: "fly lords").HP = 1800;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Grimm_Nightmare":
        	//NKG + Zote
                running = true;
				GameObject Zote = SpawnBoss("greyprincezote", new Vector2 (150.0f,10.0f));
				GameObject NKG = GameObject.Find("Grimm Control/Nightmare Grimm Boss");

                NKG
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        Zote.SetActive(true);
                        new[] { "Grimm Control/Nightmare Grimm Boss" }
                        .Map(s => GameObject.Find(s)).Append(Zote)
                        .ShareHealth(name: "reapers").HP = 2600;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Hollow_Knight":
        	//PV + Lost Kin
                
                running = true;
				GameObject LostKin = SpawnBoss("lostkin", new Vector2 (35.0f,20.0f));
                GameObject PureVessel = GameObject.Find("Battle Scene/HK Prime");
                
                PureVessel
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Intro 4", () =>
                    {
                        LostKin!.SetActive(true);
                        new[] { "Battle Scene/HK Prime" }
                        .Map(s => GameObject.Find(s)).Append(LostKin)
                        .ShareHealth(name: "void vessels").HP = 2800;
                    }, 1);

                break;

            case "GG_Radiance":
				//Absrad + Markoth + Seer
				running = true;
				GameObject Markoth = SpawnBoss("markoth", new Vector2 (60.0f,25.0f));
                Markoth.SetActive(true);
                GameObject AbsoluteRadiance = GameObject.Find("Boss Control/Absolute Radiance");
				break;
            /* case "GG_Atrium_Roof":

                loadedboss = GameObject.FindGameObjectsWithTag("custom");
                if (loadedboss != null)
                {
                    foreach (GameObject loaded in loadedboss)
                    {
                        Destroy(loaded);
                    }
                }

                break;
            case "GG_Workshop":

                loadedboss = GameObject.FindGameObjectsWithTag("custom");
                if (loadedboss != null)
                {
                    foreach (GameObject loaded in loadedboss)
                    {
                        Destroy(loaded);
                    }
                }
                break; */

            default:
                running = false;
                return;
    	}
	}
}
