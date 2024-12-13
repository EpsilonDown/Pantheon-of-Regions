using HutongGames.PlayMaker.Actions;
using Osmi.Game;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class FailedChampion : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _hpcheck;
        private int sharedhp;
        private bool end = false;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("FalseyControl");
            _hpcheck = gameObject.LocateMyFSM("Check Health");
            
        }

        private void Start()
        {


            _control.GetState("Check GG").ChangeTransition("FINISHED","Recover");
            

            _hpcheck.GetState("Check").RemoveAction(1);
            _hpcheck.GetState("Check").RemoveAction(0);
            
            _control.GetState("Rubble End").AddAction(_control.GetAction("State 1",2));
            _control.GetState("Rubble End").AddAction(new Wait()
            {
                time = new(2.5f),
                finishEvent = FsmEvent.GetFsmEvent("FINISHED")
            }); 
            
        }
        private void Update()
        {
            sharedhp = GameObject.Find("Crossroads").GetComponent<SharedHealthManager>().HP;
            if (sharedhp < 1030 && _hpcheck.Fsm.GetFsmBool("Stun 1").Value == false)
            {
                _hpcheck.Fsm.GetFsmBool("Stun 1").Value = true;
                _hpcheck.SendEvent("STUN");

            }
            else if (sharedhp < 530 && _hpcheck.Fsm.GetFsmBool("Stun 2").Value == false)
            {
                _hpcheck.Fsm.GetFsmBool("Stun 2").Value = true;
                _hpcheck.SendEvent("STUN");

            }
            else if (sharedhp < 30 && end == false)
            {
                _hpcheck.SendEvent("STUN");
                gameObject.GetComponent<HealthManager>().StopSharing(50);
                
                
                end = true;
            }
        }
    }
}