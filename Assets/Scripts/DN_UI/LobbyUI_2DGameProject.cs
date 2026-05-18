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
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.LobbyUI);
        DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.MainUI, DaniTechUIType.MainUI);
    }
    
    public void OnClick_GameEnd()
    {
        DaniTechGameManager.Inst.SaveAndEndGame();
    }
}
