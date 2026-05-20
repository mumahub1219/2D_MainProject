using UnityEngine;

public class SkillProjectile : SkillBase
{

    [SerializeField] private SpriteRenderer SpriteRenderer_Effect;

    private Vector3 _moveDirection = new Vector3(1, 0 , 0);

    public void InitSkillObject(bool isDirectionRight, Vector3 playerPos)
    {
        this.transform.position = playerPos;

        _moveDirection = isDirectionRight ? new Vector3(1, 0 , 0) : new Vector3(-1, 0, 0);
        SpriteRenderer_Effect.flipX = !isDirectionRight;
        // SpriteRenderer_Effect.flipY = !isDirectionRight; Y축 전환이 필요한 경우 사용
    }

    private void Update()
    {
        transform.position += _moveDirection * 5.0f * Time.deltaTime;
    }


}
