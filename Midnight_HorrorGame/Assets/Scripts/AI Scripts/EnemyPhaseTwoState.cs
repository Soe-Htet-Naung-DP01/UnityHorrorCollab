using UnityEngine;

public class EnemyPhaseTwoState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered phase 2");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }
}
