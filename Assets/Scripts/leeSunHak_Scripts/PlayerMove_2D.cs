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

    [Header("애니메이터")]
    [SerializeField] private AnimatorController_2D AnimatiorController_Entitiy;

    [Header("스킬")]
    [SerializeField] private Collider2D Collider_PlayerNormalAttack;

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

        UpdateAnimationState();
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

    private void ChangePlayerState(EntityAnimState newState)
    {
        AnimatiorController_Entitiy.SetState(newState);
    }

    private void UpdateAnimationState()
    {
        if (_isGrounded == false)
        {
            ChangePlayerState(EntityAnimState.Jump);
        }
        else
        {
            if (_horizontalInput != 0)
            {
                ChangePlayerState(EntityAnimState.Run);
            }
            else
            {
                ChangePlayerState(EntityAnimState.Idle);
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") == false) return;
        
        var objectComponent = collision.gameObject.GetComponent<BladeObject>();
        if (objectComponent == null) return;

        Debug.Log("가시에 닿았습니다!");
        GameManager.Inst.RestartPlayer();
    }


    public bool CheckSKillUseable(bool isShowMsg = true)
    {
        if (_isSkillUsing == true)
        {
            if(isShowMsg == true)
            {
                UIManager.Instance.OpenSimplePopup("스킬이 이미 사용 중입니다");
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
        GameObjectManager.Inst.RequestSpawnSkillObject(_lookRight, transform.position);
        
    }


    IEnumerator CostartNoramalAttack()
    {
        _isSkillUsing = true;
        yield return new WaitForSeconds(1.0f);
        Collider_PlayerNormalAttack.gameObject.SetActive(false);
        _isSkillUsing = false;
    }

}
