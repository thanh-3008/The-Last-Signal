using UnityEngine;
using UnityEngine.EventSystems;
public class StatHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipUI;
    private void Awake()
    {
        tooltipUI.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipUI.SetActive(true);
        Debug.Log("Hiển thị mô tả");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipUI.SetActive(false);
    }
}
