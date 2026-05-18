using UnityEngine;

public class LobbyUI_2DGameProject : UIBase
{
    [SerializeField] private UIButton Button_GameStart;
    [SerializeField] private UIButton Button_GameEnd;

    private void OnEnable()
    {
        Button_GameStart.BindOnClickButtonEvent(OnClick_GameStart);
        Button_GameEnd.BindOnClickButtonEvent(OnClick_GameEnd);
    }

    public void OnClick_GameStart()
    {
        UIManager.Instance.CloseContentUI(UIType.LobbyUI);
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.MainUI);
    }
    
    public void OnClick_GameEnd()
    {
        GameManager.Inst.SaveAndEndGame();
    }
}
