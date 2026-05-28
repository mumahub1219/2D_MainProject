using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Image Image_Frame;
    [SerializeField] private Image Image_Selected;
    [SerializeField] private Text Text_StackCount;
    [SerializeField] private UIButton Button_Slot;

    private event Action<long> OnSelectEvent;

    public long SlotItemUniqueId { get; private set; }
    public bool IsUsableItem { get; private set; }

    private string _slotDataId;

    private void OnEnable()
    {
        Image_Selected.gameObject.SetActive(false);
        Button_Slot.BindOnClickButtonEvent(OncClick_SelectItem);
    }

    private void OnDisable()
    {
        OnSelectEvent = null;
    }

    public void SetIcon(string itemDataId, int itemCount)
    {
        var itemData = GameDataManager.Instance.GetItemData(itemDataId);
        if (itemData == null) return;

        IsUsableItem = (string.IsNullOrEmpty(itemData.UseItemType) == false);

        string iconPath = itemData.IconPath;
        if (string.IsNullOrEmpty(iconPath) == true) return;

        GameUtil.LoadAndSetSpriteImage(Image_MainIcon, iconPath).Forget();

        Text_StackCount.text = $"{itemCount}";
    }

    public string GetSlotDataId()
    {
        return _slotDataId;
    }

    public void InitSlot(long slotUniqueId, string itemDataId, int itemStackCount)
    {
        SlotItemUniqueId = slotUniqueId;
        SetIcon(itemDataId, itemStackCount);
    }

    public void OncClick_SelectItem()
    {
        OnSelectEvent?.Invoke(SlotItemUniqueId);

        // 툴팁, 팝업도 여기서 띄워주기
    }

    public void BindSlotSelectEvent(Action<long> onSelectEvent)
    {
        OnSelectEvent = onSelectEvent;
    }

    public void ChangeSelectedState(bool isSelected)
    {
        Image_Selected.gameObject.SetActive(isSelected);
    }
}
