using UnityEngine;

public class Npc_2D : MonoBehaviour
{
    [Header("애니메이터")]
    [SerializeField] private AnimatorController_2D AnimatiorController_Entitiy;

    private int _instanceId;
    private string _npcDataId;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnInteractionButtonClicked("G");
        }
    }

    public void StartInteract()
    {
        ChangeNpcState(EntityAnimState.InteractionStart);
    }

    public void ChangeNpcState(EntityAnimState newState)
    {
        AnimatiorController_Entitiy.SetState(newState);
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
        UIManager.Instance.AddInteractionSlot(_instanceId, "G", "상점 열기", this.gameObject.transform, OnInteractionButtonClicked);
    }

    private void OnInteractionButtonClicked(string interactionKey)
    {
        if (interactionKey == "G")
        {
            UIManager.Instance.OpenContentUI(UIType.InventoryUI);
            UIManager.Instance.OpenContentUI(UIType.StoreUI);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.RemoveInteractionSlot(_instanceId);

            UIManager.Instance.CloseContentUI(UIType.StoreUI);
            UIManager.Instance.CloseContentUI(UIType.InventoryUI);
        }
    }
}
