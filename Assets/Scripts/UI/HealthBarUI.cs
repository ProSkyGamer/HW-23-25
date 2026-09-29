#region

using Health;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;

    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _healthText;
    private const string HealthFormat = "{0}/{1}";

    private void Update()
    {
        float healthPercentage = _healthComponent.Current / _healthComponent.Max;

        _slider.value = healthPercentage;

        _healthText.text = string.Format(HealthFormat, _healthComponent.Current, _healthComponent.Max);
    }
}