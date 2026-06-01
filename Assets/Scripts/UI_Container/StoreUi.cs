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

    private void OnEnable()
    {
        Button_CloseBG.BindOnClickButtonEvent(OnClick_CloseStoreUI);
        Button_Close.BindOnClickButtonEvent(OnClick_CloseStoreUI);
        Button_BuyItem.BindOnClickButtonEvent(OnClick_BuyItem);
    }

    private void OnClick_CloseStoreUI()
    {
        UIManager.Instance.CloseContentUI(UIType.StoreUI);
    }

    private void OnClick_BuyItem()
    {

    }
}
