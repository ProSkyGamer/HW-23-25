#region

#endregion

namespace Controllers
{
    public class JumpingController : Controller
    {
        private readonly IJumper _jumper;

        public JumpingController(IJumper jumper)
        {
            _jumper = jumper;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_jumper.IsOnNavMeshLink)
                if (_jumper.IsJumping == false)
                    _jumper.Jump();
        }
    }
}