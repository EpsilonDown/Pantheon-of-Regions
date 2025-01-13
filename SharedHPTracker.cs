using PantheonOfRegions.Behaviours;
namespace PantheonOfRegions
{
    public class SharedHPTracker : MonoBehaviour
    {
        public SharedHealthManager sharedhp;

        public void Start()
        {
            string goName = gameObject.name;


            if (goName.Contains("Nightmares"))
            {
                gameObject.AddComponent<Nightmares>();
            }
            else if (goName.Contains("Crossroads Dominators"))
            {
                gameObject.AddComponent<Crossroads Dominators>();
            }
        }
    }
    internal class Nightmares : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private GameObject zote;
        private PlayMakerFSM zote_control;
        private int sharedhp;
        private int ragecount = 0;
        private void Awake()
        {
            sharedhp = gameObject.GetComponent<SharedHealthManager>().HP;
        }

            

    }
}
