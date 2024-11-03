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
        private bool rage = false;
        private void Awake()
        {
            _attack = gameObject.LocateMyFSM("Fatty Fly Attack");
            _bounce = gameObject.LocateMyFSM("fat fly bounce");
            _rage = gameObject.LocateMyFSM("Set Rage");
        }    
        
        private void Start()
        {

        }
        private void Update()
        {
            sharedhp = GameObject.Find("colosseum champions").GetComponent<SharedHealthManager>().HP;
            if (sharedhp < 600 && rage == false)
            {
                
                GameObject rageblobble = Instantiate(PantheonOfRegions.GameObjects["oblobble"], new Vector2(110.0f, 10.0f), Quaternion.identity);
                GameObject.DontDestroyOnLoad(rageblobble);
                rageblobble.AddToShared(GameObject.Find("colosseum champions").GetComponent<SharedHealthManager>());
                rageblobble.SetActive(true);
                rage = true;
                Destroy(gameObject);
            }
        }
    }
}
