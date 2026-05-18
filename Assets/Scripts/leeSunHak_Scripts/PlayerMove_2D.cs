using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerMove_2D : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpForce = 10f;

    [Header("지면 설정")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.5f;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidBody;
    private bool _isGrounded;
    private float _horizontalInput;
    private bool _lookRight = true;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
        if (_horizontalInput > 0 && !_lookRight)
        {
            Flip();
        }
        else if (_horizontalInput < 0 && _lookRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        Move();
    }

    void Move()
    {
        _rigidBody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, _rigidBody.linearVelocity.y);
        _animator.SetInteger("playerState",0);
    }

    void Jump()
    {
        _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _jumpForce);
        _animator.SetInteger("playerState", 2);
    }

    void Flip()
    {
        _lookRight = !_lookRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin") == false)
        {
            return;
        }

        var objectComponent = collision.gameObject.GetComponent<CoinObject>();
        if (objectComponent == null)
        {
            return;
        }

        GameManager.Inst.IncreaseCoinScore();
        Destroy(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }
    }


}
