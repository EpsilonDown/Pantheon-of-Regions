namespace PantheonOfRegions.Behaviours
{
    internal class SoulWarrior : MonoBehaviour
    {
        private PlayMakerFSM _knight;

        private void Awake()
        {
            _knight = gameObject.LocateMyFSM("Mage Knight");
        }

        private IEnumerator Start()
        {
            _knight.Fsm.GetFsmFloat("Tele X Max").Value = 43f;
            _knight.Fsm.GetFsmFloat("Tele X Min").Value = 29f;
            
            _knight.SetState("Init");
            
            yield return new WaitWhile(() => _knight.ActiveStateName != "Sleep");

            _knight.SendEvent("WAKE");
            
            yield return new WaitWhile(() => _knight.ActiveStateName != "Wake");
            
            _knight.SetState("Idle");
        }
    }
}
