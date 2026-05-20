using UnityEngine;

public enum EntityAnimState
{
    None = 0,
    Idle,
    Walk,
    Run,
    Attack,
    Jump,
    Dead
}
public class AnimatorController_2D : MonoBehaviour
{

    [SerializeField] private Animator Animator_Player;

    private EntityAnimState _currentState;

    public void SetState(EntityAnimState newState)
    {
        if(newState == EntityAnimState.Idle && _currentState == EntityAnimState.Idle)
        {
            return;
        }

        _currentState = newState;
        switch (_currentState)
        {
            case EntityAnimState.Walk:
                Animator_Player.SetBool("IsWalk", true);
                break;
            case EntityAnimState.Run:
                Animator_Player.SetBool("IsRun", true);
                break;
            case EntityAnimState.Attack:
                Animator_Player.SetTrigger("IsAttack");
                break;
            case EntityAnimState.Jump:
                Animator_Player.SetBool("IsJump", true);
                break;
            case EntityAnimState.Dead:
                Animator_Player.SetBool("IsDead", true);
                break;
        }
    }

    private void ResetAllParameters()
    {
        Animator_Player.SetBool("IsRun", false);
        Animator_Player.SetBool("IsDead", false);
    }
}
