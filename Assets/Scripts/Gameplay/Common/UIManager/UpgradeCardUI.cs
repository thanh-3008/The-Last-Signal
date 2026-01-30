using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public TextMeshProUGUI textName1;
    public TextMeshProUGUI textName2;
    public TextMeshProUGUI textName3;

    public TextMeshProUGUI textDes1;
    public TextMeshProUGUI textDes2;
    public TextMeshProUGUI textDes3;

    public Image image1;
    public Image image2;
    public Image image3;

    public void SetUp(List<UpgradeData> upgradeDatas)
    {
        // Xử lý Panel 1
        SetPanelData(panel1, textName1, textDes1, image1, upgradeDatas[0]);

        // Xử lý Panel 2
        SetPanelData(panel2, textName2, textDes2, image2, upgradeDatas[1]);

        // Xử lý Panel 3
        SetPanelData(panel3, textName3, textDes3, image3, upgradeDatas[2]);
    }

    // Tạo một hàm phụ để tránh lặp lại code (DRY - Don't Repeat Yourself)
    private void SetPanelData(GameObject panel, TextMeshProUGUI nameTxt, TextMeshProUGUI desTxt, Image iconImg, UpgradeData data)
    {
        nameTxt.text = data.nameUpgrade.GetLocalizedString();
        desTxt.text = data.desUpgrade.GetLocalizedString();
        iconImg.sprite = data.imgUpgrade;

        // Lấy component Image của Panel để đổi màu nền
        Image panelBg = panel.GetComponent<Image>();

        switch (data.type)
        {
            case UpgradeData.UpgradeType.StatModifier:
                panelBg.color = Color.blue;
                break;
            case UpgradeData.UpgradeType.Ability:
                panelBg.color = Color.yellow;
                break;
            case UpgradeData.UpgradeType.Consumable:
                panelBg.color = Color.green;
                break;
            case UpgradeData.UpgradeType.UltimateUpgrade:
                panelBg.color = Color.magenta; 
                break;
            default:
                panelBg.color = Color.white;
                break;
        }
    }
}

