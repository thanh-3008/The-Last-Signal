using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [Header("UI Text Components")]
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textTalent;
    public TextMeshProUGUI textProfession;
    public TextMeshProUGUI textDes;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textDame;
    public TextMeshProUGUI textSpeed;
    public TextMeshProUGUI textArmor;
    public TextMeshProUGUI textCritChance;
    public TextMeshProUGUI textCritDame;
    public TextMeshProUGUI textExpGain;
    public TextMeshProUGUI textCoinGain;

    [Header("Visuals")]
    public Image imageCharacter; // Thêm Image để xử lý khi không có animation
    public Animator animatorIdle;

    [Header("Data")]
    [SerializeField] private CharacterDatabase characterDb;
    public int currentCharacterIndex;

    private void Awake()
    {
        // Kiểm tra xem đã kéo Database vào chưa để tránh lỗi Null
        if (characterDb == null)
        {
            Debug.LogError("Chưa kéo CharacterDatabase vào Inspector của SelectCharacterUI!");
            return;
        }
        DisplayTextUI();
    }

    private void DisplayTextUI()
    {
        if (characterDb == null || characterDb.playerDatas.Count == 0) return;

        // Lấy index từ bộ nhớ
        currentCharacterIndex = PlayerPrefs.GetInt("index", 0);

        // Đảm bảo index luôn nằm trong phạm vi danh sách
        currentCharacterIndex = Mathf.Clamp(currentCharacterIndex, 0, characterDb.playerDatas.Count - 1);

        PlayerData currentData = characterDb.playerDatas[currentCharacterIndex];

        // Cập nhật thông tin chữ (Localization)
        textName.text = currentData.entityName.GetLocalizedString();
        textTalent.text = currentData.talentCharacter.GetLocalizedString();
        textProfession.text = currentData.classCharacter.GetLocalizedString();
        textDes.text = currentData.descriptionCharacter.GetLocalizedString();

        // Cập nhật chỉ số
        textHealth.text = currentData.maxHealth.ToString();
        textSpeed.text = currentData.moveSpeed.ToString();
        textDame.text = currentData.dameBase.ToString();
        textArmor.text = currentData.armor.ToString();
        textCritChance.text = currentData.critChance.ToString() + "%";
        textCritDame.text = currentData.critDame.ToString() + "%";
        textExpGain.text = currentData.expGain.ToString();
        textCoinGain.text = currentData.coinGain.ToString();

        // Cập nhật hình ảnh/Animation
        UpdateCharacterVisual(currentData);
    }

    private void UpdateCharacterVisual(PlayerData data)
    {
        if (animatorIdle == null) return;

        if (data.animatorIdle != null)
        {
            // Bật Animator và gán Controller
            animatorIdle.enabled = true;
            animatorIdle.runtimeAnimatorController = data.animatorIdle;

            // Reset Animator để chắc chắn chạy lại từ frame đầu của Idle
            animatorIdle.Rebind();
            animatorIdle.Update(0f);
        }
        else
        {
            // Nếu nhân vật này không có Animation, tắt Animator và dùng Sprite tĩnh
            animatorIdle.enabled = false;
            if (imageCharacter != null)
            {
                imageCharacter.sprite = data.avatarCharacter;
            }
        }
    }

    // --- Xử lý Chuyển đổi ngôn ngữ ---
    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocalChange;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocalChange;
    }

    private void OnLocalChange(Locale locale)
    {
        DisplayTextUI();
    }

    // --- Xử lý Đổi nhân vật ---
    public void NextCharacter()
    {
        if (characterDb == null) return;

        currentCharacterIndex++;
        if (currentCharacterIndex >= characterDb.playerDatas.Count)
        {
            currentCharacterIndex = 0;
        }

        PlayerPrefs.SetInt("index", currentCharacterIndex);
        DisplayTextUI();
    }

    public void PreviousCharacter()
    {
        if (characterDb == null) return;

        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characterDb.playerDatas.Count - 1;
        }

        PlayerPrefs.SetInt("index", currentCharacterIndex);
        DisplayTextUI();
    }

    public void SelectCharacter()
    {
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene");
    }
}