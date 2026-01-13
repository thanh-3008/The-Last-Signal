using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class SelectCharacterUI : MonoBehaviour
{
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

    public int currentCharacterIndex;
    [SerializeField]
    private List<PlayerData> playerDatas;
    private void Start()
    {
        DisplayTextUI();
    }
    private void DisplayTextUI()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("index", 0);
        textName.text = playerDatas[currentCharacterIndex].entityName.GetLocalizedString();
        textTalent.text = playerDatas[currentCharacterIndex].talentCharacter.GetLocalizedString();
        textProfession.text = playerDatas[currentCharacterIndex].classCharacter.GetLocalizedString();
        textDes.text = playerDatas[currentCharacterIndex].descriptionCharacter.GetLocalizedString();
        textHealth.text = playerDatas[currentCharacterIndex].maxHealth.ToString();
        textSpeed.text = playerDatas[currentCharacterIndex].moveSpeed.ToString();
        textDame.text = playerDatas[currentCharacterIndex].dameBase.ToString();
        textArmor.text = playerDatas[currentCharacterIndex].armor.ToString();
        textCritChance.text = playerDatas[currentCharacterIndex].critChance.ToString();
        textCritDame.text = playerDatas[currentCharacterIndex].critDame.ToString();
        textExpGain.text = playerDatas[currentCharacterIndex].expGain.ToString();
        textCoinGain.text = playerDatas[currentCharacterIndex].coinGain.ToString();
    }
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

    public void NextCharacter()
    {
        currentCharacterIndex += 1;
        if(currentCharacterIndex>playerDatas.Count-1)
        {
            currentCharacterIndex = 0;
        }
        PlayerPrefs.SetInt("index", currentCharacterIndex);
        DisplayTextUI();
    }
    public void PreviousCharacter()
    {
        currentCharacterIndex -= 1;
        if(currentCharacterIndex<0)
        {
            currentCharacterIndex = playerDatas.Count - 1;
        }
        PlayerPrefs.SetInt("index", currentCharacterIndex);
        DisplayTextUI();
    }
}
