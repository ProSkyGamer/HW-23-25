#region

using System;
using UnityEngine;

#endregion

namespace Health
{
    public class HealthComponent : MonoBehaviour, IHaveHealth
    {
        public event Action Hit;
        public event Action Dead;

        // Там конечно сказано в задании, что можно из логики вызывать,
        // но я лучше так сделаю раз все равно знаю как)

        public float Current { get; private set; }
        public float Max { get; private set; }

        // я думал насчет того стоит ли это прописать просто через сериализованное поле здесь или вынести,
        // но как будто логичнее вынести, так и в будущем удобнее будет
        // Да и сейчас получится странно, что половина статов указывается в менеджере, а половина на самом игроке

        // вообще сейчас вот сижу думаю, а логично ли его оставить отдельным компоненом или стоило прикрепить к agent character
        // наверное стоило бы создать какой-то компонент по типу Player, который бы объединял в себе agent character и наследовался бы от IHaveHealth?
        // и сделать его фасадом для 
        public void Initialize(float maxHealth)
        {
            Current = Max = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0f)
                return;

            Current -= damage;
            Current = GetNormalizedHealth(Current);

            Debug.Log($"New health: {Current}");

            if (Current <= 0f)
                Die();
            else
                Hit?.Invoke();
        }

        private void Die()
        {
            Dead?.Invoke();
        }

        public void Heal(float heal)
        {
            if (heal <= 0f)
                return;

            Current += heal;
            Current = GetNormalizedHealth(Current);
        }

        private float GetNormalizedHealth(float health)
        {
            return Mathf.Clamp(health, 0f, Max);
        }
    }
}