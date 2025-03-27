using HutongGames.PlayMaker.Actions;
using Vasi;
using Random = UnityEngine.Random;
namespace PantheonOfRegions.Behaviours
{
    internal class Markoth : MonoBehaviour
    {
        private List<GameObject> _nails = new();

        private PlayMakerFSM _movement;
        private PlayMakerFSM _shield;
        private PlayMakerFSM _nail;
        private PlayMakerFSM _attack;
        
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            _shield = gameObject.LocateMyFSM("Shield Attack");
            _attack = gameObject.LocateMyFSM("Attacking");

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            //remove shield on death
            #region fixes
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

            FsmState nailState = _attack.GetState("Nail");
            nailState.RemoveAction<SpawnObjectFromGlobalPool>();

            nailState.AddMethod(() => {
                var nail = Instantiate(markothNail, RandomVector3(), Quaternion.identity);
                nail.AddComponent<MarkothNail>();
                nail.SetActive(true);
                
                _nails.Add(nail);
            });
            #endregion


            _shield.SetState("Ready");
            _shield.GetState("Send Attack").RemoveAction(0);
            _shield.GetState("Idle").RemoveAction(1);
            _shield.GetState("Idle").RemoveTransition("FINISHED");
            FsmState nailFan = _attack.AddState("Nail Fan");
            nailFan.AddTransition("SHIELD END", "Wait");
            FsmState reflector = _attack.AddState("Reflector");
            reflector.AddTransition("ATTACK END", "Wait");


            nailFan.AddCustomAction(() => {
                //int offset = 10; //Random.Range(0, 10);
                for (int i = 0; i < 8; i++)
                {
                    int angle = 45*i; //+ offset;
                    var nail = Instantiate(markothNail,
                        gameObject.transform.position + new Vector3(4*Mathf.Cos(angle), 4*Mathf.Sin(angle), 0f), 
                        Quaternion.Euler(new Vector3 (0, 0, angle + 45f)));
                    DontDestroyOnLoad(nail);
                    nail.AddComponent<MarkothNail2>();
                    nail.SetActive(true);
                    
                    _nails.Add(nail);
                }
            });

            
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(49f, 72f);
            float y = Random.Range(28f, 32f);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }


    internal class MarkothNail : MonoBehaviour
    {
        private float ymax = 32f;
        private float ymin = 21f;
        private void Start()
        {
            
            PlayMakerFSM nailCtrl = gameObject.LocateMyFSM("Control");
            nailCtrl.GetAction<RandomFloat>("Set Pos", 0).max = 72f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 0).min = 49f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 1).max = nailCtrl.Fsm.GetFsmFloat("HeroY").Value + 10f;
            nailCtrl.GetAction<RandomFloat>("Set Pos", 1).min = nailCtrl.Fsm.GetFsmFloat("HeroY").Value - 4f;
            FsmState checkDistance = nailCtrl.GetState("Check Distance");
            checkDistance.InsertMethod(0, () => nailCtrl.Fsm.GetFsmVector3("Tele Pos").Value = transform.position);
            checkDistance.GetAction<FloatCompare>(2).float2 = 2;
            checkDistance.GetAction<FloatCompare>(3).float2 = Mathf.Infinity;
            nailCtrl.GetState("Recycle").InsertMethod(0, () => Destroy(gameObject));
            nailCtrl.SetState(nailCtrl.Fsm.StartState);
            nailCtrl.enabled = true;
        }
    }
    internal class MarkothNail2 : MonoBehaviour
    {

        private void Start()
        {
            PlayMakerFSM nailCtrl = gameObject.LocateMyFSM("Control");

            nailCtrl.ChangeTransition("Init","FINISHED","Antic");
            nailCtrl.GetState("Antic").GetAction<Wait>(1).time = 1.5f;
            nailCtrl.GetState("Recycle").InsertMethod(0, () => Destroy(gameObject));
            nailCtrl.SetState(nailCtrl.Fsm.StartState);
            nailCtrl.enabled = true;
        }
    }
}
