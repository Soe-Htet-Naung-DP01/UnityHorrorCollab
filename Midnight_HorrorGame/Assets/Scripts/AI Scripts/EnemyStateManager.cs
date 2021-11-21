using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    EnemyPhaseOneState PhaseOne = new EnemyPhaseOneState();
    EnemyPhaseTwoState PhaseTwo = new EnemyPhaseTwoState();
    EnemyPhaseThreeState PhaseThree = new EnemyPhaseThreeState();

    // Start is called before the first frame update
    void Start()
    {
        // defines the state the monster will start with
        currentState = PhaseOne;

        // enters the current state
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
