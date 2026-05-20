using System.Collections;
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

    [Header("스킬")]
    [SerializeField] private Collider2D Collider_PlayerNormalAttack;
    [SerializeField] private GameObject Prefab_SkillProjectile;
    [SerializeField] private Transform Transform_SkillProjectileRoot;

    private Rigidbody2D _rigidBody;
    private bool _isGrounded;
    private float _horizontalInput;
    private bool _lookRight = true;
    private bool _isSkillUsing = false;

    public enum ViewType { sideView, TopVie, }

    private Vector2 _lookDirection;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Collider_PlayerNormalAttack.gameObject.SetActive(false);
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
        if (collision.gameObject.CompareTag("Spike") == false) return;
        
        var objectComponent = collision.gameObject.GetComponent<SpikeObject>();
        if (objectComponent == null) return;

        GameManager.Inst.RestartPlayer();
    }


    public bool CheckSKillUseable(bool isShowMsg = true)
    {
        if (_isSkillUsing == true)
        {
            if(isShowMsg == true)
            {
                UIManager.Instance.OpenSimplePopup("스킬이 이이 사용 중입니다");
            }

            return false;
        }


        return true;
    }

    public void UseNormalAttack()
    {
        if(CheckSKillUseable(isShowMsg:false) == false) return;


        Collider_PlayerNormalAttack.gameObject.SetActive(true);
        StartCoroutine(CostartNoramalAttack());
    }

    public void UseUseCircleSkill()
    {
        if (CheckSKillUseable() == false) return;
        

    }

    public void UseRaySkill()
    {
        if (CheckSKillUseable() == false) return;
        

    }

    public void ProjectileSkill()
    {
        if (CheckSKillUseable() == false) return;
        CoreateProjectileSkillObject();

    }

    private void CoreateProjectileSkillObject()
    {
        var gObj = Instantiate(Prefab_SkillProjectile, Transform_SkillProjectileRoot);
        if (gObj == null) return;

        var skillProjectile = gObj.GetComponent<SkillProjectile>();
        if (skillProjectile == null) return;

        skillProjectile.InitSkillObject(_lookRight, this.transform.position);

    }

    IEnumerator CostartNoramalAttack()
    {
        _isSkillUsing = true;
        yield return new WaitForSeconds(1.0f);
        Collider_PlayerNormalAttack.gameObject.SetActive(false);
        _isSkillUsing = false;
    }

}
