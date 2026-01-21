using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [Header ("Base Stats")]
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public float finalDamage;
    public event Action<float, float> OnHealthChanged;

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public virtual void TakeDamage(float rawDamage)
    {
        // 3. Trừ máu
        currentHealth -= GetFinalDamage(rawDamage); ;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Phát thông báo cho những đối tượng lắng nghe
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public float GetFinalDamage(float rawDamage)
    {
        // 1. Tính hệ số giảm sát thương dựa trên Armor
        // Công thức: Damage nhận = Damage gốc * (100 / (100 + Armor))
        float damageReductionMultiplier = 100f / (100f + Mathf.Max(armor, 0));

        // 2. Tính sát thương thực tế
        finalDamage = rawDamage * damageReductionMultiplier;
        return finalDamage;
    }
    protected abstract void Die();

}
