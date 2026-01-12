using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewDataTest",menuName ="DataTest/Test")]
public class DataTest : ScriptableObject
{
    public Sprite avatar;

    public LocalizedString nameData;
    public LocalizedString descriptionData;
}
