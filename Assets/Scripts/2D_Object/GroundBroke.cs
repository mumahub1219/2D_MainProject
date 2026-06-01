using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundBroke : MonoBehaviour
{
    [Header("타일 설정")]
    [SerializeField] private TileBase _animatedBreakTile;
    [SerializeField] private float _destroyDelay = 0.5f;
    [SerializeField] private float _restoreDelay = 3.0f;

    private Tilemap _tilemap;
    private Dictionary<Vector3Int, TileBase> _tileList = new Dictionary<Vector3Int, TileBase>();

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleTileBreak(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleTileBreak(collision);
    }

    private void HandleTileBreak(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contactCount > 0 && collision.contacts[0].normal.y < -0.5f)
            {
                Vector3 hitPosition = collision.contacts[0].point;

                Vector3Int tilePosition = _tilemap.WorldToCell(hitPosition + new Vector3(0, -0.1f, 0));

                if (_tilemap.HasTile(tilePosition) == true)
                {
                    TileBase currentTile = _tilemap.GetTile(tilePosition);

                    if (_tilemap.GetTile(tilePosition) != _animatedBreakTile)
                    {
                        StartCoroutine(CoBreakAndRestoreTileRoutine(tilePosition, currentTile));
                    }
                }
            }
        }
    }

    private IEnumerator CoBreakAndRestoreTileRoutine(Vector3Int tilePosition, TileBase tileList)
    {
        _tileList.TryAdd(tilePosition, tileList);

        _tilemap.SetTile(tilePosition, _animatedBreakTile);

        yield return new WaitForSeconds(_destroyDelay);
        _tilemap.SetTile(tilePosition, null);

        yield return new WaitForSeconds(_restoreDelay);

        if (_tileList.ContainsKey(tilePosition) == true)
        {
            _tilemap.SetTile(tilePosition, _tileList[tilePosition]);
            _tileList.Remove(tilePosition);
        }
    }
}
