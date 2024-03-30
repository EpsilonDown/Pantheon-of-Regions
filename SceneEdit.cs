namespace PantheonOfRegions;

public sealed partial class PantheonOfRegions {
	private static bool running = false;

	private static void EditScene(Scene prev, Scene next)
    {
        switch (next.name)
        {
			case "GG_Vengefly_V":
        		//Vengefly Kings + Gorb
                running = true;
				new GameObject Gorb = SpawnBoss("Gorb", new Vector2[30.0f,30.0f])
				break;
			
			case "GG_Mega_Moss_Charger":
        	//Moss Charger+Hornet 1
                running = true;
				break;
			
			case "GG_Failed_Champion":
        	//Failed Champion + Mawlek
                running = true;
				break;
		
			case "GG_Mantis_Lords_V":
		//Sisters of Battle + Hu
                running = true;
                GameObject battle2 = next.GetRootGameObjects().First(go => go.name == "Mantis Battle");
                InstantiateHu(battle2);
                battle2.Child("Mantis Lord Throne 2")
                    .LocateMyFSM("Mantis Throne Main")
                    .InsertCustomAction("Roar 2", () => {
                        HuInstance!.SetActive(true);
                        RingInstance!.SetActive(true);
                        HuInstance.transform.position = new Vector2(30f, 12f);
                        new[] { 1, 2, 3 }
                            .Map(i => "Battle Sub/Mantis Lord S" + i)
                            .Map(path => battle2.Child(path)!)
                            .Append(HuInstance!)
                            .ShareHealth(name: "Alliance Of Battle").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 2850 : 3650;
                    }, 4);
            break;
			
			case "GG_Crystal_Guardian_2":
        		//Crystal Guardian + Xero
				//Different region but close enough
                running = true;
				break;
			case "GG_Soul_Tyrant":
        	//Soul Warrior + Knight
                running = true;
				break;
			case "GG_Traitor_Lord":
                //Traitor Lord + Marmu
                running = true;
                
                GameObject TraitorLord = GameObject.Find("Battle Scene/Wave 3/Mantis Traitor Lord");
                PlayMakerFSM fsm = TraitorLord.LocateMyFSM("Mantis");
                fsm.RemoveAction("Slam?", 2);

                GameObject battle = next.GetRootGameObjects().First(go => go.name == "Battle Scene");
                InstantiateMarmu(battle);
                battle.Child("Wave 3/Mantis Traitor Lord")
                    .LocateMyFSM("Mantis")
                    .InsertCustomAction("Roar", () =>
                    {
                        MarmuInstance!.SetActive(true);
                        MarmuInstance.transform.position = new Vector2(40f, 40f);
                        new[] { "Wave 3/Mantis Traitor Lord" }
                            .Map(path => battle.Child(path)!)
                            .Append(MarmuInstance!)
                            .ShareHealth(name: "Queens Tributes").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 1216 : 1900;
                    }, 3);
                break;
			
			case "GG_Nailmasters":
        		//ALL Nailmasters!!!!
        		running = true;
				break;
			case "GG_Uumuu":
				//Uuwuu + No eyes
        		running = true;
				break;
			case "GG_Nosk_V":
        	//Nosk + Galien
                running = true;
				break;
			case "GG_White_Defender":
        	//Flukemarm + White Defender (Not the other meaning)
                running = true;
				break;
			case "GG_Hornet_2":
                //Hive Knight + Hornet
                running = true;
                GameObject battle3 = next.GetRootGameObjects().First(go => go.name == "Boss Holder");
                GameObject Hiveknight = GameObject.Find("Boss Holder/Hornet Boss 2");

                PlayMakerFSM fsm3 = Hiveknight.LocateMyFSM("Control");
                fsm3.RemoveAction("Phase Check", 2);
                fsm3.RemoveAction("Phase Check", 1);
                var phase = fsm3.GetState("Phase Check");
                phase.AddCustomAction(() => { fsm3.SendEvent("Phase 3"); }); 
                InstantiateHornet();
                HornetInstance!.SetActive(true);
                HornetInstance.transform.position = new Vector2(35f, 30f);
			
                battle3.Child("Hive Knight")
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
				break;
			case "GG_Watcher_Knights":
        	//Collector + Watcher knights
                running = true;
				break;
			case "GG_Gruz_Mother":
        	//Gruz + Sly
                running = true;
				break;
			case "GG_Grimm_Nightmare":
        	//NKG + Zote
                running = true;
				break;
			case "GG_Hollow_Knight":
        	//PV + Lost Kin
                running = true;
				break;
			case "GG_Radiance":
				//Absrad + Markoth + Seer
				running = true;
				break;
			
        default:
                running = false;
                return;
    	}
	}
}
