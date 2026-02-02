using UnityEngine;

/// <summary>
/// Singleton manager ?? cung c?p reference t?i PlayerController cho toàn b? game.
/// Tìm player m?t l?n duy nh?t ngay t? ??u game và cache reference.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private PlayerController playerController;
    public PlayerController Player => playerController;

    private void Awake()
    {
        // Kh?i t?o Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("PlayerManager: Có nhi?u instance c?a PlayerManager. Xoá instance d? th?a.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void FindPlayer()
    {
        // Cách 1: Tìm theo tag "Player"
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerController = playerObj.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("PlayerManager: Tìm th?y Player qua tag.");
                return;
            }
        }

        // Cách 2: N?u không tìm được theo tag, tìm component PlayerController trong scene
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            Debug.Log("PlayerManager: Tìm th?y Player qua FindFirstObjectByType.");
            return;
        }

        // Không tìm ???c player
        Debug.LogError("PlayerManager: Không tìm thấy PlayerController trong scene!");
    }
    public PlayerController GetPlayer()
    {
        if (playerController == null)
        {
            FindPlayer();
        }
        return playerController;
    }

    /// <summary>
    /// Ki?m tra xem Player có t?n t?i không.
    /// </summary>
    public bool HasPlayer()
    {
        return playerController != null;
    }
}
