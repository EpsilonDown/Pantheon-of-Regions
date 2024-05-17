using HutongGames.PlayMaker.Actions;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class GreatNailsageSly : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _control.Fsm.GetFsmBool("Final Rage").Value = true;
            
            _control.GetAction<FloatCompare>("Cyc Down").float2.Value = 15f + 4f;
            _control.GetAction<FloatOperator>("Cyc Jump Launch").float1.Value = 20f;
            _control.GetAction<SetFloatValue>("Jump To L", 0).floatValue.Value = 24f - 8f;
            _control.GetAction<SetFloatValue>("Jump To L", 1).floatValue.Value = 15f;
            _control.GetAction<SetFloatValue>("Jump To R", 0).floatValue.Value = 15f + 8f;
            _control.GetAction<SetFloatValue>("Jump To R", 1).floatValue.Value = 24f;
            
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
        }

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
        }
    }
}
