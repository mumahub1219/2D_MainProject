using UnityEngine;

public class Npc_2D : MonoBehaviour
{
    [Header("애니메이터")]
    [SerializeField] private AnimatorController_2D AnimatiorController_Entitiy;


    public void StartInteract()
    {
        ChangeNpcState(EntityAnimState.InteractionStart);
    }

    public void ChangeNpcState(EntityAnimState newState)
    {
        AnimatiorController_Entitiy.SetState(newState);
    }


}
