#region

using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;

    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _healthText;
    private const string HealthFormat = "{0}/{1}";

    private void Update()
    {
        float healthPercentage = _agentCharacter.CurrentHealth / _agentCharacter.MaxHealth;

        _slider.value = healthPercentage;

        _healthText.text = string.Format(HealthFormat, _agentCharacter.CurrentHealth, _agentCharacter.MaxHealth);
    }
}