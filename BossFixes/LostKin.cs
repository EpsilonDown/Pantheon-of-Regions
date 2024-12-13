using Vasi;
using HutongGames.PlayMaker.Actions;
using System.Reflection;
using UnityEngine.Tilemaps;
using Satchel;

namespace PantheonOfRegions.Behaviours
{
    internal class LostKin : MonoBehaviour
    {
        private bool init = true;
        private PlayMakerFSM _control;
        private PlayMakerFSM _spawn;
        private tk2dSprite? lostkinSprite = null;
        private Sprite headglobSprite = null;
        private GameObject child = null;
        private static readonly Lazy<Texture2D> lostkinTex = new(() => AssemblyUtils.GetTextureFromResources("VoidKin.png"));
        private static readonly Texture2D headglobTex = AssemblyUtils.GetTextureFromResources("void_glob.png");

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
            headglobSprite = Sprite.Create(headglobTex, new Rect(0, 0, 109, 110), new Vector2(0.545f, 0.55f), 64);

        }

        private void GlobApplyTexture(GameObject projectile)
        {
            var renderer = projectile.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
            renderer.sprite = headglobSprite;
            Destroy(projectile.transform.FindChild("Gas Attack").gameObject);
        }
        private void Start()
        {
            //lostkinSprite!.CurrentSprite.material.mainTexture = lostkinTexOrig;
            //lostkinTexOrig = lostkinSprite.CurrentSprite.material.mainTexture as Texture2D;
            


            /*
            GameObject globalpool = GameObject.Find("_GameManager/GlobalPool");
            Modding.Logger.Log("LK Edited 1");

            
            Modding.Logger.Log("LK Edited 2");

            int children = globalpool.transform.childCount;

            Modding.Logger.Log("LK Edited 3");

            for (int i = 0; i < children; ++i)
                child = globalpool.transform.GetChild(i).gameObject;
                Modding.Logger.Log("LK Edited 4");
                if (child.name == "IK Projectile DS(Clone)")
                {
                    Modding.Logger.Log("LK Edited 5");
                    child.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = headglobSprite;
                    Modding.Logger.Log("LK Edited 6");
                    Destroy(child.transform.GetChild(0).gameObject);
                    Modding.Logger.Log("LK Edited 7");
                }
                */
                

            //GameObject glob = _control.GetAction<SpawnObjectFromGlobalPool>("Dstab Land", 5).gameObject.Value;
            //headglobSprite = glob.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite;
            //Destroy(glob.transform.GetChild(0).gameObject);
          


            lostkinSprite = this.GetComponent<tk2dSprite>();
            lostkinSprite!.CurrentSprite.material.mainTexture = lostkinTex.Value;
            ApplyTextureToTk2dSprite(lostkinSprite, lostkinTex.Value);

            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 16);
            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 14);
            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 12);
            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 10);
            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 8);
            _control.InsertCustomAction("Dstab Land", () => {
                GlobApplyTexture(_control.Fsm.GetFsmGameObject("Projectile").Value);
            }, 6);
            _control.RemoveAction("Dstab Land", 2);

            _control.GetAction<Tk2dPlayAnimationWithEvents>("Intro Land").animationCompleteEvent = null;
            _control.GetState("Intro Land").AddAction(new Wait()
            {
                time = new(1f),
                finishEvent = FsmEvent.GetFsmEvent("FINISHED")
            });

            #region hit effect
            UObject.DestroyImmediate(gameObject.GetComponent<EnemyDeathEffects>());
            UObject.DestroyImmediate(gameObject.GetComponent<InfectedEnemyEffects>());
            ReflectionHelper.SetField(gameObject.GetComponent<HealthManager>(), "preventInvincibleEffect", true);

            EnemyHitEffectsUninfected hitEffect = gameObject.AddComponent<EnemyHitEffectsUninfected>();
            ReflectionHelper.SetField(gameObject.GetComponent<HealthManager>(), "hitEffectReceiver", hitEffect as IHitEffectReciever);

            EnemyHitEffectsUninfected voidEffect = GameObject.Find("Battle Scene/HK Prime")!.GetComponent<EnemyHitEffectsUninfected>();
            hitEffect.effectOrigin = voidEffect.effectOrigin;
            hitEffect.audioPlayerPrefab = voidEffect.audioPlayerPrefab;
            hitEffect.enemyDamage = voidEffect.enemyDamage;
            hitEffect.uninfectedHitPt = voidEffect.uninfectedHitPt;
            hitEffect.slashEffectGhost1 = voidEffect.slashEffectGhost1;
            hitEffect.slashEffectGhost2 = voidEffect.slashEffectGhost2;
            hitEffect.uninfectedHitPt = voidEffect.uninfectedHitPt;
            #endregion

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

            _control.RemoveAction("Roar", 8);
            _control.RemoveAction("Roar", 7);
            _control.RemoveAction("Roar", 6);
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
