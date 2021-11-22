using System;
using UnityEngine;

public class EnemyPhaseOneState : EnemyBaseState
{
    // direction to go to, starts off by going right, change the number at the end to the "speed" of the object
    Vector3 dir = Vector3.right * 2.0f;

    float speed = 2.0f;

    // enemy will aggro on the player when it is within 3 units
    float playerDistanceBeforeAggro = 9.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered phase 1");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if ((enemy.player.transform.position - enemy.transform.position).sqrMagnitude < Math.Pow(playerDistanceBeforeAggro,2))
        {
            Debug.Log("Player is nearby, entering Phase 2");
            enemy.SwitchState(enemy.PhaseTwo);
        }

        enemy.transform.Translate(dir * Time.deltaTime);

        if (enemy.transform.position.x >= 10)
        {
            dir = Vector3.left*speed;
        }
        else if (enemy.transform.position.x <= -10)
        {
            dir = Vector3.right*speed;
        }
    }
}
