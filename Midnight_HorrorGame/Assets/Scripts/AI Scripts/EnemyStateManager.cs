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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
