using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupUi : MonoBehaviour
{
    [SerializeField] private Text text_CharacterName;
    [SerializeField] private Text text_CharacterLevel;

    private void OnEnable()
    {
        text_CharacterName.text = "캐릭터 이름 : 이상";
        text_CharacterLevel.text = "캐릭터 레벨 : 30";

    }
}
