using UnityEngine;

public enum EntityAnimState
{
    None = 0,
    Idle,
    Walk,
    Run,
    Attack,
    Jump,
    DoublsJump,
    Hit,
    Dead,
    Positive,
    Negative,
    InteractionStart
}
public class AnimatorController_2D : MonoBehaviour
{

    [SerializeField] private Animator Animator_Entity;

    private EntityAnimState _currentState;

    public void SetState(EntityAnimState newState)
    {
        if(newState == _currentState)
        {
            return;
        }

        ResetAllParameters();

        _currentState = newState;
        switch (_currentState)
        {
            case EntityAnimState.Idle:
                break;
            case EntityAnimState.Walk:
                Animator_Entity.SetBool("IsWalk", true);
                break;
            case EntityAnimState.Run:
                Animator_Entity.SetBool("IsRun", true);
                break;
            case EntityAnimState.Attack:
                Animator_Entity.SetTrigger("IsAttack");
                break;
            case EntityAnimState.Jump:
                Animator_Entity.SetBool("IsJump", true);
                break;
            case EntityAnimState.Hit:
                Animator_Entity.SetTrigger("IsHit");
                break;
            case EntityAnimState.Dead:
                Animator_Entity.SetBool("IsDead", true);
                break;
            case EntityAnimState.Positive:
                Animator_Entity.SetTrigger("IsPositive");
                break;
            case EntityAnimState.Negative:
                Animator_Entity.SetTrigger("IsNegative");
                break;
            case EntityAnimState.InteractionStart:
                Animator_Entity.SetTrigger("IsInteractionStart");
                break;
        }
    }

    private void ResetAllParameters()
    {
        Animator_Entity.SetBool("IsRun", false);
        Animator_Entity.SetBool("IsJump", false);
    }
}
