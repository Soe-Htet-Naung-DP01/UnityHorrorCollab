using UnityEngine;

public class EnemyPhaseOneState : EnemyBaseState
{

    float XSpeed = 2.0f;
    float YSpeed = 0.0f;
    float ZSpeed = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered phase 1");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
            
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector3 TowardsXDirection = new Vector3(XSpeed, YSpeed, ZSpeed);
        // moves the enemy in the X direction until it reaches the 10 on the x-axis
        // then it switched to Phase 2
        if (enemy.transform.position.x < 10)
        {
            enemy.transform.Translate(TowardsXDirection * Time.deltaTime);
        }
        else
        {
            enemy.SwitchState(enemy.PhaseTwo);
        }
    }
}
