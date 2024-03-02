using HutongGames.PlayMaker.Actions;

namespace PantheonOfRegions;
/*
public virtual GhostWarriorHuControl(GameObject Hu, GameObject Ring) {
    PlayMakerFSM Movement = Hu.LocateMyFSM("Movement");
    PlayMakerFSM Attacking = Hu.LocateMyFSM("Attacking");
    public GameObject ringHolder = Ring;
    protected virtual void ConfigureRingPositions()
    {
        foreach (var fsm in ringHolder.GetComponentsInChildren<PlayMakerFSM>(true))
        {
            fsm.ChangeTransition("Init", "FINISHED", "Antic");
        }
    }

    protected virtual void SetRingPositions(Vector3 ringRootPos)
    {
        ringHolder.transform.position = ringRootPos;
        foreach (var fsm in ringHolder.GetComponentsInChildren<PlayMakerFSM>(true))
        {
            var downRay = new Vector2(30f, 20f);
            var down = fsm.GetState("Down");
            var fc = down.GetFirstActionOfType<FloatCompare>();
            fc.float2 = downRay[1] + .3f;

            var land = fsm.GetState("Land");
            var spos = land.GetFirstActionOfType<SetPosition>();
            spos.y = downRay[1];

            var reset = fsm.GetState("Reset");
            var spos2 = reset.GetFirstActionOfType<SetPosition>();
            spos2.y = downRay[1];
        }
    }

    IEnumerator chasing;
}
*/
