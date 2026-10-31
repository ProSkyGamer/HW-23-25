#region

using UnityEngine;

#endregion

namespace Controllers
{
    public class ClickPositionController : Controller
    {
        private const int SelectPointButtonIndex = 1;

        private readonly Camera _camera;
        private readonly LayerMask _layerMask;
        private readonly IPositionMover _positionMover;
        private readonly float _maxCastDistance = 200f;

        private bool IsFollowInputPressed => Input.GetMouseButtonDown(SelectPointButtonIndex);

        public ClickPositionController(IPositionMover positionMover, Camera camera, LayerMask layerMask)
        {
            _camera = camera;
            _layerMask = layerMask;
            _positionMover = positionMover;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (IsFollowInputPressed)
            {
                ResetDowntime();
                ProcessPlayerInput();
            }
        }

        private void ProcessPlayerInput()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxCastDistance, _layerMask))
            {
                Vector3 movePosition = hitInfo.point;

                _positionMover.SetDestination(movePosition);
            }
        }
    }
}