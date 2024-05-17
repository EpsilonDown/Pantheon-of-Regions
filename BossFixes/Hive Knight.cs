using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class HiveKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            yield return new WaitWhile(() => _control.ActiveStateName != "Sleep");

            GetComponent<MeshRenderer>().enabled = true;

            _control.Fsm.GetFsmFloat("Left X").Value = 15f;
            _control.Fsm.GetFsmFloat("Right X").Value = 37f;
            _control.SetState("Activate");
        }
    }
}
