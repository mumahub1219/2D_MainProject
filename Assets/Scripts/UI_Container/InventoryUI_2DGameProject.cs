using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_2DGameProject : UIBase
{
    [Header("버튼")]
    [SerializeField] private UIButton Button_Close;
    [SerializeField] private UIButton Button_CloseBG;
    [SerializeField] private UIButton Button_UseSelectItem;

    [Header("프리팹")]
    [SerializeField] private GameObject Prefab_Slot;

    [Header("슬롯 리스트 영역")]
    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<long, InventorySlotUI> _itemSlotList = new Dictionary<long, InventorySlotUI>();
    private long _currentSelectedUniqueId;
        
    private void OnEnable()
    {
        Button_Close.BindOnClickButtonEvent(Onclick_CloseInventoryUI);
        Button_CloseBG.BindOnClickButtonEvent(Onclick_CloseInventoryUI);
        Button_UseSelectItem.BindOnClickButtonEvent(OnclickUseSelectItem);
        SetInventoryItemSlotOnEnable();

        Button_UseSelectItem.gameObject.SetActive(false);
    }

    public void Onclick_CloseInventoryUI()
    {
        UIManager.Instance.CloseContentUI(UIType.InventoryUI);
    }

    private void SetInventoryItemSlotOnEnable()
    {
        if (_itemSlotList.Count > 0)
        {
            foreach (var slot in _itemSlotList)
            {
                DestroyImmediate(slot.Value.gameObject);
            }
            _itemSlotList.Clear();
        }

        var itemList = GameManager.Inst.GetPlayerItemList();
        if (itemList == null || itemList.Count == 0) return;

        foreach (var itemModel in itemList)
        {
            CreateInventorySlot(itemModel.ItemUniqueId,itemModel.ItemDataId, itemModel.ItemStackCount);
        }
    }

    private void CreateInventorySlot(long itemUniqueId,string dataId, int itemStackCount)
    {
        var gObj = Instantiate(Prefab_Slot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<InventorySlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(itemUniqueId, dataId, itemStackCount);
        slotComponent.gameObject.name = $"itemslot : {slotComponent.SlotItemUniqueId}";

        _itemSlotList.Add(slotComponent.SlotItemUniqueId, slotComponent);

        slotComponent.BindSlotSelectEvent(OnclickChildSlotSelected);
    }

    private void OnclickChildSlotSelected(long selectedSlotUniqueId)
    {
        foreach (var slotKv in _itemSlotList)
        {
            var slot = slotKv.Value;
            bool isSlotSelected = (selectedSlotUniqueId == slot.SlotItemUniqueId);
            slot.ChangeSelectedState(isSlotSelected);

            if(slot.IsUsableItem == true)
            {
                _currentSelectedUniqueId = slot.SlotItemUniqueId;
                Button_UseSelectItem.gameObject.SetActive(slot.IsUsableItem);
            }
        }
    }

    public void OnclickUseSelectItem()
    {
        RequestSelecUseItem();
    }

    private void RequestSelecUseItem()
    {
        bool isItemRemoved = GameManager.Inst.RequestUseItem(_currentSelectedUniqueId);
        if (isItemRemoved == true)
        {
            RemoveItemSlot(_currentSelectedUniqueId);
            _currentSelectedUniqueId = 0;
            Button_UseSelectItem.gameObject.SetActive(false);
        }
    }

    private void RemoveItemSlot(long removedItemUniqueId)
    {
        if (_itemSlotList.ContainsKey(removedItemUniqueId) == false)
        {
            Debug.LogError("제거된 아이템 슬롯을 찾을 수 없음!!");
            return;
        }

        var slotComponent = _itemSlotList[removedItemUniqueId];
        _itemSlotList.Remove(removedItemUniqueId);
        Destroy(slotComponent.gameObject);
    }
}
