using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Transform_Player;
    [SerializeField] private float smoothing = 5.0f;

    private float _fixedY;
    private float _fixedZ;

    private void Start()
    {
        _fixedY = transform.position.y;
        _fixedZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (Transform_Player == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(Transform_Player.position.x, _fixedY, _fixedZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}

