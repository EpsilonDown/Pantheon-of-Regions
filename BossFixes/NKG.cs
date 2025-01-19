using HutongGames.PlayMaker.Actions;
using Osmi.Game;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class NightmareKingGrimm : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private GameObject zote;
        private PlayMakerFSM zote_control;
        private int sharedhp;
        private SharedHealthManager hpsharer;
        private int ragecount = 0;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            zote = GameObject.Find("Grey Prince(Clone)(Clone)");
            zote_control = zote.LocateMyFSM("Control");
        }

        private void Start()
        {
            hpsharer = GameObject.Find("nightmares").GetComponent<SharedHealthManager>();

            _control.GetState("Explode").AddMethod(() => zote.LocateMyFSM("Control").SendEvent("STUN"));

            _control.RemoveAction("Set Balloon HP", 0);
            _control.RemoveAction("Balloon?", 0);
            _control.RemoveTransition("Balloon?", "FINISHED");
            _control.RemoveAction("Adjust HP", 4);

            _control.InsertCustomAction("Tele Out",() => {
                GameObject.Find("Grey Prince(Clone)(Clone)").GetComponent<GreyPrinceZote>().LeapEnabler();
            }, 0);
            _control.InsertCustomAction("Spike Return", () => {
                GameObject.Find("Grey Prince(Clone)(Clone)").GetComponent<GreyPrinceZote>().LeapBlocker();
            }, 0);

        }
        private void Update()
        {
            sharedhp = hpsharer.HP;
            if (_control.ActiveStateName == "Balloon?")
            {
                if (sharedhp < 1600 && ragecount == 0)
                {
                    zote_control.SetState("Longfall");
                    _control.SendEvent("BALLOON 1");
                    ragecount++;
                    
                }
                else if (sharedhp < 1000 && ragecount == 1)
                {
                    zote_control.SetState("Longfall");
                    _control.SendEvent("BALLOON 1");
                    ragecount++;
                    
                }
                else if (sharedhp < 500 && ragecount == 2)
                {
                    zote_control.SetState("Longfall");
                    _control.SendEvent("BALLOON 1");
                    ragecount++;
                    
                }
                else
                {
                    _control.SetState("Move Choice");
                }
            }
            else if (_control.ActiveStateName == "Deflate")
            {
            }
        }
    }

}