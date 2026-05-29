using System;
using UnityEngine;
using UnityEngine.UI;

public class HudInteractionSlotUI : MonoBehaviour
{
    [SerializeField] private Text Text_InteractionTitle;
    [SerializeField] private Text Text_KeyName;
    [SerializeField] private UIButton Button_OnclickInteraction;

    private int _instanceId;
    private Transform _targetTransform;

    private string _interactionCallbackMsg;
    private event Action<string> _onClickCallback;


    private void OnEnable()
    {
        Button_OnclickInteraction.BindOnClickButtonEvent(Onclick_Interaction);
    }

    private void OnDisable()
    {
        _onClickCallback = null;
    }

    public void Onclick_Interaction()
    {
        _onClickCallback?.Invoke(_interactionCallbackMsg);
    }

    public void InitSlot(int instanceId, string interactionTitle, string interactionKey,Transform targetTransform, Action<string> onClickCallback)
    {
        _instanceId = instanceId;
        _targetTransform = targetTransform;

        Text_KeyName.text = interactionKey;
        Text_InteractionTitle.text = interactionTitle;
    }


}
