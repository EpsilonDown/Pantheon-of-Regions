using Vasi;
using HutongGames.PlayMaker.Actions;
using Satchel;
namespace PantheonOfRegions.Behaviours
{

    internal class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;
        private GameObject zoteling;
        private GameObject Burst;
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _control = gameObject.LocateMyFSM("Control");
            
        }

        private void Start()
        {
            zoteling = Instantiate(PantheonOfRegions.GameObjects["zoteling"]);
            Burst = Instantiate(PantheonOfRegions.GameObjects["volatilezoteling"]);
            Modding.Logger.Log("zote Edited 1/5");

            //zoteling.tag = "zoteling";
            //Burst.transform.parent = this.gameObject.transform;

            Modding.Logger.Log("zote Edited 2/5");

            //_control.GetAction<FindGameObject>("Spit L", 0).withTag = "zoteling";
            //_control.GetAction<FindGameObject>("Spit R", 0).withTag = "zoteling";
            Modding.Logger.Log("zote Edited 3/5");

            _constrainX.Fsm.GetFsmFloat("Edge L").Value = 70f;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = 103f;
            
            _control.Fsm.GetFsmFloat("Left X").Value = 72f;
            _control.Fsm.GetFsmFloat("Right X").Value = 101f;

            _control.GetAction<GGCheckIfBossScene>("Level Check").regularSceneEvent = _control.Fsm.Events.First(@event => @event.Name == "3");

            _control.GetAction<SetDamageHeroAmount>("Set Damage", 0).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 1).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 2).damageDealt = 1;
           

            _control.GetAction<Wait>("GG Pause").time = 3f;
            //change the start transition to just begin the spawn antics
            _control.RemoveAction("Roar", 7);
            _control.RemoveAction("Roar", 5);
            Modding.Logger.Log("zote Edited 4/5");
            //_control.GetState("Roar").AddAction(_control.GetAction<Wait>("GG Pause"));
            _control.AddCustomAction("Roar", () => { _control.SetState("Roar End"); });
            Modding.Logger.Log("zote Edited full");
        }
    }
}
