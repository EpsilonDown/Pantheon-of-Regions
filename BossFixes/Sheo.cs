using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    public class Sheo : MonoBehaviour
    {
        private PlayMakerFSM _sheoControl;
        private PlayMakerFSM _corpseControl;
        private PlayMakerFSM _stunControl;

        private void Awake()
        {
            _sheoControl = gameObject.LocateMyFSM("nailmaster_sheo");
            //_corpseControl = gameObject.transform.Find("Corpse Sheo(Clone)(Clone)").gameObject.LocateMyFSM("Death Land");
            _stunControl = gameObject.LocateMyFSM("Stun Control");
        }

        private IEnumerator Start()
        {
            yield return null;

            while (HeroController.instance == null) yield return null;


            _sheoControl.RemoveTransition("Painting", "FINISHED");
            Modding.Logger.Log("sheo Edited 2/3");
            _sheoControl.GetAction<Wait>("Look").time.Value = 1.25f;
            Destroy(_stunControl);
            _sheoControl.RemoveAction("Roar", 8);
            _sheoControl.RemoveAction("Roar", 7);
            _sheoControl.RemoveAction("Roar", 6);
            _sheoControl.RemoveAction("Roar", 5);
            _sheoControl.RemoveAction("Roar", 4);
            Modding.Logger.Log("sheo Edited 3/3");
        }
    }
}