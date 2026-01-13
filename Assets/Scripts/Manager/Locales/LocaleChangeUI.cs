using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class LocaleChangeUI : MonoBehaviour
{
    public LocalizedString eng;
    public LocalizedString viet;
    public LocalizedString back;
    public TextMeshProUGUI textEnglish;
    public TextMeshProUGUI textVietNamese;
    public TextMeshProUGUI backMenu;
    private void Start()
    {
        UpdateLocale();
    }
    public void UpdateLocale()
    {
        textEnglish.text = eng.GetLocalizedString();
        textVietNamese.text = viet.GetLocalizedString();
        backMenu.text = back.GetLocalizedString();
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
