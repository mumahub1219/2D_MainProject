using UnityEngine;

public class MainUI_2D_Platform : DaniTechUIBase
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
        DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.MainUI, DaniTechUIType.MainUI);
        DaniTechUIManager.Instance.OpenScoreUI();
    }

    public void Onclick_EndGame()
    {
        Application.Quit();
    }
}
