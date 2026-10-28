#region

using Movement;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Controllers
{
    public class AgentCharacter : MonoBehaviour
    {
        private const float MinDirectionMagnitude = 0.05f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _jumpHeight = 3f;

        private AgentMover _mover;
        private AgentJumper _jumper;
        private DirectionalRotator _rotator;

        private float _moveSpeed;
        private float _rotateSpeed;

        public bool IsRunning => _mover.IsRunning;

        public void Initialize(float moveSpeed, float rotateSpeed)
        {
            _moveSpeed = moveSpeed;
            _rotateSpeed = rotateSpeed;

            _agent.updateRotation = false;
            _agent.autoTraverseOffMeshLink = false;

            _mover = new AgentMover(_agent, _moveSpeed);
            _jumper = new AgentJumper(_agent, this, moveSpeed, _jumpCurve, _jumpHeight);
            _rotator = new TransformDirectionalRotator(transform, _rotateSpeed);
        }

        public void Stop()
        {
            _mover.Stop();
        }

        public void Continue()
        {
            _mover.Continue();
        }

        public void SetDestination(Vector3 destination)
        {
            _mover.SetDestination(destination);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;
            _rotator.Update(deltaTime);

            if (_agent.isOnOffMeshLink)
            {
                OffMeshLinkData offMeshLinkData = _agent.currentOffMeshLinkData;

                if (_jumper.IsJumpInProgress == false)
                    _jumper.Jump(offMeshLinkData);

                _rotator.SetDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);

                return;
            }

            if (_mover.Direction.magnitude >= MinDirectionMagnitude)
                _rotator.SetDirection(_mover.Direction);
        }
    }
}