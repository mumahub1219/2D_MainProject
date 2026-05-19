using UnityEngine;

public class MainUI_2DMainProject : MonoBehaviour
{
    [SerializeField] private UIButton Button_Inventory;
    [SerializeField] private UIButton Button_Dictionary;


    private void OnEnable()
    {
        Button_Inventory.BindOnClickButtonEvent(Onclick_OpenInventory);
        Button_Dictionary.BindOnClickButtonEvent(Onclick_OpenDictionary);
    }

    public void Onclick_OpenInventory()
    {
        UIManager.Instance.OpenContentUI(UIType.InventoryUI);
    }

    public void Onclick_OpenDictionary()
    {
        UIManager.Instance.OpenContentUI(UIType.DictionaryUI);
    }
}
