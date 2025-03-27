using PantheonOfRegions.Behaviours;
using HutongGames.PlayMaker.Actions;
using Osmi.Game;
namespace PantheonOfRegions;
public sealed partial class BossAdder : MonoBehaviour
{
    private static bool running = false;
    private static GameObject[] loadedboss = { };
    public static void EditScene(Scene prev, Scene next)
    {
        BossSpawner Spawner = new BossSpawner();
        GameObject SpawnBoss(string Boss, Vector2 spawnPoint)
        {
            GameObject boss = Spawner.SpawnBoss(Boss, spawnPoint);
            loadedboss.Append(boss);
            return boss;
        }
        
        switch (prev.name)
        {

            case "GG_Crystal_Guardian_2":

                for (int i = 1; i < 5; i++)
                { Destroy(GameObject.Find("Sword" + i)); }

                break;
            case "GG_White_Defender":


                break;

            case "GG_Hornet_2":

                break;
            case "GG_Collector":

                for (int k = 0; k < 10; k++)
                { PantheonCleanup(); }

                break;
            case "GG_Gruz_Mother":
                PantheonCleanup();
                Destroy(GameObject.Find("Spin Tink"));
                break;

            case "GG_Grimm_Nightmare":
                for (int i = 0; i <= 4; i++)
                { Destroy(GameObject.Find("Zote Balloon(Clone)(Clone)")); }
                for (int i = 0; i <= 4; i++)
                { Destroy(GameObject.Find("Zoteling(Clone)(Clone)")); }

                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Hollow_Knight":
                //PV + Lost Kin
                for (int k = 0; k < 10; k++)
                {
                    Destroy(GameObject.Find("Shade Sibling (14)(Clone)(Clone)"));
                }

                break;
        }
        
        switch (next.name)
        {
            case "GG_Vengefly_V":
                //Vengefly Kings + Gorb -> Minor Fix Needed
                running = true;
                GameObject Gorb = SpawnBoss("gorb", new Vector2(43.0f, 20.0f));
                GameObject.Find("Giant Buzzer Col")
                    .LocateMyFSM("Big Buzzer")
                    .InsertCustomAction("Check Dir 2", () =>
                    {
                        Gorb!.SetActive(true);
                        new[] { "Giant Buzzer Col", "Giant Buzzer Col (1)" }
                        .Map(s => GameObject.Find(s)).Append(Gorb!)
                        .ShareHealth(name: "Howlers").HP = BossSceneController.Instance.BossLevel == 0 ? 1200 : 1800;
                    }, 0);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Mega_Moss_Charger":
                running = true;
                GameObject Hornet = SpawnBoss("hornetprotector", new Vector2(60.0f, 10.0f));
                GameObject MassiveMossCharger = GameObject.Find("Mega Moss Charger");
                MassiveMossCharger
                    .LocateMyFSM("Mossy Control")
                    .InsertCustomAction("Roar", () =>
                    {
                        Hornet.SetActive(true);
                        new[] { MassiveMossCharger, Hornet }
                        .ShareHealth(name: "Ambushers").HP = 1000;
                    }, 2);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Failed_Champion":
                //Failed Champion + Mawlek - Minor Fix Needed
                running = true;

                GameObject FailedChampion = GameObject.Find("False Knight Dream");
                FailedChampion.AddComponent<EnemyTracker>();



                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Mantis_Lords_V":
                running = true;
                GameObject ElderHu = SpawnBoss("elderhu", new Vector2(30.0f, 15.0f));
                GameObject battle = next.GetRootGameObjects().First(go => go.name == "Mantis Battle");
                ElderHu.Child("Target").transform.position = new Vector2(30f, 12f);
                battle.Child("Mantis Lord Throne 2")
                    .LocateMyFSM("Mantis Throne Main")
                    .InsertCustomAction("Roar 2", () =>
                    {
                        ElderHu!.SetActive(true);

                        new[] { 1, 2, 3 }
                            .Map(i => "Battle Sub/Mantis Lord S" + i)
                            .Map(path => battle.Child(path)!)
                            .Append(ElderHu!)
                            .ShareHealth(name: "Alliance Of Battle").HP =
                                BossSceneController.Instance.BossLevel == 0 ? 2000 : 2400;
                    }, 4);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Crystal_Guardian_2":
                running = true;
                GameObject Xero = SpawnBoss("xero", new Vector2(30.0f, 17.0f));
                GameObject EnragedGuardian = GameObject.Find("Battle Scene/Zombie Beam Miner Rematch");

                EnragedGuardian
                    .LocateMyFSM("Beam Miner")
                    .InsertCustomAction("Battle Init", () =>
                    {
                        Xero!.SetActive(true);
                        new[] { EnragedGuardian, Xero }
                        .ShareHealth(name: "fanatics").HP = 1000;
                    }, 2);

                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Soul_Tyrant":
                //Soul Warrior + Knight
                running = true;
                GameObject SoulWarrior = SpawnBoss("soulwarrior", new Vector2(30.0f, 30.0f));
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
                GameObject Marmu = SpawnBoss("marmu", new Vector2(40.0f, 36.0f));
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
                running = true;
                GameObject Oro = GameObject.Find("Brothers/Oro");
                Oro.AddComponent<EnemyTracker>();
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            case "GG_Uumuu_V":
                running = true;
                GameObject NoEyes = SpawnBoss("noeyes", new Vector2(55.0f, 120.0f));
                GameObject Uumuu = GameObject.Find("Mega Jellyfish GG");
                Uumuu
                    .LocateMyFSM("Mega Jellyfish")
                    .InsertCustomAction("Start", () =>
                    {
                        NoEyes!.SetActive(true);
                        new[] { Uumuu, NoEyes }
                        .ShareHealth(name: "blinders").HP = 800;
                    }, 9);
                break;

            case "GG_Uumuu":
                running = true;
                GameObject NoEyes2 = SpawnBoss("noeyes", new Vector2(55.0f, 120.0f));
                GameObject Uumuu2 = GameObject.Find("Mega Jellyfish GG");
                
                Uumuu2
                    .LocateMyFSM("Mega Jellyfish")
                    .InsertCustomAction("Start", () =>
                    {
                        NoEyes2!.SetActive(true);
                        new[] { Uumuu2, NoEyes2 }
                        .ShareHealth(name: "blinders2").HP = 800;
                        
                    },9); 
                break;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Nosk_V":
                running = true;
                GameObject Galien = SpawnBoss("galien", new Vector2(110.0f, 10.0f));
                GameObject Nosk = GameObject.Find("Mimic Spider");
                Nosk
                    .LocateMyFSM("Mimic Spider")
                    .InsertCustomAction("GG Activate", () =>
                    {
                        Galien!.SetActive(true);
                        new[] { Nosk, Galien }
                        .ShareHealth(name: "stalkers").HP = 1200;
                    }, 0);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            case "GG_White_Defender":
                //Flukemarm + White Defender (Not the other meaning)
                running = true;
                GameObject Flukemarm = SpawnBoss("flukemarm", new Vector2(75.0f, 20.0f));
                GameObject WhiteDefender = GameObject.Find("White Defender");
                WhiteDefender
                    .LocateMyFSM("Dung Defender")
                    .InsertCustomAction("Erupt Out First 2", () =>
                    {
                        Flukemarm!.SetActive(true);
                        new[] { WhiteDefender, Flukemarm }
                        .ShareHealth(name: "waterways").HP = 2000;
                    }, 0);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            case "GG_Hornet_2":
                running = true;

                GameObject HiveKnight = SpawnBoss("hiveknight", new Vector2(30.0f, 36.0f));
                GameObject HornetSentinel = GameObject.Find("Boss Holder/Hornet Boss 2");
                HornetSentinel.LocateMyFSM("Control").Fsm.GetFsmBool("Can Barb").Value = true;
                HornetSentinel
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        HiveKnight.SetActive(true);
                        new[] { HornetSentinel, HiveKnight }
                        .ShareHealth(name: "stinger knights").HP = 1600;
                    }, 0);
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            case "GG_God_Tamer":
                //God Tamer + Obblelobles - Done??
                running = true;
                GameObject Oblobble1 = SpawnBoss("oblobble", new Vector2(90.0f, 10.0f));
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
                Collector
                    .LocateMyFSM("Control")
                    .InsertCustomAction("Init", () =>
                    {
                        new[] { Collector }
                        .ShareHealth(name: "citycollector").HP = 1800;
                    }, 0);
                break;

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            case "GG_Gruz_Mother":
                //Gruz + Sly
                running = true;
                GameObject GruzMother = GameObject.Find("_Enemies/Giant Fly");
                GameObject Sly = Spawner.SpawnBoss("greatnailsagesly", new Vector2(98.0f, 15.0f));
                //PlayMakerFSM FlyDeath = GameObject.Find("Battle Scene").LocateMyFSM("Battle Control");
                //FlyDeath.GetState("End").RemoveAction(1);
                //FlyDeath.GetState("End").RemoveAction(0);

