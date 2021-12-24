using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MnmGhostScript : MonoBehaviour
{
    //Basic ---> Check if player is proctected by either Salt Circle or Pray.
    //If player is protected ---> Blink away and go to other  room.
    //P1 ---> Walk Around The Map Randomly, Stop by in some room, Enjoy the stay in the room for a while.
    //P2 ---> Search for Player but not always.
    //P3 ---> Destory Salt Circles and Make Pray ineffective by Chance. 
    //      ---> Blink Positions


    //UIs for testing purposes
    public Text currentPhaseText;

    //Getting Access to Another Script
    private PlayerScript _playerScript;

    //Ghost/MidNightMan's Attributes


    //PhaseControl
    public float phaseTimer = 0;
    public bool phaseOne = true;
    public bool phaseTwo = false;
    public bool phaseThree = false;

    //Phase One Variables
    public float wanderRadius = 10f;
    public float wanderTimer = 10f;
    public float w_timer = 0;
    public Transform target;
    public NavMeshAgent agentEnemy;
    

    //Phase Two Variables

    //Phase Three Variables


    // Start is called before the first frame update
    void Start()
    {
        agentEnemy = GetComponent<NavMeshAgent>();
        w_timer = wanderTimer; // for AI to start moving as soon as the game starts
        currentPhaseText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {

        PhaseConditionsChecker();
    }

    void PhaseOne()
    {
        agentEnemy.speed = 3.5f;
        WanderAround();
    }

    void PhaseTwo()
    {
        agentEnemy.speed = 4f;
    }

    void PhaseThree()
    {
        agentEnemy.speed = 5f;
    }

    void PhaseChanger() //it does what it sounds like
    {
        if(phaseOne == true && phaseTwo == false && phaseThree == false)
        {
            PhaseOne();
            currentPhaseText.text = "Phase ONE";
        }
        if (phaseOne == false  && phaseTwo == true && phaseThree == false)
        {
            PhaseTwo();
            currentPhaseText.text = "Phase TWO";
        }
        if (phaseOne == false && phaseTwo == false && phaseThree == true)
        {
            PhaseThree();
            currentPhaseText.text = "Phase Three";
        }
    }

    void PhaseConditionsChecker() // determine why and when a phase should be changed.
    {
        if(_playerScript.totalCollectedItem <= 2)
        {
            phaseOne = true;
            phaseTwo = false;
            phaseThree = false;
            PhaseChanger();
        }
    }

    public static Vector3 RandomNavSphere(Vector3 _origin, float dist, int layermask) //Pick a random point to move using NavMesh.
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += _origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    void WanderAround() //Enemy AI moves around every 10secs to a random position.
    {
        w_timer += Time.deltaTime;

        if (w_timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agentEnemy.SetDestination(newPos);
            w_timer = 0;
        }
    }

 

}
