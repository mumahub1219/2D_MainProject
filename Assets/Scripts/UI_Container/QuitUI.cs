using UnityEngine;

public class QuitUI : MonoBehaviour
{
    [SerializeField] private UIButton Button_Lobby;
    [SerializeField] private UIButton Button_Quit;

    private void OnEnable()
    {
        Button_Lobby.BindOnClickButtonEvent(Onclick_OpenLobbyUI);
        Button_Quit.BindOnClickButtonEvent(Onclick_QuitGame);
    }

    public void Onclick_OpenLobbyUI()
    {
        UIManager.Instance.CloseContentUI(UIType.QuitUI);
        UIManager.Instance.OpenContentUI(UIType.LobbyUI);

        GameManager.Inst.InitializaionRespawnSpot();
    }

    public void Onclick_QuitGame()
    {
        GameManager.Inst.SaveAndEndGame();
    }
}
