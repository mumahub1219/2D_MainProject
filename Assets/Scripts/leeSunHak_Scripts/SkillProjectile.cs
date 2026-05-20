using UnityEngine;

public class SkillProjectile : SkillBase
{

    [SerializeField] private SpriteRenderer SpriteRenderer_Effect;
    [SerializeField] private float ProjectileSpeed = 5.0f;

    private Vector3 _moveDirection = new Vector3(1, 0 , 0);
    private int _damage;

    public void InitSkillObject(bool isDirectionRight, Vector3 playerPos, int damage)
    {
        this.transform.position = playerPos;

        _moveDirection = isDirectionRight ? new Vector3(1, 0 , 0) : new Vector3(-1, 0, 0);
        SpriteRenderer_Effect.flipX = !isDirectionRight;
        // SpriteRenderer_Effect.flipY = !isDirectionRight; Y축 전환이 필요한 경우 사용

        _damage = damage;

    }

    private void Update()
    {
        transform.position += _moveDirection * ProjectileSpeed * Time.deltaTime;
    }
}
