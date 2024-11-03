using Vasi;
using HutongGames.PlayMaker.Actions;
using Random = UnityEngine.Random;

namespace PantheonOfRegions.Behaviours
{
    internal class Xero : MonoBehaviour
    {
        private PlayMakerFSM _movement;
        private PlayMakerFSM _yLimit;
        private PlayMakerFSM _attack;
        private PlayMakerFSM _summon;
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            _yLimit = gameObject.LocateMyFSM("Y Limit");
            _attack = gameObject.LocateMyFSM("Attacking");
            _summon = gameObject.LocateMyFSM("Sword Summon");
            //ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse").LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            for (int i = 1; i <= 2; i++)
            {
                GameObject sword = gameObject.Find("Sword " + i );
                if (sword != null ) { 
                    _attack.Fsm.GetFsmGameObject("Sword " + i).Value = sword;
                    Modding.Logger.Log("Xero Edited 1/4");
                    sword.transform.parent = this.transform;
                    Modding.Logger.Log("Xero Edited 2/4");
                }
                else
                {
                    Modding.Logger.Log("Sword " + i + " not found in the scene.");
                }

            }
            _attack.GetState("Summon Antic").AddCustomAction(() =>
            {
                for (int i = 3; i <= 4; i++)
                {
                    GameObject sword = gameObject.Find("Sword " + i);
                    if (sword != null)
                    {
                        _attack.Fsm.GetFsmGameObject("Sword " + i).Value = sword;
                        Modding.Logger.Log("Xero Edited 3/4");
                        sword.transform.parent = this.transform;
                        Modding.Logger.Log("Xero Edited 4/4");
                    }
                    else
                    {
                        Modding.Logger.Log("Sword " + i + " not found in the scene.");
                    }

                }
            });


                for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            _attack.ChangeTransition("Wait", "FINISHED", "Antic");
            _attack.ChangeTransition("Wait Rage", "FINISHED", "Antic");
            //_attack.GetState("Init").AddCustomAction(() => { _attack.SendEvent("READY"); });

            _yLimit.GetAction<FloatClamp>("Limit").maxValue = 23f;
            _movement.GetAction<FloatTestToBool>("Hover", 4).float2 = 20f;
            _movement.GetAction<FloatTestToBool>("Hover", 5).float2 = 40f;
            _movement.GetAction<FloatCompare>("Set Warp", 2).float2 = 30f;
            _movement.GetAction<SetPosition>("Return").x = 28f;
            _movement.GetAction<SetPosition>("Return").y = 20f;

            _summon.ChangeTransition("Init","FINISHED","Summon");

            Modding.Logger.Log("Xero Edited full");
        }
        
        private Vector3 RandomVector3()
        {
            float x = Random.Range(20f, 40f);
            float y = Random.Range(15f, 23f);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }
}
