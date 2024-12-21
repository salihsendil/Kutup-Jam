using UnityEngine;

public class WalkState : IAnimState
{

    public void EnterState(GameObject obj)
    {

    }

    public void ExitState(GameObject obj)
    {

    }

    public void UpdateState(GameObject obj)
    {
        if (obj.GetComponent<PlayerAnimController>())
        {
            if (!obj.GetComponent<PlayerAnimController>().IsWalking)
            {
                obj.GetComponent<PlayerAnimController>().SwitchState(new WalkState());
                obj.GetComponent<PlayerAnimController>().AnimatorFoot.SetBool(obj.GetComponent<PlayerAnimController>().IsWalkingHash, true);
            }
        }

        if (obj.GetComponent<EnemyAnimController>())
        {
            if (!obj.GetComponent<EnemyAnimController>().IsWalking)
            {
                obj.GetComponent<EnemyAnimController>().SwitchState(new WalkState());
                obj.GetComponent<EnemyAnimController>().AnimatorFoot.SetBool(obj.GetComponent<EnemyAnimController>().IsWalkingHash, true);
            }
        }
    }
}
