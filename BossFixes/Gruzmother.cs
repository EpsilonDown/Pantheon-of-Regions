using HutongGames.PlayMaker.Actions;
using Osmi.Game;

namespace PantheonOfRegions.Behaviours
{
    internal class Gruzmother : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _bounce;
        private GameObject healthsharer;
        //private int sharedhp;
        private bool end = false;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Big Fly Control");
            _bounce = gameObject.LocateMyFSM("bouncer_control");
            
        }

        private void Start()
        {
            /*GameObject.Find("gg_battle_transitions(Clone)").SetActive(false);
            GameObject corpse1 = gameObject.transform.GetChild(2).gameObject;
            corpse1.SetActive(true);

            if (corpse1 != null) {
                corpse1.LocateMyFSM("corpse").GetState("Blow").RemoveAction(9);
                //Destroy(corpse1.LocateMyFSM("corpse"));
                Destroy(corpse1.GetComponent<EndBossSceneTimer>());
                Modding.Logger.Log("corpse action removed");
            }
            else
            {
                Modding.Logger.Log("corpse not removed!");
            } */
            healthsharer = GameObject.Find("fly lords");

            _control.GetAction<Wait>("GG Extra Pause", 0).time = 5f;
            _control.AddState("Pause");
            _control.AddTransition("Pause","FINISHED","Super Choose");
            _control.AddCustomAction("Pause",() => {_bounce.SendEvent("STOP");});
            _control.AddAction("Pause", new Wait()
            {
                time = new(4f),
                finishEvent = FsmEvent.GetFsmEvent("FINISHED")
            });
            _control.AddState("Dead");

        }
        private void Update()
        {
            if (healthsharer.GetComponent<SharedHealthManager>().HP < 600 && end == false)
            {
                _control.SetState("Dead");
                GameObject.Find("Sly Boss(Clone)(Clone)").LocateMyFSM("Control").SendEvent("ZERO HP");
                gameObject.GetComponent<HealthManager>().IsInvincible = true;
                end = true;
            }

        }
    }
}
