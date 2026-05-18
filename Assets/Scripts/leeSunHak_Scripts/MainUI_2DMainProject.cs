using UnityEngine;

public class MainUI_2DMainProject : MonoBehaviour
{
    [SerializeField] private UIButton Button_Inventory;

    private void OnEnable()
    {
        Button_Inventory.BindOnClickButtonEvent(Onclick_OpenInventory);
    }

    public void Onclick_OpenInventory()
    {
        UIManager.Instance.OpenContentUI(UIType.InventoryUI);
    }
}
