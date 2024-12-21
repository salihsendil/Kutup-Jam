using UnityEngine;

public class WalkState : IAnimState
{

    public void EnterState(PlayerAnimController playerAnimController)
    {

    }

    public void ExitState(PlayerAnimController playerAnimController)
    {

    }

    public void UpdateState(PlayerAnimController playerAnimController)
    {
        if (!playerAnimController.IsWalking)
        {
            playerAnimController.SwitchState(new IdleState());
            playerAnimController.AnimatorFoot.SetBool(playerAnimController.IsWalkingHash, false);
        }
    }
}
