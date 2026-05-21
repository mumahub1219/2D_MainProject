using UnityEngine;

public class MainUI_2D_Platform : UIBase
{
    [SerializeField] private DaniTechUIButton Button_GameStart;
    [SerializeField] private DaniTechUIButton Button_EndGame;

    private void OnEnable()
    {
        Button_GameStart.BindOnClickButtonEvent(Onclick_GameStart);
        Button_EndGame.BindOnClickButtonEvent(Onclick_EndGame);
    }

    public void Onclick_GameStart()
    {
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.MainUI);
        UIManager.Instance.OpenScoreUI();
    }

    public void Onclick_EndGame()
    {
        Application.Quit();
    }
}
