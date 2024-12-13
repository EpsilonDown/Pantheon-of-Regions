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
        private int ragecount = 0;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            zote = GameObject.Find("Grey Prince(Clone)(Clone)");
            zote_control = zote.LocateMyFSM("Control");
        }

        private void Start()
        {
            
            _control.GetState("Explode").AddMethod(() => zote.LocateMyFSM("Control").SendEvent("STUN"));

            _control.RemoveAction("Set Balloon HP", 0);
            _control.RemoveAction("Balloon?", 0);
            _control.RemoveTransition("Balloon?", "FINISHED");
            _control.RemoveAction("Adjust HP", 4);

            //_control.Fsm.GetFsmInt("HP").Value = 2700;

            /* _control.InsertCustomAction("Balloon?",() => {
                _control.Fsm.GetFsmInt("HP").Value = sharedhp;
            }, 0); */

        }
        private void Update()
        {
            sharedhp = GameObject.Find("nightmares").GetComponent<SharedHealthManager>().HP;
            if (_control.ActiveStateName == "Balloon?")
            {
                if (sharedhp < 2000 && ragecount == 0)
                {
                    zote_control.GetAction<Wait>("FT Through", 5).time = 12f;
                    zote_control.SetState("FT Through");
                    _control.SendEvent("BALLOON 1");
                    ragecount++;
                    
                }
                else if (sharedhp < 1300 && ragecount == 1)
                {
                    zote_control.GetAction<Wait>("FT Through", 5).time = 12f;
                    zote_control.SetState("FT Through");
                    _control.SendEvent("BALLOON 1");
                    ragecount++;
                    
                }
                else if (sharedhp < 600 && ragecount == 2)
                {
                    zote_control.GetAction<Wait>("FT Through", 5).time = 12f;
                    zote_control.SetState("FT Through");
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
                zote_control.GetAction<Wait>("FT Through", 5).time = 1f;
            }
        }
    }

}