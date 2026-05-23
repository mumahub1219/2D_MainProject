using UnityEngine;

public enum SpawnSpotType
{
    None = 0,
    Harvest,
    DropItem,
    Dialogue,
    Monster,
    fieldItem
}

public enum StartSpawnType
{
    None = 0,
    OnAwake,
    OnEnable,
    OnRange,
    // UniTask나 코루틴으로 일정 시간마다 랜덤 생성도 구현해보자
}

public class SpawnSpot : MonoBehaviour
{
    [SerializeField] private SpawnSpotType _spawnSpotType;
    [SerializeField] private StartSpawnType _startSpawnType;

    [SerializeField] private string _spawnObjectDataId;
    [SerializeField] private Collider2D Collider_OnSpawnStart;

    private void Awake()
    {
        if(_startSpawnType == StartSpawnType.OnAwake)
        {
            StartSpawn();
        }
    }

    private void Start()
    {
        if (_startSpawnType == StartSpawnType.OnEnable)
        {
            StartSpawn();
        }


        if (Collider_OnSpawnStart != null)
        {
            Collider_OnSpawnStart.enabled = (_startSpawnType == StartSpawnType.OnRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        // TODO - 개선점
        // 이미 스폰된 객체가 있다면, 해당 객체가 사라질때까지 추가적인 스폰을 하지 않도록 추가 처리해야한다

        switch (_spawnSpotType)
        {
            case SpawnSpotType.Harvest:
                break;
            case SpawnSpotType.fieldItem:
                GameObjectManager.Inst.CreateItemObject(_spawnObjectDataId, this.transform).Forget();
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.DropItem:
                GameObjectManager.Inst.CreateFieldObject(_spawnObjectDataId, this.transform).Forget();
                // 추가처리가 들어가기 까지는 해당 스폰스팟이 더이상 동작하지 않게 비활성화 한다
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.Monster:
                GameObjectManager.Inst.CreateMonsterObject(_spawnObjectDataId, this.transform).Forget();
                break;
            case SpawnSpotType.Dialogue:
                // 다이얼로그 발생 유형은 시작 시 이 스폰스팟을 더이상 사용하지 않게 비활성화 한다 (제거도 무관)
                UIManager.Instance.OpenDialogueUI(_spawnObjectDataId);
                this.gameObject.SetActive(false);
                break;
        }
    }

}