                Sly.SetActive(true);
                GruzMother.LocateMyFSM("Big Fly Control").InsertCustomAction("Wake", () =>
                {
                    new[] { GruzMother , Sly }.ShareHealth(name: "fly lords").HP = 2000;
                }, 0);
                GruzMother.AddComponent<EnemyTracker>();

                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Grimm_Nightmare":
                //NKG + Zote
                running = true;
                GameObject NKG = GameObject.Find("Grimm Control/Nightmare Grimm Boss");
                GameObject Zote = Spawner.SpawnBoss("greyprincezote", new Vector2(90.0f, 10.0f));
                for (int i = 0; i < 4; i++)
                {
                    GameObject balloon = Spawner.SpawnBoss("volatilezoteling", new Vector2(70.0f + 10 * i, 10.0f));
                    balloon.SetActive(true);
                }
                Zote.SetActive(true);
                NKG.LocateMyFSM("Control").InsertCustomAction("Init", () =>
                {
                    new[] { NKG, Zote }.ShareHealth(name: "reapers");
                }, 0);
                NKG.AddComponent<EnemyTracker>();

                


                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            case "GG_Hollow_Knight":
                //PV + Lost Kin

                running = true;
                GameObject LostKin = SpawnBoss("lostkin", new Vector2(35.0f, 20.0f));
                GameObject PureVessel = GameObject.Find("Battle Scene/HK Prime");
                PureVessel.AddComponent<EnemyTracker>();
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
                GameObject Markoth = SpawnBoss("markoth", new Vector2(55.0f, 30.0f));
                GameObject Seer = SpawnBoss("seer", new Vector2(65.0f, 30.0f));

