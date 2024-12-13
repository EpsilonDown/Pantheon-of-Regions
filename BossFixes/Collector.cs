using Vasi;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Osmi.Utils;
using Osmi.Game;

namespace PantheonOfRegions.Behaviours
{
    internal class TheCollector : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private GameObject buzzer;
        private GameObject roller;
        private GameObject spitter;
        private Vector3 spawnpos;
        private int minioncount;
        private int knightcount = 0;
        private Quaternion rotation = Quaternion.identity;
        private Vector2 defaultpos = new Vector2(50f, 100f);
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            gameObject.GetComponent<HealthManager>().hp = 3000;
            
            
            
            //roller = Instantiate(PantheonOfRegions.GameObjects["watcherknight"]);
            //spitter = Instantiate(PantheonOfRegions.GameObjects["belfly"]);
            
            Modding.Logger.Log("Collector stuff instantiated");
        }

        private void Start()
        {
            /*
            _control.GetState("Init").RemoveAction(13);
            _control.GetState("Init").RemoveAction(12);
            _control.GetState("Init").RemoveAction(11);
            Modding.Logger.Log("Collector Edited 1/3");

            _control.GetAction<SetSpawnJarContents>("Buzzer").enemyPrefab.Value = Instantiate(PantheonOfRegions.GameObjects["wingedsentry"]);
            _control.GetAction<SetSpawnJarContents>("Roller").enemyPrefab.Value = Instantiate(PantheonOfRegions.GameObjects["watcherknight"]);
            _control.GetAction<SetSpawnJarContents>("Spitter").enemyPrefab.Value = Instantiate(PantheonOfRegions.GameObjects["belfly"]);
            
            _control.InsertCustomAction("Buzzer", () =>
            {
                buzzer = _control.GetAction<SetSpawnJarContents>("Buzzer").enemyPrefab.Value;
                DontDestroyOnLoad(buzzer);
                buzzer.SetActive(true);
                //roller.AddComponent<EnemyTracker>();
                Modding.Logger.Log("buzzer spawn");

            },0); 

            _control.Fsm.GetFsmInt("Buzzer HP").Value = 40;
            _control.Fsm.GetFsmInt("Roller HP").Value = 400;
            _control.Fsm.GetFsmInt("Spitter HP").Value = 10;

            Modding.Logger.Log("Collector Edited 2/3");
            
            _control.AddCustomAction("Init", () =>
            {

                Modding.Logger.Log("Collector Edited 2/3");

                _control.Fsm.GetFsmGameObject("Buzzers").Value = buzzer;
                _control.Fsm.GetFsmGameObject("Buzzer Prefab").Value = buzzer;
                //buzzer.tag = "boss";
                _control.Fsm.GetFsmGameObject("Spitters").Value = spitter;
                _control.Fsm.GetFsmGameObject("Spitters Prefab").Value = spitter;

                _control.Fsm.GetFsmGameObject("Rollers").Value = roller;
                _control.Fsm.GetFsmGameObject("Rollers Prefab").Value = roller;
                Modding.Logger.Log("Collector Edited 3/3");

                _control.SendEvent("FINISHED"); 
            }); */
            /*
            _control.GetState("Roller").AddTransition("BUZZER","buzzer");
            _control.GetState("Roller").AddTransition("SPITTER", "spitter");
            _control.AddCustomAction("Buzzer", () =>
            {
               //Count Watcher knights. If above 2, summon others!
            }); */
        }
        private void Update()
        {
            buzzer = GameObject.Find("Buzzer R(Clone)");
            roller = GameObject.Find("Roller R(Clone)");
            spitter = GameObject.Find("Spitter R(Clone)");
            if (buzzer != null)
            {
                spawnpos = buzzer.transform.position;
                GameObject spawn = Instantiate(PantheonOfRegions.GameObjects["wingedsentry"], spawnpos, rotation);
                spawn.SetActive(true);
                DontDestroyOnLoad(spawn);
                Destroy(buzzer);
            }
            if (roller != null & knightcount < 6)
            {
                spawnpos = roller.transform.position + new Vector3(0f, 2f, 0f);
                GameObject spawn = Instantiate(PantheonOfRegions.GameObjects["watcherknight"], spawnpos, rotation);
                spawn.AddComponent<EnemyTracker>();
                spawn.SetActive(true);
                DontDestroyOnLoad(spawn);
                spawn.GetComponent<HealthManager>().AddToShared(GameObject.Find("citycollector").GetComponent<SharedHealthManager>());
                spawn.GetComponent<HealthManager>().hp = 350;
                knightcount++;
                Destroy(roller);
            }
            if (spitter != null)
            {
                spawnpos = spitter.transform.position;
                GameObject spawn = Instantiate(PantheonOfRegions.GameObjects["belfly"], spawnpos, rotation);
                spawn.SetActive(true);
                DontDestroyOnLoad(spawn);
                Destroy(spitter);
            }
        }
    }
}
