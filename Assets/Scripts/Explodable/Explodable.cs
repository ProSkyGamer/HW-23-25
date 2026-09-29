#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class Explodable : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _explosionCountdown;
    private readonly Collider[] _activationColliders = new Collider[1];

    private bool IsExplosionCountdownUp => CountdownTimeLeft <= 0f;

    public bool IsCountdownTriggered { get; private set; }

    public float TotalCountdownTime => _explosionCountdown;
    public float CountdownTimeLeft { get; private set; }

    private void Update()
    {
        if (IsCountdownTriggered == false)
        {
            if (TryTriggerCountdown())
                TriggerExplosionCountdown();
        }
        else
        {
            ProcessCountdown();
        }
    }

    private bool TryTriggerCountdown()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _range, _activationColliders, _layerMask);

        foreach (Collider activationCollider in _activationColliders)
        {
            if (activationCollider != null)
                return true;
        }

        return false;
    }

    private void TriggerExplosionCountdown()
    {
        IsCountdownTriggered = true;

        CountdownTimeLeft = _explosionCountdown;
    }

    private void ProcessCountdown()
    {
        CountdownTimeLeft -= Time.deltaTime;

        if (IsExplosionCountdownUp)
            Explode();
    }

    private void Explode()
    {
        List<IHaveHealth> allDamagables = GetDamageableObjectIn(_range);

        DealDamageTo(allDamagables);

        Destroy(gameObject);
    }

    private void DealDamageTo(List<IHaveHealth> allDamagables)
    {
        foreach (IHaveHealth damageable in allDamagables)
        {
            damageable.TakeDamage(_damage);
        }
    }

    private List<IHaveHealth> GetDamageableObjectIn(float range)
    {
        return Physics.SphereCastAll(transform.position, range, Vector3.down, range, _layerMask)
            .Select(cast => cast.transform.GetComponent<IHaveHealth>()).Where(health => health != null).ToList();
    }
}