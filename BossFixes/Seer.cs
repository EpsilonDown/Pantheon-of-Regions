using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class Seer : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private tk2dSprite? seerSprite = null;
        private static readonly Lazy<Texture2D> seerTex = new(() => AssemblyUtils.GetTextureFromResources("Seers.png"));
        private void ApplyTextureToTk2dSprite(tk2dSprite sprite, Texture2D texture)
        {
            Material newMaterial = new Material(Shader.Find("tk2d/TransparentVertexColor"))
            {
                mainTexture = texture
            };
            // Apply the material to the tk2dSprite
            sprite.GetComponent<Renderer>().material = newMaterial;
            sprite.ForceBuild();
        }
        private void RemoveAllActions(FsmState state)
        {
            state.Actions = Array.Empty<FsmStateAction>();
        }
        private void Awake()
        {
            gameObject.GetComponent<PlayMakerFSM>().enabled = true;
            _control = gameObject.LocateMyFSM("Mage");
            Destroy(gameObject.GetComponent<HealthManager>());
            Destroy(gameObject.GetComponent<DamageHero>());
            

        }

        private IEnumerator Start()
        {
            _control.enabled = true;
            _control.SetState("Init");
            _control.GetState("Idle After Tele").RemoveAction<DistanceFlySmooth>();
            Modding.Logger.Log("Seer 2");
            _control.GetState("Init").RemoveAction<SetPosition>();
            _control.GetState("Init").RemoveAction(2);
            _control.GetState("Init").RemoveAction(1);
            Modding.Logger.Log("Seer 3");

            _control.GetState("Wake").RemoveAction(4);
            _control.GetState("Wake").RemoveAction(3);
            _control.GetState("Wake").RemoveAction(2);
            _control.GetState("Wake").RemoveAction(1);

            RemoveAllActions(_control.GetState("Summon 3"));
            Modding.Logger.Log("Seer 5");
            _control.Fsm.GetFsmBool("Firing Range").Value = true;
            _control.ChangeTransition("Select Target", "TELEPORT", "Idle or Summoned?");
            Modding.Logger.Log("Seer 6");

            yield return null;
            _control.SetState("Idle After Tele"); 

            
            

            seerSprite = gameObject.GetComponent<tk2dSprite>();
            seerSprite.enabled = true;
            //seerSprite!.SetSprite("Seers.png");
            seerSprite!.CurrentSprite.material.mainTexture = seerTex.Value;
            ApplyTextureToTk2dSprite(seerSprite, seerTex.Value);
            gameObject.transform.SetScaleX(2f);
            gameObject.transform.SetScaleY(2f);

        }
        private void Update()
        {
            gameObject.transform.SetScaleX(2f);
            gameObject.transform.SetScaleY(2f);
        }
    }
}
