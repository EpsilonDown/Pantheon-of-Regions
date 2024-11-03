using HutongGames.PlayMaker.Actions;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class Watcherknight : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private void Start()
        {
            //_control.SetState("Init");

            _control.Fsm.GetFsmBool("Final Rage").Value = true;
            
            _control.GetAction<FloatCompare>("Cyc Down").float2.Value = 15f + 4f;
            _control.GetAction<FloatOperator>("Cyc Jump Launch").float1.Value = 20f;
            _control.GetAction<SetFloatValue>("Jump To L", 0).floatValue.Value = 24f - 8f;
            _control.GetAction<SetFloatValue>("Jump To L", 1).floatValue.Value = 15f;
            _control.GetAction<SetFloatValue>("Jump To R", 0).floatValue.Value = 15f + 8f;
            _control.GetAction<SetFloatValue>("Jump To R", 1).floatValue.Value = 24f;
            
            _control.GetState("Stun Wait").AddMethod(() => _control.SendEvent("READY"));
            _control.GetState("Grabbing").AddMethod(() => _control.SendEvent("GRABBED"));
           
        }

    }
}
