using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HudMainUI : UIBase
{
    [SerializeField] private GameObject Prefab_HudSlot;
    [SerializeField] private GameObject Prefab_InteractionSlot;

    [SerializeField] private Transform Transform_SlotRoot;

    private Dictionary<int, HudSlotUI> _hudSlotList = new Dictionary<int, HudSlotUI>();
    private Dictionary<int, HudInteractionSlotUI> _interactionSlotList = new Dictionary<int, HudInteractionSlotUI>();


    public void AddHudSlot(int instanceId, Transform targetTransform)
    {
        CreateHudSlot(instanceId, targetTransform);
    }

    private void CreateHudSlot(int instanceId, Transform targetTransform)
    {
        var gObj = Instantiate(Prefab_HudSlot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<HudSlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(instanceId, targetTransform);

        _hudSlotList.Add(instanceId, slotComponent);
    }

    public void RemoveHudSlot(int instanceId)
    {
        if (_hudSlotList.ContainsKey(instanceId) == true)
        {
            var slot = _hudSlotList[instanceId];

            Destroy(slot.gameObject);
            _hudSlotList.Remove(instanceId);
        }
    }

    public void AddInteractionSlot(int instanceId, string interactionTitle, string interactionKey,
        Transform targetTransform, Action<string> onClickCallback)
    {
        CreateInteractionSlot(instanceId, interactionTitle, interactionKey, targetTransform, onClickCallback);
    }

    private void CreateInteractionSlot(int instanceId, string interactionTitle, string interactionKey,
            Transform targetTransform, Action<string> onClickCallback)
    {
        var gObj = Instantiate(Prefab_InteractionSlot, Transform_SlotRoot);
        if (gObj == null) return;

        var slotComponent = gObj.GetComponent<HudInteractionSlotUI>();
        if (slotComponent == null) return;

        slotComponent.InitSlot(instanceId, interactionTitle, interactionKey, targetTransform, onClickCallback);

        _interactionSlotList.Add(instanceId, slotComponent);
    }

    public void RemoveInteractionSlot(int instanceId)
    {
        if (_interactionSlotList.ContainsKey(instanceId) == true)
        {
            var slot = _interactionSlotList[instanceId];

            Destroy(slot.gameObject);
            _interactionSlotList.Remove(instanceId);
        }
    }
}
