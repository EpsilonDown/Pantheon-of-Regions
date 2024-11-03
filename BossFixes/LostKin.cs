using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class LostKin : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _spawn;
        public static tk2dSprite? lostkinSprite = null;
        private static Texture2D? lostkinTexOrig = null;
        private static readonly Lazy<Texture2D> lostkinTex = new(() => AssemblyUtils.GetTextureFromResources("VoidKin.png"));
        private GameObject PureVessel = GameObject.Find("Battle Scene/HK Prime");
        private void ApplyTextureToTk2dSprite(tk2dSprite sprite, Texture2D texture)
                {
                // Create a new material with the loaded texture
                Material newMaterial = new Material(Shader.Find("tk2d/BlendVertexColor"))
                {
                    mainTexture = texture
                };

                // Apply the material to the tk2dSprite
                sprite.GetComponent<Renderer>().material = newMaterial;
                sprite.ForceBuild();
            }

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("IK Control");
            _spawn = gameObject.LocateMyFSM("Spawn Balloon");
            
        }

        private void Start()
        {
            //lostkinSprite!.CurrentSprite.material.mainTexture = lostkinTexOrig;

            lostkinSprite = this.GetComponent<tk2dSprite>();
            lostkinSprite!.CurrentSprite.material.mainTexture = lostkinTex.Value;
            //lostkinTexOrig = lostkinSprite.CurrentSprite.material.mainTexture as Texture2D;
            ApplyTextureToTk2dSprite(lostkinSprite, lostkinTex.Value);

            UObject.DestroyImmediate(this.GetComponent<EnemyDeathEffects>());
            UObject.DestroyImmediate(this.GetComponent<InfectedEnemyEffects>());
            ReflectionHelper.SetField(this.GetComponent<HealthManager>(), "preventInvincibleEffect", true);



            _control.Fsm.GetFsmFloat("Air Dash Height").Value = 6 + 3;
            _control.Fsm.GetFsmFloat("Left X").Value = 29;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = 6 + 5;
            _control.Fsm.GetFsmFloat("Right X").Value = 61;
            _control.Fsm.GetFsmFloat("Right X").Value = 61;
            _control.Fsm.GetFsmBool("Rewake Range").Value = true;

            _control.GetAction<RandomFloat>("Aim Jump 2").min = 45 - 1;
            _control.GetAction<RandomFloat>("Aim Jump 2").max = 45 + 1;
            _control.GetAction<SetPosition>("Intro Fall").x = transform.position.x;
            _control.GetAction<SetPosition>("Intro Fall").y = transform.position.y;
            _control.GetAction<SetPosition>("Set X", 0).x = transform.position.x;
            _control.GetAction<SetPosition>("Set X", 2).x = transform.position.x;
            _control.GetAction<SetDamageHeroAmount>("Roar End", 3).damageDealt = 2;
            _control.RemoveAction("Roar", 9);

            _control.RemoveAction("Roar", 9);
            _control.RemoveAction("Roar", 8);
            _control.RemoveAction("Roar", 7);
            _control.RemoveAction("Roar", 1);

            _spawn.Fsm.GetFsmFloat("X Min").Value = 29 + 1;
            _spawn.Fsm.GetFsmFloat("X Max").Value = 61 - 1;
            _spawn.Fsm.GetFsmFloat("Y Min").Value = 6 + 1;
            _spawn.Fsm.GetFsmFloat("Y Max").Value = 6 + 5;
            _spawn.RemoveAction("Spawn", 8);
            _spawn.RemoveAction("Spawn", 3);
            _spawn.InsertCustomAction("Spawn", () =>
            {
                GameObject shade = Instantiate(PantheonOfRegions.GameObjects["sibling"], _spawn.Fsm.GetFsmVector3("Spawn Vector").Value, Quaternion.identity);
                GameObject.DontDestroyOnLoad(shade);
                shade.AddComponent<EnemyTracker>();
                shade.SetActive(true);
                Destroy(shade.transform.GetChild(6).gameObject);
                _spawn.Fsm.GetFsmGameObject("Spawned Enemy").Value = shade;
            }, 6);

        }
    }
}
