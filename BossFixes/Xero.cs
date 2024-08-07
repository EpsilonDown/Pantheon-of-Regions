using Vasi;
using HutongGames.PlayMaker.Actions;
using Random = UnityEngine.Random;
namespace PantheonOfRegions.Behaviours
{
    internal class Xero : MonoBehaviour
    {
        private PlayMakerFSM _movement;
        private PlayMakerFSM _yLimit;

        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            _yLimit = gameObject.LocateMyFSM("Y Limit");

            ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse").LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            // for (int i = 1; i <= 4; i++)
            // {
            //     PlayMakerFSM xeroNail = transform.Find("Sword " + i).gameObject.LocateMyFSM("xero_nail");
            //     xeroNail.SetState(xeroNail.Fsm.StartState);
            // }
            
            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }
            
            _yLimit.GetAction<FloatClamp>("Limit").maxValue = 20f;
        }
        
        private Vector3 RandomVector3()
        {
            float x = Random.Range(20f, 40f);
            float y = Random.Range(10f, 20f);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }
}
