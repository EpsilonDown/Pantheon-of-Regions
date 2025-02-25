using Vasi;
using HutongGames.PlayMaker.Actions;
using Random = UnityEngine.Random;
using Osmi.Game;

namespace PantheonOfRegions.Behaviours
{
    internal class Gorb : MonoBehaviour
    {
        private PlayMakerFSM _movement;
        private PlayMakerFSM _attack;
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            _attack = gameObject.LocateMyFSM("Attacking");

            Destroy(gameObject.LocateMyFSM("FSM"));
            Destroy(gameObject.LocateMyFSM("Distance Attack"));
            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            _attack.RemoveAction("Init 2", 1);
            _attack.Fsm.GetFsmInt("HP").Value = 1800;
            _attack.RemoveAction("Double?", 0);
            _attack.InsertCustomAction("Double?", () => { 
               _attack.Fsm.GetFsmInt("HP").Value = GameObject.Find("Howlers").GetComponent<SharedHealthManager>().HP;
            }, 0);
            _attack.RemoveAction("Triple?", 0);
            _attack.InsertCustomAction("Triple?", () => {
                _attack.Fsm.GetFsmInt("HP").Value = GameObject.Find("Howlers").GetComponent<SharedHealthManager>().HP;
            }, 0);

            Destroy(gameObject.LocateMyFSM("Broadcast Ghost Death"));
            _movement.SetState(_movement.Fsm.StartState);
            

            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            _movement.GetAction<FloatCompare>("Hover", 4).float2 = 30f;
            _movement.GetAction<FloatCompare>("Hover", 5).float2 = 65f;
            _movement.GetAction<FloatCompare>("Hover", 6).float2 = 15f;
            _movement.GetAction<FaceObject>("Hover").objectB = HeroController.instance.gameObject;


            _movement.GetAction<FloatTestToBool>("Set Warp", 2).float2 = 48f;
            _movement.GetAction<FloatTestToBool>("Set Warp", 3).float2 = 48f;

            _movement.GetAction<SetPosition>("Return").x = 48f;
            _movement.GetAction<SetPosition>("Return").y = 18f;
        }

        private Vector2 RandomVector3()
        {
            float x = Random.Range(30f, 65f);
            float y = Random.Range(15f, 20f);

            return new Vector3(x, y, 0.006f);
        }
    }
}
