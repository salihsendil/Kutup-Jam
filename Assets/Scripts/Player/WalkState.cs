using UnityEngine;

public class WalkState : IAnimState
{

    public void EnterState(PlayerAnimController player)
    {

    }

    public void ExitState(PlayerAnimController player)
    {

    }

    public void UpdateState(PlayerAnimController player)
    {
        if (!player.IsWalking)
        {
            player.SwitchState(new IdleState());
            player.AnimatorFoot.SetBool(player.IsWalkingHash, false);
        }
    }
}
