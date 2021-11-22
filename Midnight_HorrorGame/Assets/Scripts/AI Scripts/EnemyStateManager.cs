using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    public EnemyPhaseOneState PhaseOne = new EnemyPhaseOneState();
    public EnemyPhaseTwoState PhaseTwo = new EnemyPhaseTwoState();
    public EnemyPhaseThreeState PhaseThree = new EnemyPhaseThreeState();

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
        // carries out the updatestate function of the current Phase
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        // enter the state that is put into the argument (PhaseOne, PhaseTwo etc etc)
        currentState = state;
        state.EnterState(this);
    }
}
