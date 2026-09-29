public interface IHaveHealth
{
    public float Current { get; }
    public float Max { get; }

    public void TakeDamage(float damage);
    public void Heal(float heal);
}