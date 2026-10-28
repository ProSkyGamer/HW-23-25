#region

using Controllers;
using Health;
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
        [SerializeField] private HealthComponent _healthComponent;
        [Range(0f, 1f)] [SerializeField] private float _healthPercentageToHitAnimations;

        private void Start()
        {
            _healthComponent.ValueChanged += HealthComponentOnValueChanged;
            _healthComponent.Dead += HealthComponent_OnDead;
        }

        private void HealthComponentOnValueChanged(float valueChange)
        {
            if (valueChange < 0)
                _animator.SetTrigger(_hitKey);

            float healthPercentage = _healthComponent.Current / _healthComponent.Max;
            bool isHurtAnimations = healthPercentage <= _healthPercentageToHitAnimations;
            _animator.SetBool(_hurtKey, isHurtAnimations);
        }

        private void HealthComponent_OnDead()
        {
            _animator.SetTrigger(_deadKey);
            Debug.Log("Dead");
        }

        private void Update()
        {
            _animator.SetBool(_runningKey, _agent.IsRunning);
        }
    }
}