                PlayMakerFSM BattleScene = GameObject.Find("Boss Control").LocateMyFSM("Control");
                
                BattleScene.AddCustomAction("Appear Boom", () =>
                    {
                        Markoth!.SetActive(true);
                        Seer!.SetActive(true);
                    });
                BattleScene.AddCustomAction("Battle Start", () =>
                {
                    GameObject AbsoluteRadiance = GameObject.Find("Boss Control/Absolute Radiance");
                    new[] { AbsoluteRadiance, Markoth }.ShareHealth(name: "moths").HP = 3600;
                    AbsoluteRadiance.AddComponent<AbsoluteRadiance>();
                });
                break;

            /*
            case "GG_Watcher_Knights":
                running = true;
                GameObject Collector2 = SpawnBoss("thecollector", new Vector2(23.0f, 12.0f));
                GameObject wk2 = GameObject.Find("Battle Control/Black Knight 1");

                break; */

            case "GG_Spa":

                PantheonCleanup();

                break;

            case "GG_Atrium_Roof":

                PantheonCleanup();

                break;

            case "GG_Workshop":

                PantheonCleanup();

                break;

            default:
                running = false;
                return;
        }

       
    }
    private static void PantheonCleanup()
    {
            foreach (GameObject boss in loadedboss)
            {
                Destroy(boss);
            }
        /*
        foreach (GameObject boss in PantheonOfRegions.GameObjects.Values)
        {
            Destroy(GameObject.Find(boss.name + "(Clone)"));
            Destroy(GameObject.Find("Corpse " + boss.name));
            Destroy(GameObject.Find(boss.name.Replace("Warrior", "Death")));
        } 8*/
    }
}
