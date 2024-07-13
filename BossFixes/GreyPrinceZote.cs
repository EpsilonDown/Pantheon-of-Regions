using Vasi;
using HutongGames.PlayMaker.Actions;
namespace PantheonOfRegions.Behaviours
{
    internal class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = 70f;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = 103f;
            
            _control.Fsm.GetFsmFloat("Left X").Value = 72f;
            _control.Fsm.GetFsmFloat("Right X").Value = 101f;

            _control.GetAction<GGCheckIfBossScene>("Level Check").regularSceneEvent = _control.Fsm.Events.First(@event => @event.Name == "3");

            _control.GetAction<SetDamageHeroAmount>("Set Damage", 0).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 1).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 2).damageDealt = 1;

            //_control.SetState("Pause");

            //yield return new WaitUntil(() => _control.ActiveStateName == "Dormant");

            //GetComponent<HealthManager>().IsInvincible = false;
            //_control.SetState("Activate");
        }
    }
}
