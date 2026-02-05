using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject PanelCharacterUpgrade;
    bool isPause = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PanelCharacterUpgrade.SetActive(!isPause);
            isPause = !isPause;
            GameTimeManager.Instance.SetGamePaused(isPause);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            UpgradeManager.Instance.ShowUpgradeSelection();
        }
    }
}
