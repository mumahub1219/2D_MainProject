using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject Prefab_QuestSlot;
    [SerializeField] private Transform Transform_QuestUISlotroot;
    [SerializeField] private DaniTechUIButton Button_CreateSlot;
    [SerializeField] private DaniTechUIButton Button_CloseQuestUI;

    private List<QuestUI> _uiSlotList = new List<QuestUI>();

    private void OnEnable()
    {
        Button_CreateSlot.BindOnClickButtonEvent(Onclick_CreateSlot);
        Button_CloseQuestUI.BindOnClickButtonEvent(OnClick_CloseQuestUI);
        
    }

    public void OnClick_CloseQuestUI()
    {
        
    }

    public void Onclick_CreateSlot()
    {
        Debug.LogWarning("눌러짐");

        CreateQuestSlot("");
    }

    private void CreateQuestSlot(string questDataId)
    {
        var gameObject = Instantiate(Prefab_QuestSlot, Transform_QuestUISlotroot);
        if (gameObject == null) 
        {
            return;
        }

        var slotComponent = gameObject.GetComponent<QuestUI>();
        if (slotComponent == null)
        {
            return;
        }

        _uiSlotList.Add(slotComponent);
        Debug.Log($"슬롯 생성됨 {_uiSlotList.Count}");
    }
}
