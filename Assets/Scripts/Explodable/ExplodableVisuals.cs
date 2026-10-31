#region

using UnityEngine;

#endregion

public class ExplodableVisuals : MonoBehaviour
{
    [SerializeField] private Explodable _explodable;
    [Range(0f, 1f)] [SerializeField] private float _intenseBlinkingSwitchPercentage;

    [SerializeField] private float _normalFlashingInterval;
    [SerializeField] private float _intenseFlashingInterval;

    [SerializeField] private Transform _normalStateTransform;
    [SerializeField] private Transform _flashStateTransform;
    [SerializeField] private bool _startingVisualState;
    private bool _currentVisualState;

    private float _timeTillColorChange;

    private bool IsIntenseFlashingStage => _explodable.CountdownTimeLeft / _explodable.TotalCountdownTime <= _intenseBlinkingSwitchPercentage;
    private bool IsColorChangeTimerUp => _timeTillColorChange <= 0f;

    private void Awake()
    {
        ChangeCurrentVisualStateTo(_startingVisualState);
    }

    private void Update()
    {
        if (_explodable.IsCountdownTriggered)
            ProcessColorChange();
    }

    private void ProcessColorChange()
    {
        _timeTillColorChange -= Time.deltaTime;

        if (IsColorChangeTimerUp)
        {
            ChangeCurrentVisualStateTo(!_currentVisualState);

            _timeTillColorChange = IsIntenseFlashingStage ? _intenseFlashingInterval : _normalFlashingInterval;
        }
    }

    private void ChangeCurrentVisualStateTo(bool newState)
    {
        _currentVisualState = newState;

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _normalStateTransform.gameObject.SetActive(_currentVisualState);
        _flashStateTransform.gameObject.SetActive(_currentVisualState == false);
    }
}