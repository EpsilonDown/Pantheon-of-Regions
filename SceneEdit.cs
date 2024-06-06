using JetBrains.Annotations;
using Osmi.Game;

namespace PantheonOfRegions;
public sealed partial class BossAdder : MonoBehaviour
{
    private static bool running = false;

	public static void EditScene(Scene prev, Scene next)
    {


        GameObject SpawnBoss(string Boss, Vector2 spawnPoint)
        {
            GameObject boss = Instantiate(PantheonOfRegions.GameObjects[Boss], spawnPoint, Quaternion.identity);
            boss.AddComponent<EnemyTracker>();
            boss.SetActive(false);
            GameObject.DontDestroyOnLoad(boss);
            var hm = boss.GetComponent<HealthManager>();
            hm.SetGeoSmall(0);
            hm.SetGeoMedium(0);
            hm.SetGeoLarge(0);

            return boss;
        }

        switch (next.name)
        {
			case "GG_Vengefly_V":
        		//Vengefly Kings + Gorb
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
                        .ShareHealth(name: "Howlers").HP = 1500;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Mega_Moss_Charger":
        	//Moss Charger+Hornet 1
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
                        .ShareHealth(name: "Ambushers").HP = 1380;
                    }, 2);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Failed_Champion":
        	//Failed Champion + Mawlek
                running = true;
				GameObject Mawlek = SpawnBoss("broodingmawlek", new Vector2 (60.0f,50.0f));
				GameObject FailedChampion = GameObject.Find("False Knight Dream");
                Mawlek.SetActive(true);
                /* FailedChampion
                    .LocateMyFSM("FalseyControl")
                    .InsertCustomAction("Start Fall", () =>
                    {
                        
                        new[] { "False Knight Dream" }
                        .Map(s => GameObject.Find(s)).Append(Mawlek)
                        .ShareHealth(name: "Crossroads").HP = 1800;
                    }, 2); */
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Mantis_Lords_V":
		//Sisters of Battle + Hu
                running = true;
				GameObject ElderHu = SpawnBoss("elderhu", new Vector2 (30.0f,15.0f));
                GameObject battle = next.GetRootGameObjects().First(go => go.name == "Mantis Battle");
				
