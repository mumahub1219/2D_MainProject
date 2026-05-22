using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Inst { get; set; }

    [Header("카메라")]
    public Camera MainCamera;

    [Header("추적 설정")]
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, -10);
    [SerializeField] private float _smoothSpeed = 5.0f;

    private void Awake()
    {
        Inst = this;
        CameraSetting();
    }

    private void Start()
    {
        StartCoroutine(InitCameraLoop());
    }

    private void LateUpdate()
    {
        CameraMove(_cameraTarget);
    }

    public void CameraSetting()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    private IEnumerator InitCameraLoop()
    {
        while (GameObjectManager.Inst.GetLocalPlayer() == null)
        {
            yield return null;
        }

        GetLocalPlayerPosition();
    }

    public void GetLocalPlayerPosition()
    {
        var localPlayer = GameObjectManager.Inst.GetLocalPlayer();
        if (localPlayer == null) return;

        SetTarget(localPlayer.transform);
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) return;
        
        _cameraTarget = newTarget;
    }

    public void CameraMove(Transform cameraTarget)
    {
        if (cameraTarget == null || MainCamera == null) return;

        float targetX = cameraTarget.position.x + _offset.x;
        float targetY = MainCamera.transform.position.y;
        float targetZ = _offset.z;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        Vector3 smoothedPosition = Vector3.Lerp(MainCamera.transform.position, targetPosition, _smoothSpeed * Time.deltaTime);

        MainCamera.transform.position = smoothedPosition;
    }
}
