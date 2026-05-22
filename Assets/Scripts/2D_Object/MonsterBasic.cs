using System.Collections;
using UnityEngine;

public class MonsterBasic : MonsterBase
{
    [Header("데이터 세팅 부분")]
    public float SkillCoolTime;

    [SerializeField] private SpriteRenderer SpriteRenderer_Monster;

    private int _instanceId;
    private string _dataId;

    private MonsterData _monsterData;
    private int _baseHp;
    private int _baseAtk;
    private bool _isAlive = true;
    private bool _lookRight = true;
    private Vector3 _moveDirection;

    private void OnDisable()
    {
        _isAlive = false;
    }

    public void InitMonster(int instanceId, string dataId)
    {
        _instanceId = instanceId;
        _dataId = dataId;

        var monsterData = GameDataManager.Instance.GetMonsterData(dataId);
        if (monsterData != null)
        {
            _monsterData = monsterData;
            _baseHp = _monsterData.BaseHp;
            _baseAtk = _monsterData.BaseAtk;
        }

        StartCoroutine(CheckAndUseSkill());
    }

    public int GetMonsterInstanceId()
    {
        return _instanceId;
    }

    // 데미지 부분
    private int GetFinalNormalDamage(int baseAtk, float normalMultiple)
    {
        return (int)(baseAtk * normalMultiple);
    }

    private int GetFinalSkillDamage(int baseAtk, float skillMultiple)
    {
        return (int)(baseAtk * skillMultiple);
    }

    

    // 방향 부분
    private void ChangeMonsterDirection() 
    {
        _lookRight = !_lookRight;
        _moveDirection = new Vector3(_lookRight ? 1 : -1, 0, 0);
        SetMeshDirectionByMoveDirection((int)_moveDirection.x);
    }

    // 스킬 부분
    IEnumerator CheckAndUseSkill()
    {
        while (_isAlive)
        {
            yield return new WaitForSeconds(SkillCoolTime);

            if (_isAlive == false)
            {
                break;
            }

            ChangeMonsterDirection();
            UseSkill();
        }
    }

    private void SetMeshDirectionByMoveDirection(int x)
    {
        SpriteRenderer_Monster.flipX = (x < 0);
    }

    private void UseSkill()
    {
        float skillMultiple = _monsterData.SkillAtkMultipleList.Count > 0 ? _monsterData.SkillAtkMultipleList[0] : 0;
        int finalSkillDamage = GetFinalSkillDamage(_baseAtk, skillMultiple);
        GameObjectManager.Inst.RequestSpawnSkillObjectSecond(_instanceId, _lookRight, this.transform.position, finalSkillDamage, this.gameObject.tag, OnSkillCollision);
    }

    private void OnSkillCollision(int colliedObjectInstanceId, int damage)
    {
        if (colliedObjectInstanceId == 0)
        {
            var player = GameObjectManager.Inst.GetLocalPlayer();

            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        _baseHp -= damage;

        if (_baseHp <= 0)
        {
            GameObjectManager.Inst.RequestDestroyMonsterObject(_instanceId);
        }
    }
}
