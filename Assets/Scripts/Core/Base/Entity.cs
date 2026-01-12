using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header ("Base Stats")]  
    public float moveSpeed;
    public float maxHealth;
    public float currentHealth;

    public event Action<float, float> OnHealthChanged;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Player take dame" + damage);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Phát thông báo cho những đối tượng lắng nghe
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();

}
