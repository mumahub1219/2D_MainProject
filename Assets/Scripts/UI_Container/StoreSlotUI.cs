using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlotUI : UIBase
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Image Image_Frame;
    [SerializeField] private Image Image_Selected;
    [SerializeField] private UIButton Button_Slot;

    [Header("설명 영역")]
    [SerializeField] private GameObject GameObject_Info;
    [SerializeField] private Text Text_Description;

    private event Action<string> OnSelectEvent;
    public bool IsUsableItem { get; private set; }

    private string _slotDataId;

    private void OnEnable()
    {
        Image_Selected.gameObject.SetActive(false);
        GameObject_Info.SetActive(false);
        Button_Slot.BindOnClickButtonEvent(OnClick_SelectItem);
    }

    private void OnDisable()
    {
        OnSelectEvent = null;
    }

    public void SetIcon(string itemDataId)
    {
        var itemData = GameDataManager.Instance.GetItemData(itemDataId);
        if (itemData == null) return;

        IsUsableItem = true;

        string iconPath = itemData.IconPath;
        if (string.IsNullOrEmpty(iconPath) == true) return;

        GameUtil.LoadAndSetSpriteImage(Image_MainIcon, iconPath).Forget();
    }

    public string GetSlotDataId()
    {
        return _slotDataId;
    }

    public void InitSlot(string dataId, Action<string> OnclickCallback)
    {
        var itemData = GameDataManager.Instance.GetItemData(dataId);
        if (itemData == null)
        {
            return;
        }

        SetIcon(dataId);

        Text_Description.text = itemData.UseItemDescription;

        string iconPath = itemData.IconPath;
        if (string.IsNullOrEmpty(iconPath) == true)
        {
            return;
        }
        // 묻지마 사용 < image에 아이콘, sprite리소스 불러와 줄 때
        GameUtil.LoadAndSetSpriteImage(Image_MainIcon, iconPath).Forget();

        _slotDataId = dataId;

        OnSelectEvent += OnclickCallback;
    }

    private void OnClick_SelectItem()
    {
        OnSelectEvent?.Invoke(_slotDataId);
    }

    public void BindSlotSelectEvent(Action<string> onSelectEvent)
    {
        OnSelectEvent = onSelectEvent;
    }

    public void ChangeSelectedState(bool isSelected)
    {
        Image_Selected.gameObject.SetActive(isSelected);

        if (GameObject_Info != null)
        {
            GameObject_Info.SetActive(isSelected);
        }
    }
    public void SetDisableSlot()
    {
        if (Button_Slot != null)
        {
            Button_Slot.gameObject.SetActive(false);
        }

        Color iconColor = Image_MainIcon.color;
        iconColor.a = 0.3f;
        Image_MainIcon.color = iconColor;

        Color frameColor = Image_Frame.color;
        frameColor.a = 0.3f;
        Image_Frame.color = frameColor;
    }
}
