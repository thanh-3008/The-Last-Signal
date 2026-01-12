using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class TestLocalization : MonoBehaviour
{
    public Image avatarCharacter;

    public TextMeshProUGUI nameCharacter;

    public TextMeshProUGUI desCharacter;

    public List<DataTest> characterData;

    public int currentIndex=0;
    [SerializeField]
    private LocalSelector localSelector;

    private void Start()
    {
        if(characterData.Count>0)
        UpdateInfomationCharacter(currentIndex);

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
        UpdateInfomationCharacter(currentIndex);
    }

    public void UpdateInfomationCharacter(int index)
    {
        avatarCharacter.sprite = characterData[index].avatar;
        nameCharacter.text = characterData[index].nameData.GetLocalizedString();
        desCharacter.text = characterData[index].descriptionData.GetLocalizedString();
        currentIndex = index;
    }

    public void NextCharacter()
    {
        currentIndex += 1;
        if(currentIndex>characterData.Count-1)
        {
            currentIndex = 0;
        }
        UpdateInfomationCharacter(currentIndex);
    }

    public void PreviousCharacter()
    {
        currentIndex -= 1;
        if(currentIndex<0)
        {
            currentIndex = characterData.Count - 1;
        }
        UpdateInfomationCharacter(currentIndex);
    }
}
