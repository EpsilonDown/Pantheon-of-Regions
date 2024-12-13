using HutongGames.PlayMaker.Actions;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    class AbsoluteRadiance : MonoBehaviour
    {
        private PlayMakerFSM _commands;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _commands = gameObject.LocateMyFSM("Attack Commands");
            _control = gameObject.LocateMyFSM("Control");
        }

        private void Start()
        {
        }
    }
}
