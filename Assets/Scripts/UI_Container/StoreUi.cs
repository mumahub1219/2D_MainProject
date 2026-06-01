using System.Collections.Generic;
using UnityEngine;

public class StoreUi : MonoBehaviour
{
    [Header("버튼 영역")]
    [SerializeField] private UIButton Button_CloseBG;
    [SerializeField] private UIButton Button_Close;
    [SerializeField] private UIButton Button_BuyItem;

    [Header("슬롯 관련 영역")]
    [SerializeField] private GameObject Prefab_Slot;
    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<string, StoreSlotUI> _storeItemSlotList = new Dictionary<string, StoreSlotUI>();
    private string _currentSelectedDataId;
    private bool _isPurchase = false;

    private void OnEnable()
    {
        Button_CloseBG.BindOnClickButtonEvent(OnClick_CloseStoreUI);
        Button_Close.BindOnClickButtonEvent(OnClick_CloseStoreUI);
        Button_BuyItem.BindOnClickButtonEvent(OnClick_BuyItem);

        ClearStoreSlotList();

        _isPurchase = false;

        ReadItemListAndCreateSlot();
    }

    private void OnDisable()
    {
        ClearStoreSlotList();
    }

    private void OnClick_CloseStoreUI()
    {
        UIManager.Instance.CloseContentUI(UIType.StoreUI);
    }

    private void ReadItemListAndCreateSlot()
    {
        var dataList = GameDataManager.Instance.ItemDataList;
        if (dataList == null) return;

        foreach (var dataKv in dataList)
        {
            var data = dataKv.Value;
            if (data == null)
            {
                continue;
            }

            CreateStoreItemSlotList(data.Id);
        }
    }

    private void CreateStoreItemSlotList(string dataId)
    {
        var gObj = Instantiate(Prefab_Slot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<StoreSlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(dataId, OnclickChildSlotSelected);
        _storeItemSlotList.Add(dataId, slotComponent);
    }

    private void OnclickChildSlotSelected(string selectedSlotDataId)
    {
        if (_isPurchase == true) return;

        bool isCurrentSelectedUsable = false;
        foreach (var slotKv in _storeItemSlotList)
        {
            var slot = slotKv.Value;
            bool isSlotSelected = (selectedSlotDataId == slot.GetSlotDataId());
            slot.ChangeSelectedState(isSlotSelected);

            if (isSlotSelected == true)
            {
                isCurrentSelectedUsable = slot.IsUsableItem;

                if (slot.IsUsableItem == true)
                {
                    _currentSelectedDataId = slot.GetSlotDataId();
                }
                else
                {
                    _currentSelectedDataId = string.Empty;
                }
            }
        }

        Button_BuyItem.gameObject.SetActive(isCurrentSelectedUsable);
    }

    private void ClearStoreSlotList()
    {
        if (_storeItemSlotList.Count > 0)
        {
            foreach (var slotKv in _storeItemSlotList)
            {
                var slot = slotKv.Value;
                Destroy(slot.gameObject);
            }

            _storeItemSlotList.Clear();
        }
    }

    private void OnClick_BuyItem()
    {
        if (string.IsNullOrEmpty(_currentSelectedDataId) == true) return;
        if (_isPurchase == true) return;

        bool isBuyItem = GameManager.Inst.RequestBuyItem(_currentSelectedDataId);

        if (isBuyItem == true) 
        {
            _isPurchase = true;
            Button_BuyItem.gameObject.SetActive(false);

            LockAllStoreSlot();
        }
    }

    private void LockAllStoreSlot()
    {
        foreach (var slotKv in _storeItemSlotList)
        {
            slotKv.Value.ChangeSelectedState(false);
            slotKv.Value.SetDisableSlot();
        }
    }
}