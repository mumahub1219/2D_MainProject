using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;


public class DictionaryUI : UIBase
{
    [Header("버튼")]
    [SerializeField] private UIButton Button_CloseBG;
    [SerializeField] private UIButton Button_Close;

    [Header("프리팹")]
    [SerializeField] private GameObject Prefab_Slot;

    [Header("디테일 정보 영역")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_Name;
    [SerializeField] private Text Text_Description;

    [Header("슬롯 리스트 영역")]
    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<string, DictionarySlotUI> _slotList = new Dictionary<string, DictionarySlotUI>(); 

    private void OnEnable()
    {
        ReadItemListAndCreateSlot();

        Button_CloseBG.BindOnClickButtonEvent(Onclick_CloseDictionary);
        Button_Close.BindOnClickButtonEvent(Onclick_CloseDictionary);
    }

    private void OnDisable()
    {
        if (_slotList.Count > 0)
        {
            foreach (var slotKv in _slotList)
            {
                var slot = slotKv.Value;
                DestroyImmediate(slot.gameObject);
            }

            _slotList.Clear();
        }
    }

    private void Onclick_CloseDictionary()
    {
        UIManager.Instance.CloseContentUI(UIType.DictionaryUI);
    }

    private void ReadItemListAndCreateSlot()
    {
        var dataList = GameDataManager.Instance.ItemDataList;
        foreach (var dataKv in dataList)
        {
            var data = dataKv.Value;
            if (data == null)
            {
                continue;
            }

            CreateDictionarySlot(data.Id);
        }
            
        //if (_slotList.Count > 0)
        //{
        //    foreach (var slotKv in _slotList)
        //    {
        //        var slot = slotKv.Value;
        //        slot.Onclick_DictionarySlot();
        //    }
        //}
    }

    private void CreateDictionarySlot(string dataId)
    {
        var gObj = Instantiate(Prefab_Slot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<DictionarySlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(dataId, OnclickChildSlotSelected);
        _slotList.Add(dataId, slotComponent);
    }

    private void OnclickChildSlotSelected(string slotDataId)
    {
        var currentSelectedData = GameDataManager.Instance.GetItemData(slotDataId);
        if (currentSelectedData == null) return;

        Text_Name.text = currentSelectedData.Name;
        Text_Description.text = currentSelectedData.Description;
        GameUtil.LoadAndSetSpriteImage(Image_MainIcon, currentSelectedData.IconPath).Forget();

        foreach (var slotKv in _slotList)
        {
            var slot = slotKv.Value;
            var dataId = slot.GetSlotDataId();
            slot.SetSelected(slotDataId == dataId);
        }
    }
}
