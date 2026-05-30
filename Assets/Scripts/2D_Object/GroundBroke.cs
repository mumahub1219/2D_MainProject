using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundBroke : MonoBehaviour
{
    [Header("타일 설정")]
    [SerializeField] private TileBase _animatedBreakTile;
    [SerializeField] private float _destroyDelay = 0.5f;

    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contactCount > 0 && collision.contacts[0].normal.y < -0.5f)
            {
                Vector3 hitPosition = collision.contacts[0].point;

                Vector3Int tilePosition = _tilemap.WorldToCell(hitPosition + new Vector3(0, -0.1f, 0));

                if (_tilemap.HasTile(tilePosition))
                {
                    if (_tilemap.GetTile(tilePosition) != _animatedBreakTile)
                    {
                        StartCoroutine(CoBreakTileRoutine(tilePosition));
                    }
                }
            }
        }
    }

    private IEnumerator CoBreakTileRoutine(Vector3Int tilePosition)
    {
        _tilemap.SetTile(tilePosition, _animatedBreakTile);

        yield return new WaitForSeconds(_destroyDelay);

        _tilemap.SetTile(tilePosition, null);
    }
}
