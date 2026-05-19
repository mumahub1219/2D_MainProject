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

        RunAnimation(Mathf.Abs(_horizontalInput));
        JumpAnimation();
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        Move();
    }

    void Move()
    {
        _rigidBody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, _rigidBody.linearVelocity.y);
    }

    void Jump()
    {
        _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, _jumpForce);
    }

    void Flip()
    {
        _lookRight = !_lookRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }
    }

    public void JumpAnimation()
    {
        if (_isGrounded == false)
        {
            _animator.SetBool("jump", true);
        }
        else
        {
            _animator.SetBool("jump", false);
        }
    }

    public void RunAnimation(float currentSpeed)
    {
        _animator.SetFloat("speed", currentSpeed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") == false)
        {
            return;
        }

        var objectComponent = collision.gameObject.GetComponent<SpikeObject>();
        if (objectComponent == null)
        {
            return;
        }

        GameManager.Inst.RestartPlayer();
    }
}
