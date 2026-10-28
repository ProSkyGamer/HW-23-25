#region

using System.Collections;
using Controllers;
using UnityEngine;

#endregion

public class MedKitSpawner : CoroutineController
{
    private readonly float _spawnInterval;
    private readonly float _spawnRange;
    private readonly float _healAmount;

    private readonly Transform _target;
    private readonly MedKitInteractable _medKitPrefab;

    public MedKitSpawner(float spawnInterval, float spawnRange, float healAmount, Transform target, MedKitInteractable medKitPrefab,
        MonoBehaviour coroutineRunner) : base(coroutineRunner)
    {
        _spawnInterval = spawnInterval;
        _spawnRange = spawnRange;
        _healAmount = healAmount;
        _target = target;
        _medKitPrefab = medKitPrefab;
    }

    protected override IEnumerator ProcessController()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            Vector3 spawningPosition = NavMeshHelper.GetRandomLocation(_target.position, _spawnRange);

            SpawnMedKitAt(spawningPosition);
        }
    }

    private void SpawnMedKitAt(Vector3 spawningPosition)
    {
        MedKitInteractable newMedKit = Object.Instantiate(_medKitPrefab, spawningPosition, Quaternion.identity);
        newMedKit.Initialize(_healAmount);
    }
}