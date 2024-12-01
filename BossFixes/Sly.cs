using HutongGames.PlayMaker.Actions;
using Osmi.Game;
using Vasi;
//Rage Phase not working - disconnect HP first and see?
namespace PantheonOfRegions.Behaviours
{
    internal class GreatNailsageSly : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private GameObject stunnail;
        private GameObject deathnail;
        private GameObject wallspotl;
        private GameObject wallspotr;
        private int sharedhp;
        private bool end = false;
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            stunnail = Instantiate(PantheonOfRegions.GameObjects["stunnail"]);
            deathnail = Instantiate(PantheonOfRegions.GameObjects["deathnail"]);
            wallspotl = Instantiate(PantheonOfRegions.GameObjects["wallspotl"], new Vector2(86f, 15f), Quaternion.identity);
            wallspotr = Instantiate(PantheonOfRegions.GameObjects["wallspotr"], new Vector2(102f, 15f), Quaternion.identity);
        }

        private void Start()
        {
            _control.RemoveAction("Init", 34);
            _control.RemoveAction("Init", 33);
            _control.RemoveAction("Init", 32);
            _control.RemoveAction("Init", 31);
            _control.Fsm.GetFsmGameObject("Stun Nail").Value = stunnail;
            _control.Fsm.GetFsmGameObject("Death Nail").Value = deathnail;
            _control.Fsm.GetFsmGameObject("Wall Spot L").Value = wallspotl;
            _control.Fsm.GetFsmGameObject("Wall Spot R").Value = wallspotr;

            _control.Fsm.GetFsmBool("Final Rage").Value = true;

            _control.GetAction<FloatCompare>("Cyc Down").float2.Value = 19f;
            _control.GetAction<FloatOperator>("Cyc Jump Launch").float1.Value = 20f;
            _control.GetAction<SetFloatValue>("Jump To L", 0).floatValue.Value = 98f;
            _control.GetAction<SetFloatValue>("Jump To L", 1).floatValue.Value = 86f;
            _control.GetAction<SetFloatValue>("Jump To R", 0).floatValue.Value = 90f;
            _control.GetAction<SetFloatValue>("Jump To R", 1).floatValue.Value = 102f;
            _control.Fsm.GetFsmFloat("Topslash Y").Value = 24f;
            sharedhp = GameObject.Find("fly lords").GetComponent<SharedHealthManager>().HP;
            /*
            _control.GetState("Bow").AddCoroutine(TweenOut);
            _control.GetState("Stun Wait").AddMethod(() => _control.SendEvent("READY"));
            _control.GetState("Grabbing").AddMethod(() => _control.SendEvent("GRABBED"));
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Docile");

            GameObject spinTink = new GameObject("Spin Tink");
            var collider = spinTink.AddComponent<CircleCollider2D>();
            spinTink.AddComponent<DamageHero>();
            collider.isTrigger = true;
            collider.radius = 3;
            spinTink.transform.SetParent(gameObject.transform, false);
            _control.Fsm.GetFsmGameObject("Spin Tink").Value = spinTink;

            _control.SetState("Battle Start");
            */
        }
        /*
        private IEnumerator TweenOut()
        {
            
            yield return new WaitForSeconds(5);

            GetComponent<BoxCollider2D>().isTrigger = true;
            yield return new WaitUntil(() =>
            {
                transform.Translate(Vector3.down * 25 * Time.deltaTime);
                return transform.position.y <= 5f;
            });
            Destroy(gameObject);
            
        }*/
        private void Update()
        {
            
            if (sharedhp < 50 && end == false)
            {
                _control.SendEvent("ZERO HP");
                gameObject.GetComponent<HealthManager>().StopSharing(0);
                
                end = true;
            }
        }
    }
}
