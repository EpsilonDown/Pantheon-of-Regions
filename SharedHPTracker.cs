using PantheonOfRegions.Behaviours;
namespace PantheonOfRegions
{
    internal class SharedHPTracker : MonoBehaviour
    {
        private SharedHealthManager sharedhp;

        private void Awake()
        {
            sharedhp = GetComponent<SharedHealthManager>();
        }

        private void Start()
        {
            string goName = gameObject.name;


            if (goName.Contains("Cliff Howlers"))
            {
                sharedhp = 
            }
            else if (goName.Contains("Crossroads Dominators"))
            {

            }
        }
    }
}
