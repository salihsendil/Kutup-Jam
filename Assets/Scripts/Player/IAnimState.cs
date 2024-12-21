using UnityEngine;

public interface IAnimState
{
    void EnterState(GameObject obj);
    void UpdateState(GameObject obj);
    void ExitState(GameObject obj);
}
