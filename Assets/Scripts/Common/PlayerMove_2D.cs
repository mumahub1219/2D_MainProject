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

    [Header("전투 관련 정보")]
    [SerializeField] private int _playerHp = 1000;
    [SerializeField] private int _playerBaseAtk = 100;

    private Rigidbody2D _rigidBody;
    private bool _isGrounded;
    private float _horizontalInput;
    private bool _lookRight = true;
    private bool _isSkillUsing = false;
    private int _playerInstancId = 0;

    public enum ViewType { sideView, TopView, }
    private Vector2 _lookDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Collider_PlayerNormalAttack.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameObjectManager.Inst.RegisterLocalPlayer(this);
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

    // 스킬 부분
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
        GameObjectManager.Inst.RequestSpawnSkillObjectFirst(_playerInstancId, _lookRight, transform.position, _playerBaseAtk, this.gameObject.tag, OnMonsterCollied);
    }

    IEnumerator CostartNoramalAttack()
    {
        _isSkillUsing = true;
        yield return new WaitForSeconds(1.0f);
        Collider_PlayerNormalAttack.gameObject.SetActive(false);
        _isSkillUsing = false;
    }

    // 상호 작용 부분
    public void TakeDamage(int damage)
    {
        _playerHp -= damage;
        Debug.Log(_playerHp);

        if (_playerHp - damage <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        GameManager.Inst.RespawnPlayer();
    }

    private void OnMonsterCollied(int monsterInstanceId, int skillDamage)
    {
        var monsterComponent = GameObjectManager.Inst.GetMonsterObjectInstanceId(monsterInstanceId);
        if (monsterComponent == null) return;

        monsterComponent.TakeDamage(skillDamage);
    }
}