                battle.Child("Mantis Lord Throne 2")
                    .LocateMyFSM("Mantis Throne Main")
                    .InsertCustomAction("Roar 2", () => {
                        ElderHu!.SetActive(true);
 
                        new[] { 1, 2, 3 }
                            .Map(i => "Battle Sub/Mantis Lord S" + i)
                            .Map(path => battle.Child(path)!)
                            .Append(ElderHu!)
                            .ShareHealth(name: "Alliance Of Battle").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 2850 : 3650;
                    }, 4);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Crystal_Guardian_2":
        		//Crystal Guardian + Xero
				//Different region but close enough
                running = true;
				GameObject Xero = SpawnBoss("xero", new Vector2 (30.0f,15.0f));
				GameObject EnragedGuardian = GameObject.Find("Battle Scene/Zombie Beam Miner Rematch");
				Xero!.SetActive(true);
                /* EnragedGuardian
                    .LocateMyFSM("Beam Miner")
                    .InsertCustomAction("Battle Init", () =>
                    {
                        
                        new[] { "Battle Scene/Zombie Beam Miner Rematch" }
                        .Map(s => GameObject.Find(s)).Append(Xero)
                        .ShareHealth(name: "guardians").HP = 1300;
                    }, 2); */
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
                        new[] { "Dream Mage Lord" }
                        .Map(s => GameObject.Find(s)).Append(SoulWarrior)
                        .ShareHealth(name: "soulmasters").HP = 1650;
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
                        new[] { "Battle Scene/Wave 3/Mantis Traitor Lord" }
						.Map(s => GameObject.Find(s)).Append(Marmu)
                        .ShareHealth(name: "Queen's Tributes").HP = 1300;
					}, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Nailmasters":
        		//ALL Nailmasters!!!!
        		running = true;
				GameObject Sheo = SpawnBoss("sheo", new Vector2 (45f,6.4f));
				GameObject Painting = SpawnBoss("painting", new Vector3 (47.8f, 6.4f,2));
				GameObject Oro = GameObject.Find("Brothers/Oro");
				GameObject Mato = GameObject.Find("Brothers/Mato");
                Oro
                    .LocateMyFSM("nailmaster")
                    .InsertCustomAction("Recovery Roar", () =>
                    {
                        Sheo.SetActive(true);
						Painting.SetActive(true);
                        new[] { "Brothers/Oro", "Brothers/Mato" }
                        .Map(s => GameObject.Find(s)).Append(Sheo)
                        .ShareHealth(name: "nailmasters").HP = 1650;
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
                        new[] { "Mega Jellyfish GG" }
                        .Map(s => GameObject.Find(s)).Append(NoEyes)
                        .ShareHealth(name: "blinders").HP = 1000;
                    }, 0);
                break;
		
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Nosk_V":
        	//Nosk + Galien
                running = true;
				GameObject Galien = SpawnBoss("galien", new Vector2 (100.0f,100.0f));
				GameObject Hammer = SpawnBoss("hammer", new Vector2 (30.0f,30.0f));
				GameObject Nosk = GameObject.Find("Mimic Spider");
                Nosk
                    .LocateMyFSM("Mimic Spider")
                    .InsertCustomAction("Init", () =>
                    {
                        Galien!.SetActive(true);
                        Hammer!.SetActive(true);
                        new[] { "Mimic Spider" }
                        .Map(s => GameObject.Find(s)).Append(Galien)
                        .ShareHealth(name: "blinders").HP = 1330;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_White_Defender":
        	//Flukemarm + White Defender (Not the other meaning)
                running = true;
				GameObject Flukemarm = SpawnBoss("flukemarm", new Vector2 (75.0f,40.0f));
				
                Flukemarm.SetActive(true);
                GameObject WhiteDefender = GameObject.Find("White Defender");
                WhiteDefender
                    .LocateMyFSM("Dung Defender")
                    .InsertCustomAction("Intro Roar", () =>
                    {
                        
                        new[] { "White Defender" }
                        .Map(s => GameObject.Find(s)).Append(Flukemarm)
                        .ShareHealth(name: "waterways").HP = 2100;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_Hornet_2":
                //Hive Knight + Hornet
                running = true;
				
				GameObject HiveKnight = SpawnBoss("hiveknight", new Vector2 (30.0f,30.0f));
				GameObject HornetSentinel = GameObject.Find("Boss Holder/Hornet Boss 2");

                HornetSentinel
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        HiveKnight.SetActive(true);
                        new[] { "Boss Holder/Hornet Boss 2" }
                        .Map(s => GameObject.Find(s)).Append(HiveKnight)
                        .ShareHealth(name: "stinger knights").HP = 1650;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_God_Tamer":
        	//God Tamer + Obblelobles
                running = true;
				GameObject Oblobble1 = SpawnBoss("oblobble", new Vector2 (90.0f,10.0f));
                GameObject Lobster = GameObject.Find("Entry Object/Lobster");
                GameObject GodTamer = GameObject.Find("Entry Object/Lancer");
                Lobster
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Wake", () =>
                    {
                        Oblobble1.SetActive(true);
                        new[] { "Entry Object/Lobster", "Entry Object/Lancer" }
                        .Map(s => GameObject.Find(s)).Append(Oblobble1)
                        .ShareHealth(name: "colosseum champions").HP = 1200;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////

			case "GG_Watcher_Knights":
        	//Collector + Watcher knights
                running = true;
				GameObject Collector = SpawnBoss("collector", new Vector2 (60.0f,30.0f));
				GameObject WatcherKnight = GameObject.Find("Battle Control/Black Knight 1");
				break;

////////////////////////////////////////////////////////////////////////////////////////////////////
				
			case "GG_Gruz_Mother":
        	//Gruz + Sly
                running = true;
				GameObject Sly = SpawnBoss("greatnailsagesly", new Vector2 (98.0f,16.0f));
                Sly.SetActive(true);
                GameObject GruzMother = GameObject.Find("_Enemies/Giant Fly");
                GruzMother
                    .LocateMyFSM("Big Fly Control")
                    .InsertCustomAction("Init", () =>
                    {
                        
                        new[] { "_Enemies/Giant Fly" }
                        .Map(s => GameObject.Find(s)).Append(Sly)
                        .ShareHealth(name: "lord of flies").HP = 1700;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Grimm_Nightmare":
        	//NKG + Zote
                running = true;
				GameObject Zote = SpawnBoss("greyprincezote", new Vector2 (100.0f,10.0f));
				GameObject NKG = GameObject.Find("Grimm Control/Nightmare Grimm Boss");
				Zote.SetActive(true);
				/*
                NKG
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        
                        new[] { "Grimm Control/Nightmare Grimm Boss" }
                        .Map(s => GameObject.Find(s)).Append(Zote)
                        .ShareHealth(name: "reapers").HP = 2600;
                    }, 0); */
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Hollow_Knight":
        	//PV + Lost Kin
                running = true;
				GameObject LostKin = SpawnBoss("lostkin", new Vector2 (50.0f,30.0f));
				GameObject Sibling = SpawnBoss("sibling", new Vector2 (50.0f,30.0f));
                GameObject PureVessel = GameObject.Find("Battle Scene/HK Prime");
				
                PureVessel
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Intro Roar", () =>
                    {
                        LostKin!.SetActive(true);
                        new[] { "Battle Scene/HK Prime" }
                        .Map(s => GameObject.Find(s)).Append(LostKin)
                        .ShareHealth(name: "void vessels").HP = 2800;
                    }, 0);
                break;
////////////////////////////////////////////////////////////////////////////////////////////////////
			case "GG_Radiance":
				//Absrad + Markoth + Seer
				running = true;
				GameObject Markoth = SpawnBoss("markoth", new Vector2 (60.0f,25.0f));
                Markoth.SetActive(true);
                GameObject AbsoluteRadiance = GameObject.Find("Boss Control/Absolute Radiance");
				break;
			
        default:
                running = false;
                return;
    	}
	}
}
