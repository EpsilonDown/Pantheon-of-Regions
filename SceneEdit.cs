namespace PantheonOfRegions;

public sealed partial class PantheonOfRegions {
	private static bool running = false;

	private static void EditScene(Scene prev, Scene next)
    {
		new GameObject battle = null;
        switch (next.name)
        {
			case "GG_Vengefly_V":
        		//Vengefly Kings + Gorb
                running = true;
				new GameObject Gorb = SpawnBoss("gorb", new Vector2 (30.0f,30.0f));
				GameObject VengeflyKing = GameObject.Find("Giant Buzzer Col");
				break;
			
			case "GG_Mega_Moss_Charger":
        	//Moss Charger+Hornet 1
                running = true;
				new GameObject Hornet = SpawnBoss("hornetprotector", new Vector2 (30.0f,30.0f));
				GameObject MassiveMossCharger = GameObject.Find("Mega Moss Charger");
				break;
			
			case "GG_Failed_Champion":
        	//Failed Champion + Mawlek
                running = true;
				new GameObject Mawlek = SpawnBoss("mawlek", new Vector2 (30.0f,30.0f));
				GameObject FailedChampion = GameObject.Find("Giant Buzzer Col");
				break;
		
			case "GG_Mantis_Lords_V":
		//Sisters of Battle + Hu
                running = true;
				new GameObject ElderHu = SpawnBoss("elderhu", new Vector2 (30.0f,30.0f));
				new GameObject RingHolder = SpawnBoss("ringholder", new Vector2 (30.0f,30.0f));
				
				GameObject battle = next.GetRootGameObjects().First(go => go.name == "Boss Holder");
				
                battle2.Child("Mantis Lord Throne 2")
                    .LocateMyFSM("Mantis Throne Main")
                    .InsertCustomAction("Roar 2", () => {
                        ElderHu!.SetActive(true);
                        new[] { 1, 2, 3 }
                            .Map(i => "Battle Sub/Mantis Lord S" + i)
                            .Map(path => battle2.Child(path)!)
                            .Append(ElderHu)
                            .ShareHealth(name: "Alliance Of Battle").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 2850 : 3650;
                    }, 4);
            break;
			
			case "GG_Crystal_Guardian_2":
        		//Crystal Guardian + Xero
				//Different region but close enough
                running = true;
				new GameObject Xero = SpawnBoss("xero", new Vector2 (30.0f,30.0f));
				GameObject EnragedGuardian = GameObject.Find("Battle Scene/Zombie Beam Miner Rematch");
				break;
				
			case "GG_Soul_Tyrant":
        	//Soul Warrior + Knight
                running = true;
				new GameObject SoulWarrior = SpawnBoss("soulwarrior", new Vector2 (30.0f,30.0f));
				GameObject SoulTyrant = GameObject.Find("Dream Mage Lord");
				break;
				
			case "GG_Traitor_Lord":
                //Traitor Lord + Marmu
                running = true;
                new GameObject Marmu = SpawnBoss("marmu", new Vector2 (30.0f,30.0f));
                GameObject TraitorLord = GameObject.Find("Battle Scene/Wave 3/Mantis Traitor Lord");
				
				TraitorLord.LocateMyFSM("Mantis").RemoveAction("Slam?", 2);
				TraitorLord.transform.parent = battle
				Marmu.transform.parent = battle
				
                battle.Child("Wave 3/Mantis Traitor Lord")
                    .LocateMyFSM("Mantis")
                    .InsertCustomAction("Roar", () =>
                    {
                        new[] {"Wave 3/Mantis Traitor Lord"}.Map(path => battle.Child(path)!).Append(Marmu!).ShareHealth(name: "Queens Tributes").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 1216 : 1900;
                    }, 3);
                break;
			
			case "GG_Nailmasters":
        		//ALL Nailmasters!!!!
        		running = true;
				new GameObject Sheo = SpawnBoss("paintmaster", new Vector2 (30.0f,30.0f));
				GameObject Oro = GameObject.Find("Brothers/Oro");
				GameObject Mato = GameObject.Find("Brothers/Mato");
				break;

				
			case "GG_Uumuu":
				//Uuwuu + No eyes
        		running = true;
				new GameObject NoEyes = SpawnBoss("noeyes", new Vector2 (30.0f,30.0f));
				GameObject Uumuu = GameObject.Find("Mega Jellyfish GG");
				break;

				
			case "GG_Nosk_V":
        	//Nosk + Galien
                running = true;
				new GameObject Galien = SpawnBoss("galien", new Vector2 (30.0f,30.0f));
				new GameObject Hammer = SpawnBoss("hammer", new Vector2 (30.0f,30.0f));
				GameObject Nosk = GameObject.Find("Mimic Spider");
				break;
				
			case "GG_White_Defender":
        	//Flukemarm + White Defender (Not the other meaning)
                running = true;
				new GameObject Flukemarm = SpawnBoss("flukemarm", new Vector2 (30.0f,30.0f));
				GameObject WhiteDefender = GameObject.Find("White Defender");
				break;
				
			case "GG_Hornet_2":
                //Hive Knight + Hornet
                running = true;
				
				new GameObject HiveKnight = SpawnBoss("hiveknight", new Vector2 (30.0f,30.0f));
				GameObject HornetSentinel = GameObject.Find("Boss Holder/Hornet Boss 2");
				
                
                battle.Child("Hive Knight")
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Activate", () =>
                    {
                        HornetInstance!.SetActive(true);
                        HornetInstance.transform.position = new Vector2(70f, 30f);
                        new[] { "Hive Knight" }
                            .Map(path => battle3.Child(path)!)
                            .Append(HornetInstance!)
                            .ShareHealth(name: "Stinger Knights").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 1216 : 1900;
                    }, 3);
                break;

			case "GG_God_Tamer":
        	//God Tamer + Obblelobles
                running = true;
				new GameObject Oblobble1 = SpawnBoss("oblobble", new Vector2 (30.0f,30.0f));
				new GameObject Oblobble2 = SpawnBoss("oblobble", new Vector2 (60.0f,30.0f));
				break;
				
			case "GG_Watcher_Knights":
        	//Collector + Watcher knights
                running = true;
				new GameObject Collector = SpawnBoss("collector", new Vector2 (30.0f,30.0f));
				GameObject WatcherKnight = GameObject.Find("Battle Control/Black Knight 1");
				break;
				
			case "GG_Gruz_Mother":
        	//Gruz + Sly
                running = true;
				new GameObject Sly = SpawnBoss("greatnailsagesly", new Vector2 (30.0f,30.0f));
				GameObject GruzMother = GameObject.Find("_Enemies/Giant Fly");
				break;
				
			case "GG_Grimm_Nightmare":
        	//NKG + Zote
                running = true;
				new GameObject Zote = SpawnBoss("greyprincezote", new Vector2 (30.0f,30.0f));
				GameObject NKG = GameObject.Find("Grimm Control/Nightmare Grimm Boss");
				break;
				
			case "GG_Hollow_Knight":
        	//PV + Lost Kin
                running = true;
				new GameObject VoidKin = SpawnBoss("lostkin", new Vector2 (30.0f,30.0f));
				new GameObject Sibling = SpawnBoss("sibling", new Vector2 (30.0f,30.0f));
				GameObject PureVessel = GameObject.Find("Battle Scene/HK Prime");
				break;
				
			case "GG_Radiance":
				//Absrad + Markoth + Seer
				running = true;
				new GameObject Markoth = SpawnBoss("markoth", new Vector2 (30.0f,30.0f));
				GameObject AbsoluteRadiance = GameObject.Find("Boss Control/Absolute Radiance");
				break;
			
        default:
                running = false;
                return;
    	}
	}
}
