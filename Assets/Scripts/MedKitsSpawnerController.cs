#region

using UnityEngine;

#endregion

public class MedKitsSpawnerController : MonoBehaviour
{
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _healAmount;
    [SerializeField] private Transform _spawnAroundTarget;
    [SerializeField] private MedKitInteractable _medKitPrefab;

    private MedKitSpawner _medKitSpawner;

    private const KeyCode SwitchStateKeyCode = KeyCode.F;

    private void Awake()
    {
        _medKitSpawner = new MedKitSpawner(_spawnInterval, _spawnRange, _healAmount, _spawnAroundTarget, _medKitPrefab, this);

        _medKitSpawner.Enable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(SwitchStateKeyCode))
            if (_medKitSpawner.IsActive)
                _medKitSpawner.Disable();
            else
                _medKitSpawner.Enable();
    }
}