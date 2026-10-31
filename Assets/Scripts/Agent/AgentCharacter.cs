#region

using System;
using DefaultNamespace.Health;
using Movement;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Controllers
{
    public class AgentCharacter : MonoBehaviour, IPositionMover, IHaveHealth, ITakeDamage, ICanHeal, IJumper
    {
        public event Action<float, float> HealthChanged;
        public event Action HealthExhausted;

        private const float MinDirectionMagnitude = 0.05f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _jumpHeight = 3f;

        private AgentMover _mover;
        private DirectionalRotator _rotator;
        private AgentJumper _jumper;

        private float _moveSpeed;
        private float _rotateSpeed;

        public bool IsMoving => _mover.IsRunning;
        public bool IsOnNavMeshLink => _agent.isOnOffMeshLink;
        public bool IsJumping => _jumper.IsJumpInProgress;
        public Vector3 Position => _agent.transform.position;

        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }

        public void Initialize(float moveSpeed, float rotateSpeed, float maxHealth)
        {
            _moveSpeed = moveSpeed;
            _rotateSpeed = rotateSpeed;

            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;

            _agent.updateRotation = false;
            _agent.autoTraverseOffMeshLink = false;

            _mover = new AgentMover(_agent, _moveSpeed);
            _jumper = new AgentJumper(_agent, this, moveSpeed, _jumpCurve, _jumpHeight);
            _rotator = new TransformDirectionalRotator(transform, _rotateSpeed);
        }

        public void Continue()
        {
            _mover.Continue();
        }

        public void Stop()
        {
            _mover.Stop();
        }

        public void SetDestination(Vector3 destination)
        {
            _mover.SetDestination(destination);
        }

        public void Jump()
        {
            if (_agent.isOnOffMeshLink == false)
                return;

            OffMeshLinkData offMeshLinkData = _agent.currentOffMeshLinkData;
            _jumper.Jump(offMeshLinkData);

            _rotator.SetDirection((offMeshLinkData.endPos - offMeshLinkData.startPos).normalized);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (IsJumping)
                return;

            _rotator.Update(deltaTime);

            if (_mover.Direction.magnitude >= MinDirectionMagnitude)
                _rotator.SetDirection(_mover.Direction);
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0f)
                throw new ArgumentOutOfRangeException($"damage cannot be less than 0!");

            float previousHealth = CurrentHealth;
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);

            if (CurrentHealth <= 0f)
                Die();
            else
                HealthChanged?.Invoke(previousHealth, CurrentHealth);
        }

        private void Die()
        {
            Stop();

            HealthExhausted?.Invoke();
        }

        public void Heal(float heal)
        {
            if (heal <= 0f)
                throw new ArgumentOutOfRangeException($"heal cannot be less than 0!");

            float previousHealth = CurrentHealth;
            CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0f, MaxHealth);

            HealthChanged?.Invoke(previousHealth, CurrentHealth);
        }
    }
}