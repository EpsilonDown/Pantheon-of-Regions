using Vasi;
using HutongGames.PlayMaker.Actions;
using Osmi.Game;
using UnityEngine;

namespace PantheonOfRegions.Behaviours
{
    internal class Oblobble : MonoBehaviour
    {
        private PlayMakerFSM _attack;
        private PlayMakerFSM _bounce;
        private PlayMakerFSM _rage;
        private int sharedhp;
        private GameObject healthsharer;
        private bool rage = false;
        private void Awake()
        {
            _attack = gameObject.LocateMyFSM("Fatty Fly Attack");
            _bounce = gameObject.LocateMyFSM("fat fly bounce");
            _rage = gameObject.LocateMyFSM("Set Rage");
        }    
        
        private void Start()
        {
            healthsharer = GameObject.Find("colosseum champions");
        }
        private void Update()
        {
            sharedhp = healthsharer.GetComponent<SharedHealthManager>().HP;
            if (sharedhp < 400 && rage == false)
            {
                
                GameObject rageblobble = Instantiate(PantheonOfRegions.GameObjects["oblobble"], new Vector2(110.0f, 10.0f), Quaternion.identity);
                GameObject.DontDestroyOnLoad(rageblobble);
                rageblobble.AddToShared(GameObject.Find("colosseum champions").GetComponent<SharedHealthManager>());
                rageblobble.SetActive(true);
                rageblobble.LocateMyFSM("Set Rage").SendEvent("OBLOBBLE RAGE");
                rage = true;
                Destroy(gameObject);
            }
        }
    }
}
