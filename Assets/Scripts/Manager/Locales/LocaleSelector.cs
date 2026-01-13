
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalSelector : MonoBehaviour
{
    private bool active = false;

    private void Start()
    {
        int currentLocaleId = PlayerPrefs.GetInt("localeId", 0);
        ChangeLocale(currentLocaleId);
    }
    public void ChangeLocale(int localeId)
    {
        if (active == true) { return; }
        Debug.Log("Chuyen doi ngon ngu");       
        StartCoroutine(SetLocale(localeId));
    }
    IEnumerator SetLocale(int localeId)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        PlayerPrefs.SetInt("localeId", localeId);
        active = false;
    }
}