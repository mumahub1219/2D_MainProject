using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundBrokeParticle : MonoBehaviour
{
    private Tilemap _tilemap;
    [SerializeField] private ParticleSystem _breakEffect;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // [디버깅 1] 무언가와 충돌하긴 했는지 확인
        Debug.Log($"[발판 충돌 감지] 부딪힌 오브젝트: {collision.gameObject.name}, 태그: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Player"))
        {
            // [디버깅 2] 플레이어 태그는 맞춤
            Debug.Log("[발판 충돌 감지] 플레이어 태그 확인 완료!");

            if (collision.contactCount > 0)
            {
                float normalY = collision.GetContact(0).normal.y;
                // [디버깅 3] 충돌 방향(기하학적 수치) 확인
                Debug.Log($"[발판 충돌 감지] 충돌 normal.y 값: {normalY} (이 값이 -0.5보다 작아야 바닥으로 인정됩니다)");

                if (normalY < -0.5f)
                {
                    Vector3 hitPosition = collision.GetContact(0).point;
                    Vector3Int tilePosition = _tilemap.WorldToCell(hitPosition + new Vector3(0, -0.1f, 0));

                    // [디버깅 4] 최종 변환된 타일 좌표 확인
                    Debug.Log($"[발판 충돌 감지] 계산된 타일 좌표: {tilePosition}");

                    BreakGround(tilePosition);
                }
            }
        }
    }

    public void BreakGround(Vector3Int tilePosition)
    {
        TileBase tile = _tilemap.GetTile(tilePosition);
        if (tile == null)
        {
            Debug.LogWarning($"[발판 오류] {tilePosition} 좌표에 타일이 존재하지 않습니다! (null)"); return;
        }

        Debug.Log($"[발판 파괴 실행] {tile.name} 타일을 삭제하고 파티클을 생성합니다.");

        Vector3 worldPos = _tilemap.CellToWorld(tilePosition) + _tilemap.tileAnchor;

        if (_breakEffect != null)
        {
            Instantiate(_breakEffect, worldPos, Quaternion.identity);

            Destroy(_breakEffect.gameObject, _breakEffect.main.duration + _breakEffect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogError("[발판 오류] 인스펙터 창에서 _breakEffect(파티클 프리팹)가 비어있습니다!");
        }

        _tilemap.SetTile(tilePosition, null);
    }
}