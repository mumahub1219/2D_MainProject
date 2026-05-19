using Cysharp.Threading.Tasks;
using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class DictionarySlotUI : MonoBehaviour
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_Name;
    [SerializeField] private GameObject GObj_Selected;
    [SerializeField] private UIButton Button_SlotClick;

    private event Action<string> _onclickSlot;

    private string _slotDataId;

    private void OnEnable()
    {
        Button_SlotClick.BindOnClickButtonEvent(Onclick_DictionarySlot);
    }

    private void OnDisable()
    {
        _onclickSlot = null;
    }

    public string GetSlotDataId()
    {
        return _slotDataId;
    }

    public void Onclick_DictionarySlot()
    {
        _onclickSlot?.Invoke(_slotDataId);
    }

    public void InitSlot(string dataId, Action<string> OnclickCallback)
    {
        var itemData = GameDataManager.Instance.GetItemData(dataId);
        if (itemData == null) 
        {
            return;
        }

        Text_Name.text = itemData.Name;

        string iconPath = itemData.IconPath;
        if (string.IsNullOrEmpty(iconPath) == true)
        {
            return;
        }
        // 묻지마 사용 < image에 아이콘, sprite리소스 불러와 줄 때
        GameUtil.LoadAndSetSpriteImage(Image_MainIcon, iconPath).Forget();

        _slotDataId = dataId;

        _onclickSlot += OnclickCallback;

    }

    public void SetSelected(bool isSelect)
    {
        GObj_Selected.SetActive(isSelect);
    }
}
