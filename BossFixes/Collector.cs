using Vasi;
using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions.Behaviours
{
    internal class TheCollector : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _death;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            _death = gameObject.LocateMyFSM("Death");
        }

        private IEnumerator Start()
        {
            _control.Fsm.GetFsmFloat("Bottle XL").Value = 41f;
            _control.Fsm.GetFsmFloat("Bottle XR").Value = 67f;
            _control.Fsm.GetFsmFloat("Roof X L").Value = 41f;
            _control.Fsm.GetFsmFloat("Roof X R").Value = 67f;
            _control.Fsm.GetFsmFloat("Roof Y").Value = 103f;
            _control.Fsm.GetFsmFloat("Return Y").Value = 103f;

            GameObject spawnJar = _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value;
            spawnJar.GetComponent<SpawnJarControl>().spawnY = 100f;
            spawnJar.GetComponent<SpawnJarControl>().breakY = 95f;
            spawnJar.CreatePool(3);
            _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value = spawnJar;
            _control.Fsm.GetFsmGameObject("Spawn Jar").Value = spawnJar;
            _control.GetAction<FloatCompare>("Spawn").tolerance = 1;

            _death.ChangeTransition("Start Effect", "FINISHED", "Kill Enemies");

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;

            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Sleep");

            _control.SetState("Roar Recover");
        }
    }
}
