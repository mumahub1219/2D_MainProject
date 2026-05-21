using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Inst { get; set; }

    [Header("카메라 참조")]
    public Camera MainCamera;

    [Header("추적 설정")]
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, -10);
    [SerializeField] private float _smoothSpeed = 5.0f;

    

    private void Awake()
    {
        Inst = this;

        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    private void Start()
    {
        SetTarget(GameManager.Inst.LocalPlayer.transform);
    }

    private void LateUpdate()
    {
        CameraMove(_cameraTarget);
    }

    public void SetTarget(Transform newTarget)
    {
        _cameraTarget = newTarget;
    }

    public void CameraMove(Transform cameraTarget)
    {
        if (cameraTarget == null) return;

        Vector3 targetPosition = cameraTarget.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        //Vector3 targetPosition = new Vector3(cameraTarget.position.x, _fixedY, _fixedZ);
        //Vector3 movePosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        //transform.position = movePosition;
    }
}
