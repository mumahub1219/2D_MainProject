using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; set; }

    // 플레이 중에 저장되어야 하는 정보들이 있는 위치
    private DaniTechPlayerModel _playerModel = new DaniTechPlayerModel();

    private int _CoinScore;

    [SerializeField] private Vector3 _respawnPosition;
    [SerializeField] private Vector3 _startRespawnPosition;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        StartCoroutine(InitGameLoop());
    }

    public void SaveData()
    {
        NetworkManager.Inst.RequstSaveData(_playerModel);
    }

    public void SaveAndEndGame()
    {
        SaveData();
        Application.Quit();
    }

    private void LoadSaveData()
    {
        _playerModel = NetworkManager.Inst.RequstLoadSaveData();
    }

    // 플레이어 정보 받기
    private PlayerMove_2D GetPlayerInfo()
    {
        return GameObjectManager.Inst.GetLocalPlayer();
    }

    public void IncreasePlayerExp(int exp)
    {
        // 추후에 한곳에서 관리할 수 있게 익스텐션으로 빼도 된다
        _playerModel.PlayerTotalExp += exp;
    }

    // 스코어 UI 부분
    public void IncreaseCoinScore()
    {
        _CoinScore++;
        UpdateScoreUI();
        EndGameCondition();
    }

    public void UpdateScoreUI()
    {
        var obj = UIManager.Instance.GetComponentInChildren<ScoreUI>();

        if (obj == null)
        {
            Debug.LogError("obj가 널입니다.");
            return;
        }

        obj.CurrentCoinScore(_CoinScore);
    }

    public void EndGameCondition()
    {
        bool isCoinScoreEnough = (_CoinScore == 5);
        if (isCoinScoreEnough)
        {
            Application.Quit();
        }
    }

    // 플레이어 리스폰 부분
    public void RespawnSpot()
    {
        var localPlayer = GetPlayerInfo();

        if (localPlayer != null)
        {
            var currentPosition = localPlayer.transform.position;
            _respawnPosition = currentPosition;
            _startRespawnPosition = currentPosition;
        }
    }

    private IEnumerator InitGameLoop()
    {
        LoadSaveData();

        var localPlayer = GetPlayerInfo();

        while (localPlayer == null)
        {
            yield return null;
        }

        RespawnSpot();
    }

    public void RespawnPlayer()
    {
        var localPlayer = GetPlayerInfo();
        if (localPlayer == null) return;

        if (localPlayer.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0.0f;
        }

        localPlayer.transform.position = _respawnPosition;
        localPlayer.SetPlayerHp();
    }

    public void SetRespawnPosition(Vector3 newPosition)
    {
        _respawnPosition = newPosition;
    }

    public void InitializaionRespawnSpot()
    {
        var localPlayer = GetPlayerInfo();
        if (localPlayer == null) return;

        localPlayer.transform.position = _startRespawnPosition;
        _respawnPosition = _startRespawnPosition;
    }

    // 아이템 추가
    public void AddItem(string itemDataId, int addItemCount)
    {
        // 저장할때 고유값 ID를 부여하기 위해 사용
        long uniqueId = GameUtil.GenerateUniqueId();

        // TODO : 우선 쉽게 사용할 수 있도록 중복 처리는 빼두었다. 습득할때마다 아이템이 하나씩 추가되도록 해두고
        // 추후에 중복값은 StackCount가 다 찰때까지 누적해줄 수 있도록 로직을 추가하자
        var newItem = new DaniTechItemModel();
        newItem.ItemUniqueId = uniqueId;
        newItem.ItemDataId = itemDataId;
        newItem.ItemStackCount = addItemCount;

        _playerModel.ItemList.Add(newItem);
    }

    public List<DaniTechItemModel> GetPlayerItemList()
    {
        // _playerModel이 Private이므로 외부에서 ItemList를 받아올 수 있게 Get함수를 사용한다
        return _playerModel.ItemList;
    }
}
