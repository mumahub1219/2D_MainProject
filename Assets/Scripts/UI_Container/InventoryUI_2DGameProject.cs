using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_2DGameProject : UIBase
{
    [Header("버튼")]
    [SerializeField] private UIButton Button_Close;

    [Header("프리팹")]
    [SerializeField] private GameObject Prefab_Slot;

    [Header("슬롯 리스트 영역")]
    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<string, InventorySlotUI> _slotList = new Dictionary<string, InventorySlotUI>();

    private void OnEnable()
    {
        Button_Close.BindOnClickButtonEvent(Onclick_CloseInventory);
    }

    public void Onclick_CloseInventory()
    {
        UIManager.Instance.CloseContentUI(UIType.InventoryUI);
    }

    private void CreateInventorySlot(string dataId)
    {
        var gObj = Instantiate(Prefab_Slot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<InventorySlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(dataId, OnclickChildSlotSelected);
        _slotList.Add(dataId, slotComponent);
    }

    private void OnclickChildSlotSelected(string slotDataId)
    {
        
    }
}
