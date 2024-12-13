using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class Gruzmother : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _bounce;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Big Fly Control");
            _bounce = gameObject.LocateMyFSM("bouncer_control");
        }

        private void Start()
        {
            _control.GetAction<Wait>("GG Extra Pause", 0).time = 5f;
            _control.AddState("Pause");
            _control.AddTransition("Pause","FINISHED","Super Choose");
            _control.AddCustomAction("Pause",() => {_bounce.SendEvent("STOP");});
            _control.AddAction("Pause", new Wait()
            {
                time = new(4f),
                finishEvent = FsmEvent.GetFsmEvent("FINISHED")
            });

        }

    }
}
