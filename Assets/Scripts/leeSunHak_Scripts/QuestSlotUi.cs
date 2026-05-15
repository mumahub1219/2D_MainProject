using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlotUi : MonoBehaviour
{
    [SerializeField] private Text Text_QuestName;
    [SerializeField] private Text Text_QuestContent;

    [SerializeField] private DaniTechUIButton Button_Agree;
    [SerializeField] private DaniTechUIButton Button_Disagree;

    private void OnEnable()
    {
        Button_Agree.BindOnClickButtonEvent(OnClick_AgreeQuest);
        Button_Disagree.BindOnClickButtonEvent(OnClick_DisagreeQuest);
    }

    public void SetQuestSlot(string questDataId)
    {
        Text_QuestName.text = questDataId;

    }

    public void OnClick_AgreeQuest()
    {

    }

    public void OnClick_DisagreeQuest()
    {

    }
}
