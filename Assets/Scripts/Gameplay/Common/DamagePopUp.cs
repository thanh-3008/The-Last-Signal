using UnityEngine;
using TMPro; // Cần dòng này để dùng TextMeshPro

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());

        if (!isCriticalHit)
        {
            textMesh.fontSize = 5;
            textColor = new Color(1f, 0.92f, 0.016f, 1f);
        }
        else
        {
            textMesh.fontSize = 7;
            textColor = new Color(1f, 0.2f, 0.2f, 1f);
        }

        textMesh.color = textColor;

        // ✅ CHỈ BAY LÊN TRÊN (KHÔNG CÓ X)
        moveVector = new Vector3(0f, 0.5f, 0f);

        disappearTimer = DISAPPEAR_TIMER_MAX;
    }

    private void Update()
    {
        moveVector.x = 0;
        // 1. Di chuyển (Chỉ thay đổi X và Y cho 2D)
        transform.position += moveVector * Time.deltaTime;

        // Giảm tốc độ bay dần dần
        moveVector -= moveVector * 8f * Time.deltaTime;

        // 2. Hiệu ứng Scale (Nảy lên rồi thu nhỏ)
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            // Nửa đầu: Phóng to
            transform.localScale += Vector3.one * 1f * Time.deltaTime;
        }
        else
        {
            // Nửa sau: Thu nhỏ
            transform.localScale -= Vector3.one * 1f * Time.deltaTime;
        }

        // 3. Xử lý mờ dần (Fade out)
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float fadeSpeed = 3f;
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject); // Hủy object khi đã mờ hết
            }
        }
    }
}