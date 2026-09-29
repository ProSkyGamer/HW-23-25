#region

using Controllers;
using Health;
using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private AgentCharacter _agent;
        [SerializeField] private Camera _camera;

        [SerializeField] private float _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _switchToRandomPointModeTime = 5f;
        [SerializeField] private float _randomPointRadius = 10f;

        private Controller _playerController;

        private void Awake()
        {
            _healthComponent.Initialize(_maxHealth);
            _agent.Initialize(_moveSpeed, _rotateSpeed);

            _playerController = new AgentController(_agent, _camera, _groundMask, _switchToRandomPointModeTime, _randomPointRadius);

            _playerController.Enable();
        }

        private void Start()
        {
            _healthComponent.Dead += HealthComponent_OnDead;
        }

        private void HealthComponent_OnDead()
        {
            _playerController.Disable();
            _agent.Stop();
        }

        private void Update()
        {
            _playerController.Update(Time.deltaTime);
        }
    }
}