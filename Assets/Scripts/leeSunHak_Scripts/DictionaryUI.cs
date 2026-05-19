using UnityEngine;
using UnityEngine.UI;

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


    private void OnEnable()
    {
        Button_CloseBG.BindOnClickButtonEvent(Onclick_CloseDictionary);
        Button_Close.BindOnClickButtonEvent(Onclick_CloseDictionary);
    }

    public void Onclick_CloseDictionary()
    {
        UIManager.Instance.CloseUI(UIRootType.ContentUI, UIType.DictionaryUI);
    }


}
