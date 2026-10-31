#region

using System;

#endregion

public interface IHaveHealth
{
    public event Action<float, float> HealthChanged;
    public event Action HealthExhausted;

    public float CurrentHealth { get; }
    public float MaxHealth { get; }
}