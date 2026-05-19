using UnityEngine;

public class DictionaryUI : MonoBehaviour
{
    [SerializeField] UIButton Button_CloseBG;
    [SerializeField] UIButton Button_Close;

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
