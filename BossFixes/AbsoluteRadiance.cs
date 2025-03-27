using HutongGames.PlayMaker.Actions;
using Osmi.Game;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    class AbsoluteRadiance : MonoBehaviour
    {
        private PlayMakerFSM _commands;
        private PlayMakerFSM _control;
        private PlayMakerFSM _phase;
        private PlayMakerFSM mk_attack;
        private PlayMakerFSM mk_shield;
        private GameObject Markoth;
        private GameObject healthsharer;
        private GameObject Shield;

        private void Awake()
        {
            _commands = gameObject.LocateMyFSM("Attack Commands");
            _control = gameObject.LocateMyFSM("Control");
            _phase = gameObject.LocateMyFSM("Phase Control");
        }

        private void Start()
        {
            Markoth = GameObject.Find("Ghost Warrior Markoth(Clone)(Clone)");
            mk_shield = Markoth.LocateMyFSM("Shield Attack");
            mk_attack = Markoth.LocateMyFSM("Attacking");

            healthsharer = GameObject.Find("moths");

            Shield = Instantiate(GameObject.Find("Markoth Shield(Clone)"));

            if (Shield != null) { Modding.Logger.Log("shield found"); }
            Shield!.SetActive(false);

            #region Phase Controller
            _phase.GetState("Check 1").RemoveAction<GetHP>();
            _phase.GetState("Check 2").RemoveAction<GetHP>();
            _phase.GetState("Check 3").RemoveAction<GetHP>();
            _phase.Fsm.GetFsmInt("HP").Value = 3600;
            _phase.Fsm.GetFsmInt("P2 Spike Waves").Value = 3200;
            _phase.Fsm.GetFsmInt("P3 A1 Rage").Value = 2600;
            _phase.Fsm.GetFsmInt("P4 Stun1").Value = 2200;
            _phase.Fsm.GetFsmInt("P5 Ascend").Value = 1200;

            _phase.InsertCustomAction("Check 1", () => {
                _phase.Fsm.GetFsmInt("HP").Value = healthsharer.GetComponent<SharedHealthManager>().HP;
            }, 0);
            _phase.InsertCustomAction("Check 2", () => {
                _phase.Fsm.GetFsmInt("HP").Value = healthsharer.GetComponent<SharedHealthManager>().HP;
            }, 0);
            _phase.InsertCustomAction("Check 3", () => {
                _phase.Fsm.GetFsmInt("HP").Value = healthsharer.GetComponent<SharedHealthManager>().HP;
            }, 0);
            _phase.InsertCustomAction("Check 4", () => {
                _phase.Fsm.GetFsmInt("HP").Value = healthsharer.GetComponent<SharedHealthManager>().HP;
            }, 0);
            #endregion

            _control.AddCustomAction("First Tele", () =>
            {
                mk_shield.SendEvent("SHIELD END");
            });

            _commands.AddCustomAction("Nail Fan", () =>
            {
                mk_shield.SetState("Ready");
                mk_attack.SetState("Nail Fan");
            });
            _commands.AddCustomAction("NF Glow", () =>
            {
                mk_shield.SetState("Ready");
                mk_attack.SetState("Reflector");
            });
            _commands.AddCustomAction("End", () =>
            {
                mk_shield.SendEvent("SHIELD END");
                mk_attack.SendEvent("SHIELD END");
            });
            _commands.AddCustomAction("EB End", () =>
            {
                mk_shield.SendEvent("SHIELD END");
                mk_attack.SendEvent("ATTACK END");
            });

            _commands.AddCustomAction("Stun1 Start", () =>
            {
                mk_shield.SetState("Ready");

            });

        }
    }
}
