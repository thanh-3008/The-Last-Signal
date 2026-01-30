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
        SetPanelData(panel1, textName1, textDes1, image1, upgradeDatas[0]);
        SetPanelData(panel2, textName2, textDes2, image2, upgradeDatas[1]);
        SetPanelData(panel3, textName3, textDes3, image3, upgradeDatas[2]);
    }

    private void SetPanelData(GameObject panel, TextMeshProUGUI nameTxt, TextMeshProUGUI desTxt, Image iconImg, UpgradeData data)
    {
        // Kiểm tra để tránh lỗi ArgumentException: Empty Table Reference
        nameTxt.text = (data.nameUpgrade != null && !data.nameUpgrade.IsEmpty) ? data.nameUpgrade.GetLocalizedString() : "No Name";
        desTxt.text = (data.desUpgrade != null && !data.desUpgrade.IsEmpty) ? data.desUpgrade.GetLocalizedString() : "No Description";

        iconImg.sprite = data.imgUpgrade;

        Image panelBg = panel.GetComponent<Image>();
        string hexColor = "#FFFFFF"; // Mặc định là trắng

        // Chọn mã Hex dựa trên loại Upgrade
        switch (data.type)
        {
            case UpgradeData.UpgradeType.StatModifier:
                hexColor = "#84A7F7CE";
                break;
            case UpgradeData.UpgradeType.Ability:
                hexColor = "#E8CD32CE";
                break;
            case UpgradeData.UpgradeType.Consumable:
                hexColor = "#85D540CE";
                break;
            case UpgradeData.UpgradeType.UltimateUpgrade:
                hexColor = "#9D22B4CE";
                break;
            default:
                hexColor = "#FFFFFF";
                break;
        }

        // Chuyển mã Hex thành Color và gán cho panel
        if (ColorUtility.TryParseHtmlString(hexColor, out Color customColor))
        {
            panelBg.color = customColor;
        }
    }
}