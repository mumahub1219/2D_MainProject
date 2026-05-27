using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_2DGameProject : UIBase
{
    [Header("버튼")]
    [SerializeField] private UIButton Button_Close;
    [SerializeField] private UIButton Button_UseSelectItem;

    [Header("프리팹")]
    [SerializeField] private GameObject Prefab_Slot;

    [Header("슬롯 리스트 영역")]
    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<string, InventorySlotUI> _itemSlotList = new Dictionary<string, InventorySlotUI>();

    private void OnEnable()
    {
        Button_Close.BindOnClickButtonEvent(Onclick_CloseInventoryUI);
        SetInventoryItemSlotOnEnable();
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
    }

    private void CreateInventorySlot(string dataId)
    {
        var gObj = Instantiate(Prefab_Slot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<InventorySlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(dataId, OnclickChildSlotSelected);
        _itemSlotList.Add(dataId, slotComponent);
    }

    private void OnChideSlotSelected(int selectedSlotInstanceId)
    {
        foreach (var slotKv in _itemSlotList)
        {
            var slot = slotKv.Value;
            bool isSlotSelected = (selectedSlotInstanceId == slot.SlotInstanceId);
            slot.ChangeSelectedState(isSlotSelected);
        }
    }

    private void OnclickChildSlotSelected(string slotDataId)
    {
        
    }

    public void OnclickUseSelectItem(int selectedSlotInstanceId)
    {
        
    }
}
