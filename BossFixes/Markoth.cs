using HutongGames.PlayMaker.Actions;
using Vasi;
using Random = UnityEngine.Random;
namespace PantheonOfRegions.Behaviours
{
    internal class Markoth : MonoBehaviour
    {
        private List<GameObject> _nails = new();

        private PlayMakerFSM _movement;

        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            FsmState broadcastDeathSet = gameObject.LocateMyFSM("Broadcast Ghost Death").GetState("Set");
            broadcastDeathSet.RemoveAction<SendEventByName>();
            broadcastDeathSet.AddMethod(() => {
                foreach (GameObject markothNail in _nails)
                {
                    FSMUtility.SendEventToGameObject(markothNail, "GHOST DEATH");
                }

                GameObject markothShield = GameObject.Find("Markoth Shield(Clone)");
                FSMUtility.SendEventToGameObject(markothShield.transform.Find("Shield").gameObject, "GHOST DEATH");
                FSMUtility.SendEventToGameObject(markothShield.transform.Find("Shield 2").gameObject, "GHOST DEATH");
            });

            for (int index = 1; index <= 8; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            var personalObjectPool = GetComponent<PersonalObjectPool>();
            personalObjectPool.DestroyPooled();
            Destroy(personalObjectPool);
            GameObject markothNail = personalObjectPool.startupPool[0].prefab;
            markothNail.SetActive(false);
            markothNail.AddComponent<MarkothNail>();
            FsmState nailState = gameObject.LocateMyFSM("Attacking").GetState("Nail");
            nailState.RemoveAction<SpawnObjectFromGlobalPool>();
            nailState.AddMethod(() => {
                var nail = Instantiate(markothNail, RandomVector3(), Quaternion.identity);
                nail.SetActive(true);
                _nails.Add(nail);
            });                }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(49f, 72f);
            float y = Random.Range(21f, 32f);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }

    internal class MarkothNail : MonoBehaviour
    {
        private void Start()
        {
            PlayMakerFSM nailCtrl = gameObject.LocateMyFSM("Control");
            nailCtrl.GetAction<RandomFloat>("Set Pos", 0).max = 72f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 0).min = 49f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 1).max = 32f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 1).min = 21f;
            FsmState checkDistance = nailCtrl.GetState("Check Distance");
            checkDistance.InsertMethod(0, () => nailCtrl.Fsm.GetFsmVector3("Tele Pos").Value = transform.position);
            checkDistance.GetAction<FloatCompare>(2).float2 = 2;
            checkDistance.GetAction<FloatCompare>(3).float2 = Mathf.Infinity;
            nailCtrl.GetState("Recycle").InsertMethod(0, () => Destroy(gameObject));
            nailCtrl.SetState(nailCtrl.Fsm.StartState);
            nailCtrl.enabled = true;
        }
    }
}
