using UnityEngine;

public class EnemyPhaseThreeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered phase 3");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }
}
