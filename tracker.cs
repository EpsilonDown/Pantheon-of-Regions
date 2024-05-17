
using PantheonOfRegions.Behaviours;
namespace PantheonOfRegions
{
    internal class EnemyTracker : MonoBehaviour
    {
        private HealthManager _hm;
        private PlayMakerFSM _fsm;

        private void Awake()
        {
            _hm = GetComponent<HealthManager>();
            _fsm = GetComponent<PlayMakerFSM>();
        }

        private void Start()
        {
            string goName = gameObject.name;

            
            if (goName.Contains("Mawlek Body"))
            {
                gameObject.AddComponent<BroodingMawlek>();
            }
            else if (goName.Contains("Jar Collector"))
            {
                gameObject.AddComponent<TheCollector>();
            }
            else if (goName.Contains("Fluke Mother"))
            {
                gameObject.AddComponent<Flukemarm>();
            }
            else if (goName.Contains("Ghost Warrior Galien"))
            {
                gameObject.AddComponent<Galien>();
            }
            else if (goName.Contains("Ghost Warrior Slug"))
            {
                gameObject.AddComponent<Gorb>();
            }
            else if (goName.Contains("Ghost Warrior Hu"))
            {
                gameObject.AddComponent<ElderHu>();
            }
            else if (goName.Contains("Ghost Warrior Markoth"))
            {
                gameObject.AddComponent<Markoth>();
            }
            else if (goName.Contains("Ghost Warrior Marmu"))
            {
                gameObject.AddComponent<Marmu>();
            }
            else if (goName.Contains("Ghost Warrior No Eyes"))
            {
                gameObject.AddComponent<NoEyes>();
            }
            else if (goName.Contains("Ghost Warrior Xero"))
            {
                gameObject.AddComponent<Xero>();
            }
            else if (goName.Contains("Grey Prince"))
            {
                gameObject.AddComponent<GreyPrinceZote>();
            }
            else if (goName.Contains("Lobster"))
            {
                gameObject.AddComponent<Lobster>();
            }
            else if (goName.Contains("Hive Knight"))
            {
                gameObject.AddComponent<HiveKnight>();
            }
            else if (goName.Contains("Hornet Boss 1"))
            {
                gameObject.AddComponent<HornetProtector>();
            }
            else if (goName.Contains("Lost Kin"))
            {
                gameObject.AddComponent<LostKin>();
            }
            else if (goName.Contains("Mega Fat Bee"))
            {
                gameObject.AddComponent<Oblobble>();
            }
            else if (goName.Contains("Sheo Boss"))
            {
                gameObject.AddComponent<PaintmasterSheo>();
            }
            else if (goName.Contains("Sly Boss"))
            {
                gameObject.AddComponent<GreatNailsageSly>();
            }
            else if (goName.Contains("Mage Knight"))
            {
                gameObject.AddComponent<SoulWarrior>();
            }
            else
            {
                _fsm.SetState(_fsm.Fsm.StartState);
            }
        }
    }
}