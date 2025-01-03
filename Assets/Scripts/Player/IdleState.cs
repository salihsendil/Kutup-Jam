using UnityEngine;

public class IdleState : IAnimState
{

    public void EnterState(PlayerAnimController player)
    {

    }

    public void ExitState(PlayerAnimController player)
    {

    }

    public void UpdateState(PlayerAnimController player)
    {
        if (player.IsWalking)
        {
            player.SwitchState(new WalkState());
            player.AnimatorFoot.SetBool(player.IsWalkingHash, true);
        }
    }
}
