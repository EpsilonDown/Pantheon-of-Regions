using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class Flukemarm : MonoBehaviour
    {
        private PlayMakerFSM _mother;
        
        private void Awake()
        {
            _mother = gameObject.LocateMyFSM("Fluke Mother");
        }

        private void Start()
        {
            GameObject hatcherCage = GameObject.Find("Hatcher Cage (2)(Clone)")
            //GameObject hatcherCage = Instantiate(PantheonOfRegions.GameObjects["hatchercage"], transform.position, Quaternion.identity);
            hatcherCage.SetActive(true);
            foreach (var collider in hatcherCage.GetComponents<BoxCollider2D>())
            {
                Destroy(collider);
            }
            _mother.Fsm.GetFsmGameObject("Cage").Value = hatcherCage;

        }
    }
}
