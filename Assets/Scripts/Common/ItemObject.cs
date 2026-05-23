using UnityEngine;

public class ItemObject : ItemBase
{
    [SerializeField] private int _itemObjectInstanceId;
    [SerializeField] private string _itemObjectDataId;
    [SerializeField] private string _itemObjectName;


    public void InitItemObjectInfoOncreated(int instanceId, string itemObjectDataId)
    {

    }

}
