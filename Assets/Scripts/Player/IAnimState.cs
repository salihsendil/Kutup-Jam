using UnityEngine;

public interface IAnimState
{
    void EnterState(PlayerAnimController playerAnimController);
    void UpdateState(PlayerAnimController playerAnimController);
    void ExitState(PlayerAnimController playerAnimController);
}
