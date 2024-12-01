using Vasi;
using HutongGames.PlayMaker.Actions;
using Random = UnityEngine.Random;
//Current bug: Boss randomly teleports to y=42f
namespace PantheonOfRegions.Behaviours
{
    internal class Gorb : MonoBehaviour
    {
        private PlayMakerFSM _movement;
        
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            Destroy(gameObject.LocateMyFSM("Distance Attack"));

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            Destroy(gameObject.LocateMyFSM("Broadcast Ghost Death"));

            _movement.SetState(_movement.Fsm.StartState);

            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector2($"P{index}").Value = RandomVector2();
            }

            _movement.GetAction<FloatCompare>("Hover", 4).float2 = 30f;
            _movement.GetAction<FloatCompare>("Hover", 5).float2 = 65f;
            _movement.GetAction<FloatCompare>("Hover", 6).float2 = 12f;
            _movement.GetAction<FaceObject>("Hover").objectB = HeroController.instance.gameObject;
            
            _movement.GetAction<FloatTestToBool>("Set Warp", 2).float2 = 48f;
            _movement.GetAction<FloatTestToBool>("Set Warp", 3).float2 = 48f;

            _movement.GetAction<SetPosition>("Return").x = 48f;
            _movement.GetAction<SetPosition>("Return").y = 20f;
        }

        private Vector2 RandomVector2()
        {
            float x = Random.Range(30f, 65f);
            float y = Random.Range(15f, 22f);

            return new Vector2(x, y);
        }
    }
}
