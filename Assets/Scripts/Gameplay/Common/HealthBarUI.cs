using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Entity targetEntity;

    public Image imageHealthBar;

    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if(obj!= null)
        {
            targetEntity = obj.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Không tìm thấy enity");
        }
    }

    private void OnEnable()
    {
        if (targetEntity != null)
        {
            targetEntity.OnHealthChanged += UpdateHealthBar;
        }
    }

    private void OnDisable()
    {
        if (targetEntity != null)
        {
            targetEntity.OnHealthChanged -= UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        imageHealthBar.fillAmount = currentHealth / maxHealth;
    }
}
