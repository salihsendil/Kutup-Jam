using UnityEngine;

public class IdleState : IAnimState
{

    public void EnterState(PlayerAnimController playerAnimController)
    {

    }

    public void ExitState(PlayerAnimController playerAnimController)
    {

    }

    public void UpdateState(PlayerAnimController playerAnimController)
    {
        if (playerAnimController.IsWalking)
        {
            playerAnimController.SwitchState(new WalkState());
            playerAnimController.AnimatorFoot.SetBool(playerAnimController.IsWalkingHash, true);
        }
    }
}
