using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings; // Thêm để bắt sự kiện đổi ngôn ngữ

public class CurrentUpgradeUI : MonoBehaviour
{
    [Header("--- Target Player ---")]
    [SerializeField] private PlayerController player;
    public LocalizedString nameCurrentStat;
    public TextMeshProUGUI textNameCurrentStat;

    public LocalizedString nameUpgradeStats;
    public TextMeshProUGUI textNameUpgradeStats;

    public LocalizedString nameUpgradeSkills;
    public TextMeshProUGUI textNameUpgradeSkills;

    [Header("--- Localized Strings ---")]
    public LocalizedString locHealth;
    public LocalizedString locDefense;
    public LocalizedString locAttack;
    public LocalizedString locSpeed;
    public LocalizedString locCritRate;
    public LocalizedString locCritDamage;
    public LocalizedString locExpGain;
    public LocalizedString locCoinGain;

    [Header("--- Row UI References (Labels) ---")]
    public TextMeshProUGUI rowHealth;
    public TextMeshProUGUI rowDefense;
    public TextMeshProUGUI rowAttack;
    public TextMeshProUGUI rowSpeed;
    public TextMeshProUGUI rowCritRate;
    public TextMeshProUGUI rowCritDamage;
    public TextMeshProUGUI rowExpGain;
    public TextMeshProUGUI rowCoinGain;

    [Header("--- Value UI References ---")]
    public TextMeshProUGUI valueHealth;
    public TextMeshProUGUI valueDefense;
    public TextMeshProUGUI valueAttack;
    public TextMeshProUGUI valueSpeed;
    public TextMeshProUGUI valueCritRate;
    public TextMeshProUGUI valueCritDamage;
    public TextMeshProUGUI valueExpGain;
    public TextMeshProUGUI valueCoinGain;

    private void OnEnable()
    {
        // Đăng ký sự kiện thay đổi ngôn ngữ
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

        // Đảm bảo có player
        if (player == null && PlayerManager.Instance != null)
        {
            player = PlayerManager.Instance.GetPlayer();
        }

        RefreshUI();
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh memory leak
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void OnLocaleChanged(Locale locale)
    {
        UpdateLabels();
    }

    public void RefreshUI()
    {
        UpdateLabels();
        if (player != null) UpdateValues();
    }

    public void UpdateLabels()
    {
        if(textNameCurrentStat) textNameCurrentStat.text = nameCurrentStat.GetLocalizedString();
        // Sử dụng toán tử null-coalescing để tránh crash nếu quên kéo Text trong Inspector
        if (rowHealth) rowHealth.text = locHealth.GetLocalizedString();
        if (rowDefense) rowDefense.text = locDefense.GetLocalizedString();
        if (rowAttack) rowAttack.text = locAttack.GetLocalizedString();
        if (rowSpeed) rowSpeed.text = locSpeed.GetLocalizedString();
        if (rowCritRate) rowCritRate.text = locCritRate.GetLocalizedString();
        if (rowCritDamage) rowCritDamage.text = locCritDamage.GetLocalizedString();
        if (rowExpGain) rowExpGain.text = locExpGain.GetLocalizedString();
        if (rowCoinGain) rowCoinGain.text = locCoinGain.GetLocalizedString();
    }

    public void UpdateValues()
    {
        if (player == null || player.data == null) return;

        valueHealth.text = $"{player.currentHealth}/{player.data.maxHealth}";
        valueDefense.text = player.data.armor.ToString();
        valueAttack.text = player.data.dameBase.ToString();
        valueSpeed.text = player.data.moveSpeed.ToString();
        valueCritRate.text = $"{player.data.critChance}%";
        valueCritDamage.text = $"{player.data.critDame}%";
        valueExpGain.text = $"+{player.data.expGain}%";
        valueCoinGain.text = $"+{player.data.coinGain}%";
    }
}