using Vasi;
using HutongGames.PlayMaker.Actions;
namespace PantheonOfRegions.Behaviours
{

    internal class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;
        private GameObject NKG;
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _control = gameObject.LocateMyFSM("Control");
            
        }

        private void Start()
        {
            
            gameObject.transform.position = new Vector3(88f, 10f, 0f);

            _control.RemoveAction("Spit L", 0);
            _control.RemoveAction("Spit R", 0);
            _control.RemoveAction("Level 1", 0);
            _control.RemoveAction("Level 2", 0);
            _control.RemoveAction("Level 3", 0);
            _control.RemoveAction("4+", 0);

            _control.InsertCustomAction("Spit L", ()=> {
                GameObject zoteling = Instantiate(PantheonOfRegions.GameObjects["zoteling"]);
                zoteling.LocateMyFSM("Control").AddCustomAction("Reset", () => { Destroy(zoteling); });
                _control.Fsm.GetFsmGameObject("Zoteling").Value = zoteling;
                zoteling.SetActive(true);
            }, 0);
            _control.InsertCustomAction("Spit R", () => {
                GameObject zoteling = Instantiate(PantheonOfRegions.GameObjects["zoteling"]);
                zoteling.LocateMyFSM("Control").AddCustomAction("Reset", () => { Destroy(zoteling); });
                _control.Fsm.GetFsmGameObject("Zoteling").Value = zoteling;
                zoteling.SetActive(true);
            }, 0);


            
            _control.Fsm.GetFsmFloat("Left X").Value = 72f;
            _control.Fsm.GetFsmFloat("Right X").Value = 101f;
            _control.RemoveAction("Enter 2", 0);
            _control.GetAction<Wait>("Enter 2").time = 3f;
            _control.RemoveAction("Roar", 7);
            _control.RemoveAction("Roar", 5);
            _control.AddAction("Roar", new Wait()
            {
                time = new(4f),
                finishEvent = FsmEvent.GetFsmEvent("ZOTE TITLE END")
            });

            
            //_control.GetState("Send Event").AddMethod(() => NKG.LocateMyFSM("Control").SendEvent("STUN"));

            _constrainX.Fsm.GetFsmFloat("Edge L").Value = 70f;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = 103f;


        }

    }
}
