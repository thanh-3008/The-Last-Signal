using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public List<UpgradeData> allUpgrades;
    public GameObject upgradePanel;

    private void Awake()
    {
        // Setup Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("UpgradeManager: Có nhi?u instance. Xoá instance d? th?a.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Ensure PlayerManager is available before using upgrades
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("UpgradeManager: PlayerManager not found in scene!");
        }
    }

    /// <summary>
    /// Apply upgrade to player. Player reference is obtained from PlayerManager.
    /// </summary>
    public void ApplyUpgrade(UpgradeData upgrade)
    {
        
    }
}
