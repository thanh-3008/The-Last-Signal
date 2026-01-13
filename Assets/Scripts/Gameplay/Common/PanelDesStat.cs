using TMPro;
using UnityEngine;

public class PanelDesStat : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDes;

    private Vector3 targetPosition;

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        // 1. Lấy vị trí chuột trực tiếp (tọa độ Screen Space)
        // Không dùng Camera.main vì dễ gây lỗi Null và không cần thiết cho UI Overlay
        targetPosition = Input.mousePosition;

        // Cộng thêm một khoảng offset (ví dụ: x+10, y-10) để panel không đè lên con trỏ
        this.transform.position = targetPosition + new Vector3(50, 30, 0);
    }
}