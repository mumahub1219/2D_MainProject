using UnityEngine;

public class LoadingUI_2DGameProject : MonoBehaviour
{
    [SerializeField] private UIButton Button_Close;

    private void OnEnable()
    {
        Button_Close.BindOnClickButtonEvent(Onclick_CloseInventory);
    }

    public void Onclick_CloseInventory()
    {
        UIManager.Instance.CloseContentUI(UIType.InventoryUI);
    }
}
