#region

using System.Collections;
using UnityEngine;

#endregion

public class MedKitInteractable : MonoBehaviour
{
    private class TriggerResult
    {
        public IHaveHealth TriggeredObject;
    }

    [SerializeField] private float _healAmount;
    [SerializeField] private float _pickupRange;
    [SerializeField] private LayerMask _interactableLayers;

    private Coroutine _lifeCycleCoroutine;

    private readonly Collider[] _activationColliders = new Collider[1];

    public void Initialize(float healAmount)
    {
        _healAmount = healAmount;

        if (_lifeCycleCoroutine != null)
            StopCoroutine(_lifeCycleCoroutine);

        _lifeCycleCoroutine = StartCoroutine(ProcessLifeCycle(_pickupRange, _interactableLayers, _healAmount));
    }

    private void Awake()
    {
        if (_lifeCycleCoroutine != null)
            StartCoroutine(ProcessLifeCycle(_pickupRange, _interactableLayers, _healAmount));
    }

    private IEnumerator ProcessLifeCycle(float activationRange, LayerMask activationLayerMask,
        float healAmount)
    {
        TriggerResult triggerResult = new();

        yield return StartCoroutine(ProcessTrigger(activationRange, activationLayerMask, triggerResult));

        HealObject(triggerResult.TriggeredObject, healAmount);

        Destroy(gameObject);
    }

    private IEnumerator ProcessTrigger(float triggerRange, LayerMask layerMask, TriggerResult triggerResult)
    {
        while (true)
        {
            Physics.OverlapSphereNonAlloc(transform.position, triggerRange, _activationColliders, layerMask);

            foreach (Collider activationCollider in _activationColliders)
            {
                if (activationCollider != null && activationCollider.transform.TryGetComponent(out IHaveHealth health))
                {
                    triggerResult.TriggeredObject = health;

                    yield break;
                }
            }

            yield return null;
        }
    }

    private void HealObject(IHaveHealth healingObject, float healAmount)
    {
        healingObject.Heal(healAmount);
    }
}