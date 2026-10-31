namespace Controllers
{
    public class CompositeDowntimeSwitchController : Controller
    {
        private readonly Controller _downTimeSwitchController;
        private readonly Controller _switchToController;
        private readonly float _switchModeTime;

        public CompositeDowntimeSwitchController(Controller downTimeSwitchController, Controller switchToController, float switchModeTime)
        {
            _downTimeSwitchController = downTimeSwitchController;
            _switchToController = switchToController;
            _switchModeTime = switchModeTime;
        }

        public override void Enable()
        {
            base.Enable();

            _downTimeSwitchController.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            _downTimeSwitchController.Disable();
            _switchToController.Disable();
        }

        protected override void UpdateInternal(float deltaTime)
        {
            _downTimeSwitchController.Update(deltaTime);
            _switchToController.Update(deltaTime);

            if (_downTimeSwitchController.Downtime >= _switchModeTime)
            {
                if (_switchToController.IsActive == false)
                    _switchToController.Enable();
            }
            else if (_switchToController.IsActive)
            {
                _switchToController.Disable();
            }
        }
    }
}