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

        private AgentMover _mover;
        private DirectionalRotator _rotator;

        private float _moveSpeed;
        private float _rotateSpeed;

        public bool IsRunning => _mover.IsRunning;

        public void Initialize(float moveSpeed, float rotateSpeed)
        {
            _moveSpeed = moveSpeed;
            _rotateSpeed = rotateSpeed;

            _agent.updateRotation = false;

            _mover = new AgentMover(_agent, _moveSpeed);
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
            if (_mover.Direction.magnitude >= MinDirectionMagnitude)
                _rotator.SetDirection(_mover.Direction);

            float deltaTime = Time.deltaTime;
            _rotator.Update(deltaTime);
        }
    }
}