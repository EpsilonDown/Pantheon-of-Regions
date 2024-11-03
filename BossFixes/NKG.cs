using HutongGames.PlayMaker.Actions;
using Vasi;

namespace PantheonOfRegions.Behaviours
{
    internal class NightmareKingGrimm : MonoBehaviour
    {
        
        //private GameObject _grimmBats;
        //private GameObject _spikeHolder;

        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _constrainY;
        private PlayMakerFSM _control;
        internal class ArenaInfo
        {
            public const float CenterX = 23f;
            public const float CenterY = 12f;
            public const float LeftX = 7f;
            public const float TopY = 20f;
            public const float RightX = 40f;
            public const float BottomY = 6f;
        }
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _constrainY = gameObject.LocateMyFSM("Constrain Y");
            _control = gameObject.LocateMyFSM("Control");

            #region Removables
            /*_spikeHolder = Instantiate(PantheonOfRegions.GameObjects["nightmaregrimmspikeholder"]);
            _spikeHolder.transform.SetPosition2D(10, 7 - 3);
            _spikeHolder.SetActive(true);

            _grimmBats = Instantiate(PantheonOfRegions.GameObjects["nightmaregrimmbats"]);
            _grimmBats.transform.SetPosition2D(20, 10);
            _grimmBats.SetActive(true);*/
            #endregion
        }

        private void Start()
        {
            #region Removables
            /*
            PlayMakerFSM batCtrl = _grimmBats.transform.Find("Real Bat").gameObject.LocateMyFSM("Control");
            batCtrl.Fsm.GetFsmGameObject("Grimm").Value = gameObject;
            batCtrl.GetAction<FloatCompare>("Face Middle").float2 = ArenaInfo.CenterX;
            batCtrl.GetAction<iTweenMoveTo>("Get To Middle").vectorPosition = new Vector3(ArenaInfo.CenterX, ArenaInfo.CenterY, 0);
            batCtrl.GetAction<FloatCompare>("Fly", 3).float2 = ArenaInfo.CenterX - 2;
            batCtrl.GetAction<FloatCompare>("Fly", 4).float2 = ArenaInfo.CenterX + 2;
            batCtrl.GetAction<FloatCompare>("Fly", 5).float2 = ArenaInfo.CenterY - 1;
            batCtrl.GetAction<FloatCompare>("Fly", 6).float2 = ArenaInfo.CenterY + 1;
            batCtrl.SetState(batCtrl.Fsm.StartState);
            batCtrl.SendEvent("BOSS AWAKE");

            foreach (var fakeBat in _grimmBats.transform.GetComponentsInChildren<FakeBat>(true))
            {
                ReflectionHelper.SetField(fakeBat, "grimm", transform);
            }

            _control.SetState("Init");

            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");

            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;

            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Mid Y").Value = ArenaInfo.CenterY;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 2;
            _control.GetAction<FloatCompare>("Balloon Check").float2 = ArenaInfo.BottomY + 3;
            _control.GetAction<SetPosition>("Balloon Pos").x = ArenaInfo.CenterX;

            _control.GetState("Explode").RemoveAction(3);
            _control.GetState("Explode").RemoveAction(4);
            _control.GetState("HUD Canvas OUT").RemoveAction<SendEventByName>();
            _control.GetState("Spike Attack").RemoveAction<SendEventToRegister>();

            _control.GetState("Death Start").InsertMethod(0, () =>
            {
                Destroy(_grimmBats);
                Destroy(_spikeHolder);
            });
                */
            #endregion

        }
    }
}