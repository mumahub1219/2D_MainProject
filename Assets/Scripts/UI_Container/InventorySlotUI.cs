using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_Name;


    [SerializeField] private GameObject GObj_Selected;
    [SerializeField] private UIButton Button_SlotClick;

    private event Action<int> OnSelectSlot;

    public int SlotInstanceId { get; private set; }

    private string _slotDataId;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public string GetSlotDataId()
    {
        return _slotDataId;
    }

    public void InitSlot(string dataId, Action<string> OnclickCallback)
    {
        
    }

    public void ChangeSelectedState(bool isSelected)
    {

    }
}
