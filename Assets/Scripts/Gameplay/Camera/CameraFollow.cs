using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [Header ("Target Setting")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10);

    [Header("Smooth Settings")]
    [SerializeField] private float smoothTime = 0.1f; // Thời gian trễ (càng nhỏ càng bám sát)
    private Vector3 currentVelocity = Vector3.zero;
    private void Awake()
    {
        if(target==null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("Camera không tìm thấy đối tượng nào có Tag là 'Player'!");
            }
        }
    }
    private void LateUpdate()
    {
        Vector3 targetPosision = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosision, ref currentVelocity, smoothTime);
    }
}
