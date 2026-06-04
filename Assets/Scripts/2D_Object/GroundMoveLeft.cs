using UnityEngine;

public class GroundMoveLeft : ObjectBase_2D
{
    [Header("움직임 설정")]
    [SerializeField] private float _moveDistance = 5.0f;
    [SerializeField] private float _speed = 2.0f;

    private Vector3 _startPosition;
    private Vector3 _lastPosition;
    private float _currentVelocityX;
    private PlayerMove_2D _Player;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        GroundMovement();
        CalculateVelocity();
        ApplyVelocityToPlayer();
    }

    private void GroundMovement()
    {
        float timeFactor = Time.time * _speed;
        float progress = (Mathf.Sin(timeFactor) + 1.0f) * 0.5f;
        float newX = _startPosition.x - (progress * _moveDistance);

        transform.position = new Vector3(newX, _startPosition.y, _startPosition.z);
    }

    private void CalculateVelocity()
    {
        _currentVelocityX = (transform.position.x - _lastPosition.x) / Time.fixedDeltaTime;
        _lastPosition = transform.position;
    }

    private void ApplyVelocityToPlayer()
    {
        if (_Player != null)
        {
            _Player.SetExternalVelocityX(_currentVelocityX);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 위에서 밟았을 때만 플레이어 컴포넌트를 가져와 저장
            if (collision.contacts[0].normal.y < -0.5f)
            {
                _Player = collision.gameObject.GetComponent<PlayerMove_2D>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 발판에서 벗어나면 플레이어의 외부 속도를 0으로 초기화하고 참조를 끊음
            if (_Player != null)
            {
                _Player.SetExternalVelocityX(0f);
                _Player = null;
            }
        }
    }
}
