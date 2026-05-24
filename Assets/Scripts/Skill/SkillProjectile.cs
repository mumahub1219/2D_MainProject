using System;
using System.Collections;
using UnityEngine;

public class SkillProjectile : SkillBase
{
    public int SkillObjectInstancId { get; private set; }

    [SerializeField] private SpriteRenderer SpriteRenderer_Effect;
    [SerializeField] private float ProjectileSpeed = 5.0f;

    private Vector3 _moveDirection = new Vector3(1, 0 , 0);

    private int _damage = 1;
    private int _ownerInstanceId;

    private event Action<int, int> _onSkillCollision;

    private void Update()
    {
        transform.position += _moveDirection * ProjectileSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        _onSkillCollision = null;
    }

    public void InitSkillObject(int ownerInstanceId, bool isDirectionRight, Vector3 playerPos, int damage, string parentTag, Action<int, int> onSkillCollision)
    {
        this.transform.position = playerPos;

        _moveDirection = isDirectionRight ? new Vector3(1, 0 , 0) : new Vector3(-1, 0, 0);
        SpriteRenderer_Effect.flipX = !isDirectionRight;
        // SpriteRenderer_Effect.flipY = !isDirectionRight; Y축 전환이 필요한 경우 사용

        _damage = damage;
        _ownerInstanceId = ownerInstanceId;
        _onSkillCollision = onSkillCollision;

        this.gameObject.tag = parentTag;
    }

    public void InitSkillObjectInfo(int instanceId)
    {
        SkillObjectInstancId = instanceId;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.collider);
    }

    private void CheckCollision(Collider2D collision)
    {
        bool isOwnerPlayer = (_ownerInstanceId == 0);

        if (collision.CompareTag("Player") && (isOwnerPlayer == false))
        {
            _onSkillCollision?.Invoke(0, _damage);

            GameObjectManager.Inst.RequestDestroySkillObjectFirstAndSecond(this.SkillObjectInstancId);
        }
        else if (collision.CompareTag("Enemy") && (isOwnerPlayer == true))
        {
            var gObj = collision.gameObject;
            if (gObj == null) return;

            var monsterComponent = gObj.GetComponent<MonsterBasic>();
            if (monsterComponent == null) return;

            var monsterInstanceId = monsterComponent.GetMonsterInstanceId();

            _onSkillCollision?.Invoke(monsterInstanceId, _damage);

            GameObjectManager.Inst.RequestDestroySkillObjectFirstAndSecond(this.SkillObjectInstancId);
        }
        else if (collision.CompareTag("Map") || collision.CompareTag("Spike"))
        {
            GameObjectManager.Inst.RequestDestroySkillObjectFirstAndSecond(this.SkillObjectInstancId);
        }
    }
}
