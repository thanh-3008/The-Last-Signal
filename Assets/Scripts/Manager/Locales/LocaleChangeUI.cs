using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class LocaleChangeUI : MonoBehaviour
{
    public LocalizedString eng;
    public LocalizedString viet;
    public TextMeshProUGUI textEnglish;
    public TextMeshProUGUI textVietNamese;
    private void Start()
    {
        UpdateLocale();
    }
    public void UpdateLocale()
    {
        textEnglish.text = eng.GetLocalizedString();
        textVietNamese.text = viet.GetLocalizedString();
    }
    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChange;
    }
    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChange;
    }
    private void OnLocaleChange(Locale locale)
    {
        UpdateLocale();
    }
}
