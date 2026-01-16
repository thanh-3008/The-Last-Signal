using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DesStatManager : MonoBehaviour
{
    public LocalizedString des;
    public TextMeshProUGUI textDes;

    private void Awake()
    {
        DisplayTextDes();
    }
    private void DisplayTextDes()
    {
        textDes.text = des.GetLocalizedString();
    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChange;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChange;
    }

    private void OnLocaleChange(Locale locale)
    {
        DisplayTextDes();
    }
}
