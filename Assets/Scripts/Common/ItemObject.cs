using UnityEngine;

public class ItemObject : ItemBase
{
    private int _itemObjectInstanceId;
    private string _itemObjectDataId;
    private string _itemObjectName;

    public void InitItemObjectInfoOncreated(int instanceId, string itemObjectDataId)
    {
        _itemObjectInstanceId = instanceId;
        _itemObjectDataId  = itemObjectDataId;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkillObject"))
        {
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            GameManager.Inst.AddItem(_itemObjectDataId, 1);

            GameObjectManager.Inst.RequestDestroyItemObject(this._itemObjectInstanceId);

            var inventory = UIManager.Instance.GetComponentInChildren<InventoryUI_2DGameProject>();
            if (inventory != null)
            {
                inventory.RefreshInventorySlots();
            }
        }
    }
}
