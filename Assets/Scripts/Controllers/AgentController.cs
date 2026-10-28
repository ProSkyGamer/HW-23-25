#region

using UnityEngine;

#endregion

namespace Controllers
{
    public class AgentController : Controller
    {
        private const int SelectPointButtonIndex = 1;

        private readonly Camera _camera;
        private readonly LayerMask _layerMask;
        private readonly AgentCharacter _agentCharacter;
        private readonly float _maxCastDistance = 200f;

        private readonly float _switchModeTime;
        private float _switchModeTimer;
        private readonly float _searchRadius;

        private bool IsFollowInputPressed => Input.GetMouseButtonDown(SelectPointButtonIndex);

        public AgentController(AgentCharacter agentCharacter, Camera camera, LayerMask layerMask, float switchModeTime, float searchRadius)
        {
            _camera = camera;
            _layerMask = layerMask;
            _agentCharacter = agentCharacter;
            _switchModeTime = switchModeTime;
            _searchRadius = searchRadius;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (IsFollowInputPressed)
                ProcessPlayerInput();
            else if (_agentCharacter.IsRunning == false && _switchModeTimer < _switchModeTime)
                _switchModeTimer += deltaTime;
            else if (_agentCharacter.IsRunning == false && _switchModeTimer >= _switchModeTime)
                SetRandomDestination();
        }

        private void ProcessPlayerInput()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxCastDistance, _layerMask))
            {
                Vector3 movePosition = hitInfo.point;

                _agentCharacter.SetDestination(movePosition);
            }

            _switchModeTimer = 0f;
        }

        private void SetRandomDestination()
        {
            _agentCharacter.SetDestination(NavMeshHelper.GetRandomLocation(_agentCharacter.transform.position, _searchRadius));
        }
    }
}