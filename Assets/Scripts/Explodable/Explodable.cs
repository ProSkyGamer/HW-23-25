#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class Explodable : MonoBehaviour
{
    public event Action Exploded;

    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _explosionCountdown;
    private readonly Collider[] _activationColliders = new Collider[1];

    public bool IsCountdownTriggered { get; private set; }

    public float TotalCountdownTime => _explosionCountdown;
    public float CountdownTimeLeft { get; private set; }

    private void Awake()
    {
        StartCoroutine(ProcessLifeCycle(_range, _layerMask, _explosionCountdown, _range, _layerMask));
    }

    private IEnumerator ProcessLifeCycle(float activationRange, LayerMask activationLayerMask, float explosionCountdown,
        float explosionRange, LayerMask explosionLayerMask)
    {
        yield return StartCoroutine(ProcessTrigger(activationRange, activationLayerMask));

        yield return StartCoroutine(ProcessCountdown(explosionCountdown));

        Explode(explosionRange, explosionLayerMask);

        Exploded?.Invoke();

        Destroy(gameObject);
    }

    private IEnumerator ProcessTrigger(float triggerRange, LayerMask layerMask)
    {
        while (true)
        {
            Physics.OverlapSphereNonAlloc(transform.position, triggerRange, _activationColliders, layerMask);

            foreach (Collider activationCollider in _activationColliders)
            {
                if (activationCollider != null)
                    yield break;
            }

            yield return null;
        }
    }

    private IEnumerator ProcessCountdown(float countdown)
    {
        IsCountdownTriggered = true;
        CountdownTimeLeft = countdown;

        while (CountdownTimeLeft > 0)
        {
            CountdownTimeLeft -= Time.deltaTime;

            yield return null;
        }
    }

    private void Explode(float explosionRange, LayerMask damagingLayers)
    {
        List<IHaveHealth> allDamagables = GetDamageableObjectIn(explosionRange, damagingLayers);

        DealDamageTo(allDamagables);
    }

    private void DealDamageTo(List<IHaveHealth> allDamagables)
    {
        foreach (IHaveHealth damageable in allDamagables)
        {
            damageable.TakeDamage(_damage);
        }
    }

    private List<IHaveHealth> GetDamageableObjectIn(float range, LayerMask damagingLayers)
    {
        return Physics.SphereCastAll(transform.position, range, Vector3.down, range, damagingLayers)
            .Select(cast => cast.transform.GetComponent<IHaveHealth>()).Where(health => health != null).ToList();
    }
}