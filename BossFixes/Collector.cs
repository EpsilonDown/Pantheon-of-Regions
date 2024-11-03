using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class TheCollector : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            gameObject.GetComponent<HealthManager>().hp = 3000;
        }

        private void Start()
        {
            /* _control.Fsm.GetFsmFloat("Bottle XL").Value = 41f;
            _control.Fsm.GetFsmFloat("Bottle XR").Value = 67f;
            _control.Fsm.GetFsmFloat("Roof X L").Value = 41f;
            _control.Fsm.GetFsmFloat("Roof X R").Value = 67f;
            _control.Fsm.GetFsmFloat("Roof Y").Value = 103f;
            _control.Fsm.GetFsmFloat("Return Y").Value = 95f; 

            GameObject spawnJar = _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value;
            spawnJar.GetComponent<SpawnJarControl>().spawnY = 100f;
            spawnJar.GetComponent<SpawnJarControl>().breakY = 95f;
            spawnJar.CreatePool(3);
            _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value = spawnJar;
            _control.Fsm.GetFsmGameObject("Spawn Jar").Value = spawnJar;
            _control.GetAction<FloatCompare>("Spawn").tolerance = 1;

            _death.ChangeTransition("Start Effect", "FINISHED", "Kill Enemies"); 

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true; */
            _control.GetState("Init").RemoveAction(13);
            _control.GetState("Init").RemoveAction(12);
            _control.GetState("Init").RemoveAction(11);

            Modding.Logger.Log("Collector Edited 1/3");

            _control.AddCustomAction("Init", () =>
            {

                GameObject buzzer = GameObject.Find("Battle Scene/Wave 2/Ruins Flying Sentry");
                GameObject roller = GameObject.Find("Battle Control/Black Knight 1");
                GameObject spitter = GameObject.Find("Ceiling Dropper (1)");
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
            });

            _control.GetState("Roller").AddTransition("BUZZER","buzzer");
            _control.GetState("Roller").AddTransition("SPITTER", "spitter");
            _control.AddCustomAction("Buzzer", () =>
            {
               //Count Watcher knights. If above 2, summon others!
            });
        }
    }
}
