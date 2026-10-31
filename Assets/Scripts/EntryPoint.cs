#region

using Controllers;
using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AgentCharacter _agent;
        [SerializeField] private Camera _camera;

        [SerializeField] private float _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _switchToRandomPointModeTime = 5f;
        [SerializeField] private float _randomPointRadius = 10f;

        [SerializeField] private float _medKitsSpawnInterval;
        [SerializeField] private float _medKitsSpawnRange;
        [SerializeField] private float _medKitsHealAmount;
        [SerializeField] private MedKitInteractable _medKitsPrefab;

        private Controller _movementController;
        private Controller _jumpingController;

        private MedKitsSpawnerController _medKitsSpawnerController;

        private void Awake()
        {
            _agent.Initialize(_moveSpeed, _rotateSpeed, _maxHealth);

            _movementController = new CompositeDowntimeSwitchController(new ClickPositionController(_agent, _camera, _groundMask),
                new RandomDirectionController(_agent, _randomPointRadius), _switchToRandomPointModeTime);

            _jumpingController = new JumpingController(_agent);

            MedKitSpawner medKitsSpawner = new(_medKitsSpawnInterval, _medKitsSpawnRange, _medKitsHealAmount, _agent.transform, _medKitsPrefab,
                this);

            _medKitsSpawnerController = new MedKitsSpawnerController(medKitsSpawner);

            _movementController.Enable();
            _jumpingController.Enable();
            medKitsSpawner.Enable();
            _medKitsSpawnerController.Enable();
        }

        private void Start()
        {
            _agent.HealthExhausted += OnAgentHealthExhausted;
        }

        private void OnAgentHealthExhausted()
        {
            _movementController.Disable();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _movementController.Update(deltaTime);
            _jumpingController.Update(deltaTime);
            _medKitsSpawnerController.Update(deltaTime);
        }

        private void OnDestroy()
        {
            _agent.HealthExhausted -= OnAgentHealthExhausted;
        }
    }
}