using UnityEngine;

public class Npc_Explain : MonoBehaviour
{
    private int _instanceId;
    private string _npcDataId;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnInteractionButtonClicked("R");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerCollision();
        }
    }

    private void OnPlayerCollision()
    {
        UIManager.Instance.AddInteractionSlot(_instanceId, "R", "키설명", this.gameObject.transform, OnInteractionButtonClicked);
    }

    private void OnInteractionButtonClicked(string interactionKey)
    {
        if (interactionKey == "R")
        {
            UIManager.Instance.OpenPopupUI(UIType.ExplainKeyUI);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.RemoveInteractionSlot(_instanceId);
            UIManager.Instance.ClosePopupUI(UIType.ExplainKeyUI);
        }
    }
}
