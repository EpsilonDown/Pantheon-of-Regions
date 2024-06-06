using Vasi;
using HutongGames.PlayMaker.Actions;
namespace PantheonOfRegions.Behaviours
{
    internal class LostKin : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _spawn;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("IK Control");
            _spawn = gameObject.LocateMyFSM("Spawn Balloon");
        }

        private IEnumerator Start()
        {
            //_control.SetState("Pause");

            _control.Fsm.GetFsmFloat("Air Dash Height").Value = 6 + 3;
            _control.Fsm.GetFsmFloat("Left X").Value = 29;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = 6 + 5;
            _control.Fsm.GetFsmFloat("Right X").Value = 61;

            _control.GetAction<RandomFloat>("Aim Jump 2").min = 45 - 1;
            _control.GetAction<RandomFloat>("Aim Jump 2").max = 45 + 1;
            _control.GetAction<SetPosition>("Intro Fall").x = transform.position.x;
            _control.GetAction<SetPosition>("Intro Fall").y = transform.position.y;
            _control.GetAction<SetPosition>("Set X", 0).x = transform.position.x;
            _control.GetAction<SetPosition>("Set X", 2).x = transform.position.x;

            _spawn.Fsm.GetFsmFloat("X Min").Value = 29 + 1;
            _spawn.Fsm.GetFsmFloat("X Max").Value = 61 - 1;
            _spawn.Fsm.GetFsmFloat("Y Min").Value = 6 + 1;
            _spawn.Fsm.GetFsmFloat("Y Max").Value = 6 + 5;

            yield return new WaitUntil(() => _control.ActiveStateName == "Intro Fall");

            //_control.SetState("Roar End");
        }
    }
}
