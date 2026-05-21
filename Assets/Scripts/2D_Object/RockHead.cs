using UnityEngine;

public class RockHead : MonoBehaviour
{
    [Header("움직임 설정")]
    [SerializeField] private float _moveDistance = 5.0f;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _fallSharpness = 3.0f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        RockHeadMovement();
    }

    private void RockHeadMovement()
    {
        float timeFactor = Time.time * _speed;
        float progress = (Mathf.Sin(timeFactor) + 1.0f) * 0.5f;

        float modifiedProgress = Mathf.Pow(progress, _fallSharpness);

        float newY = _startPosition.y + (modifiedProgress * _moveDistance);
        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
    }
}
