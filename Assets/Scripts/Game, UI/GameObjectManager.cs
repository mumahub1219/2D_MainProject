using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    // 생성할 몬스터의 프리팹
    [SerializeField] private GameObject Prefab_Enemy;
    [SerializeField] private Transform Root_Enemy;

    [SerializeField] private GameObject Prefab_SkillProjectile;
    [SerializeField] private Transform Root_SkillObject;

    public static GameObjectManager Inst { get; set; }

    // 생성된 오브젝트의 키가 됨
    private int _objectInstanceKeyGenerator = 0;
    private int _skillObjectInstanceKeyGenerator = 0;

    // 생성된 오브젝트의 생명을 보관
    private Dictionary<int, GameObject> _createdGameObjectContainer = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> _createdSkillObjectContainer = new Dictionary<int, GameObject>();
    private Dictionary<int, DaniTech_2DFieldObject> _fieldObjectContainer = new Dictionary<int, DaniTech_2DFieldObject>();

    private void Awake()
    {
        Inst = this;
    }

    public void RequestSpawnEnemy()
    {
        if(Prefab_Enemy == null)
        {
            Debug.LogWarning("프리팹이 등록되지 않은 오브젝트 입니다.");
            return;
        }

        var gObj = Instantiate(Prefab_Enemy, Root_Enemy);
        if(gObj == null)
        {
            Debug.LogWarning("생성에 실패한 게임 오브젝트 입니다.");
            return;
        }

        // 1-1 생성에 성공했다면, 미리 Key를 발급한다.
        _objectInstanceKeyGenerator++;

        // 1-2 Dictionary에 추가하기 전에 미리 키 검사한다
        if (_createdGameObjectContainer.ContainsKey(_objectInstanceKeyGenerator) == true)
        {
            Debug.LogWarning("이미 동일한 키가 발급된 게임 오브젝트가 존재합니다");
            return;
        }

        // 1-3 동적생성(실체화)된 오브젝트를 게임 오브젝트 매니저의 자료구조(Dictionary)에 보관하자!
        _createdGameObjectContainer.Add(_objectInstanceKeyGenerator, gObj);
        InitGeneratedEntityObject(_objectInstanceKeyGenerator, gObj);

        Debug.Log($"키: {_objectInstanceKeyGenerator}의 객체 {gObj.name}이 호출되었습니다.");
    }

    private void InitGeneratedEntityObject(int generatedId, GameObject gObj)
    {
        // 4-1 지금은 Enemy지만, 나중에 IGameEntity 같은 인터페이스로 개선하면 더 좋다
        DaniTech_2DEnemy gameEntity = gObj.GetComponent<DaniTech_2DEnemy>();
        if(gameEntity == null)
        {
            Debug.LogWarning($"생성된 {gObj.name}의 InstanceId를 대입할 수 있는 컴포넌트를 가져올 수 없습니다!");
            return;
        }

        // 4-2 생성된 객체에 정보를 부여한다!
        gameEntity.InitEnemyInfo(generatedId);
    }


    public GameObject GetEntityObjectCanBeNull(int instanceId)
    {
        if(_createdGameObjectContainer.ContainsKey(instanceId) == false)
        {
            Debug.LogWarning($"{instanceId}는 존재하지 않습니다.");
            return null;
        }

        // 2-1 실체화하면서 등록된 게임 오브젝트가 있다면 반환
        return _createdGameObjectContainer[instanceId];
    } 

    public void RequestDestroyEntityObject(int instanceId)
    {
        var gObj = GetEntityObjectCanBeNull(instanceId);
        if(gObj == null)
        {
            return;
        }

        // 3-1 요청된 객체를 제거함
        _createdGameObjectContainer.Remove(instanceId);
        Destroy(gObj);
    }

    // [스킬 오브젝트] ===================================================================================================

    public void RequestSpawnSkillObject(bool isRight, Vector3 startSkillPosition, int damage)
    {
        if (Prefab_SkillProjectile == null) return;

        var gObj = Instantiate(Prefab_SkillProjectile, Root_SkillObject);
        if (gObj == null) return;

        _skillObjectInstanceKeyGenerator++;

        var skillObj = gObj.GetComponent<SkillProjectile>(); // todo : 나중에 스킬 베이즈로 공용화 필요
        if (skillObj == null) return;
        skillObj.InitSkillObject(isRight, startSkillPosition, damage);


        if (_createdSkillObjectContainer.ContainsKey(_skillObjectInstanceKeyGenerator) == true) return;

        _createdSkillObjectContainer.Add(_skillObjectInstanceKeyGenerator, gObj);
        InitGeneratedSkillObject(_skillObjectInstanceKeyGenerator, gObj);
    }

    private void InitGeneratedSkillObject(int generatedId, GameObject gObj)
    {
        SkillProjectile skillObject = gObj.GetComponent<SkillProjectile>();
        if (skillObject == null) return;

        skillObject.InitSkillObjectInfo(generatedId);
    }

    public GameObject GetSkillObjectCanBeNull(int instanceId)
    {
        if (_createdSkillObjectContainer.ContainsKey(instanceId) == false) return null;

        return _createdSkillObjectContainer[instanceId];
    }

    public void RequestDestroySkillObject(int instanceId)
    {
        var gObj = GetSkillObjectCanBeNull(instanceId);
        if (gObj == null) return;

        _createdSkillObjectContainer.Remove(instanceId);
        Destroy(gObj);
    }


    //[필드 오브젝트] ====================================================================================================

    public async UniTaskVoid CreateFieldObject(string fieldObjectDataId, Transform spawnSpot)
    {
        var fieldObject = GameDataManager.Instance.GetFieldObjectData(fieldObjectDataId);
        if (fieldObject != null)
        {
            var createdObj = await ResourceManager.Inst.InstantiateAsync(fieldObject.PrefabPath, Root_Enemy, true);
            createdObj.transform.position = spawnSpot.position;
            AddFieldObjectOnCreate(createdObj, fieldObjectDataId);
        }
    }

    private void AddFieldObjectOnCreate(GameObject createdObject, string fieldObjectDataId)
    {
        _objectInstanceKeyGenerator++;
        var generatedInstanceId = _objectInstanceKeyGenerator;
        var fieldObject = createdObject.GetComponent<DaniTech_2DFieldObject>();

        if(fieldObject != null)
        {
            _fieldObjectContainer.Add(generatedInstanceId, fieldObject);
            fieldObject.InitFieldObjectInfoOnCreated(generatedInstanceId, fieldObjectDataId);
        }
    }

    public void RequestDestroyFieldObject(int instanceId)
    {
        var fieldObjectComponent = GetFieldObjectByInstanceId(instanceId);
        if (fieldObjectComponent == null)
        {
            return;
        }

        // 요청된 필드 오브젝트를 제거함
        _fieldObjectContainer.Remove(instanceId);
        Destroy(fieldObjectComponent.gameObject);
    }

    public DaniTech_2DFieldObject GetFieldObjectByInstanceId(int fieldObjectInstanceId)
    {
        if(_fieldObjectContainer.ContainsKey(fieldObjectInstanceId) == false)
        {
            Debug.LogError($"{fieldObjectInstanceId} 찾으려는 필드 오브젝트가 유효하지 않습니다");
            return null;
        }

        return _fieldObjectContainer[fieldObjectInstanceId];
    } 
}
