using UnityEngine;

public class EnemyPhaseOneState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered phase 1");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
