#region

using Controllers;
using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class PlayerAnimations : MonoBehaviour
    {
        private readonly int _runningKey = Animator.StringToHash("IsRunning");
        private readonly int _hurtKey = Animator.StringToHash("IsHurt");
        private readonly int _hitKey = Animator.StringToHash("Hit");
        private readonly int _deadKey = Animator.StringToHash("Dead");

        [SerializeField] private Animator _animator;
        [SerializeField] private AgentCharacter _agent;
        [Range(0f, 1f)] [SerializeField] private float _healthPercentageToHitAnimations;

        private void Start()
        {
            _agent.HealthChanged += OnAgentHealthChanged;
            _agent.HealthExhausted += OnAgentHealthExhausted;
        }

        private void OnAgentHealthChanged(float previousHealth, float newHealth)
        {
            if (newHealth < previousHealth)
                _animator.SetTrigger(_hitKey);

            float healthPercentage = _agent.CurrentHealth / _agent.MaxHealth;
            bool isHurtAnimations = healthPercentage <= _healthPercentageToHitAnimations;
            _animator.SetBool(_hurtKey, isHurtAnimations);
        }

        private void OnAgentHealthExhausted()
        {
            _animator.SetTrigger(_deadKey);
            Debug.Log("Dead");
        }

        private void Update()
        {
            _animator.SetBool(_runningKey, _agent.IsMoving);
        }

        private void OnDestroy()
        {
            _agent.HealthChanged -= OnAgentHealthChanged;
            _agent.HealthExhausted -= OnAgentHealthExhausted;
        }
    }
}