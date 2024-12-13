using HutongGames.PlayMaker.Actions;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class PureVessel : MonoBehaviour
    {
        private const float GroundY = 6.4f;
        
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private void Start()
        {
            _control.ChangeTransition("Phase?", "PHASE1", "Choice P3");
            _control.ChangeTransition("Phase?", "PHASE2", "Choice P3");
        }
    }
}