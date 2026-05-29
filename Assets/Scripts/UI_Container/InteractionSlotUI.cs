using System;
using UnityEngine;
using UnityEngine.UI;

public class HudInteractionSlotUI : MonoBehaviour
{
    [SerializeField] private Text Text_InteractionTitle;
    [SerializeField] private Text Text_KeyName;
    [SerializeField] private UIButton Button_OnclickInteraction;

    [SerializeField] private int slotOffsetX;
    [SerializeField] private int slotOffsetY;

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

    public void InitSlot(int instanceId, string interactionTitle, string interactionKey,Transform targetTransform, Action<string> onClickCallback = null)
    {
        _instanceId = instanceId;
        _targetTransform = targetTransform;

        Text_KeyName.text = interactionKey;
        Text_InteractionTitle.text = interactionTitle;

        slotOffsetX = -240;
        slotOffsetY = 115;
    }

    private void Update()
    {
        if (_targetTransform != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(_targetTransform.position);

            var rectTransform = this.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector2 finalScreenPos = new Vector2(screenPos.x + slotOffsetX, screenPos.y + slotOffsetY);
                rectTransform.anchoredPosition = finalScreenPos;
            }
        }
    }
}
