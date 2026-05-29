using UnityEngine;

public class GroundMoveLeft : ObjectBase_2D
{
    [Header("움직임 설정")]
    [SerializeField] private float _moveDistance = 5.0f;
    [SerializeField] private float _speed = 2.0f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    private void Update()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        float timeFactor = Time.time * _speed;
        float progress = (Mathf.Sin(timeFactor) + 1.0f) * 0.5f;
        float newX = _startPosition.x - (progress * _moveDistance);

        transform.position = new Vector3(newX, _startPosition.y, _startPosition.z);
    }
}
