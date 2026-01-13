using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using static UnityEditor.ShaderData;
public class SettingManager : MonoBehaviour
{
    public LocalizedString changeLanguage;
    public LocalizedString back;
    public TextMeshProUGUI textChangeLanguge;
    public TextMeshProUGUI textBack;

    public GameObject panelSetting;
    public GameObject panelLocaleChange;
    private bool isPause=false;
    private bool isPanelSetting=true;

    private void Awake()
    {
        SetAllPanelSettingHide();
    }
    void Start()
    {
        UpdateDisplay();
    }

    private void Update()
    {
        if(isPanelSetting==true)
        DisplayUISetting();
    }
    private void SetAllPanelSettingHide()
    {
        panelSetting.SetActive(false);
        panelLocaleChange.SetActive(false);
    }
    public void SetPanelLocalHide()
    {
        isPanelSetting = true;
        panelLocaleChange.SetActive(false);
    }

    public void DisplayUISetting()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == true) Resume();
            else Pause();
        }
    }
    public void DisplayUIChangeLocale()
    {
        panelLocaleChange.SetActive(true);
        isPanelSetting = false;
    }
    public void Resume()
    {
        panelSetting.SetActive(false);
        isPause = false;
    }

    public void Pause()
    {
        panelSetting.SetActive(true);
        isPause = true;
    }

    public void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChange;
    }
    public void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChange;
    }

    private void OnLocaleChange(Locale locale)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        textChangeLanguge.text = changeLanguage.GetLocalizedString();
        textBack.text = back.GetLocalizedString();
    }
}