using System;
using UnityEngine;

public class BladeObject : MonoBehaviour
{
    [Header("움직임 설정")]
    [SerializeField] private float _moveDistance = 5.0f;
    [SerializeField] private float _speed = 2.0f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        BladeMovement();
    }

    private void BladeMovement()
    {
        float timeFactor = Time.time * _speed;
        float progress = (Mathf.Sin(timeFactor) + 1.0f) * 0.5f;
        float newY = _startPosition.y + (progress * _moveDistance);

        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
    }
}
