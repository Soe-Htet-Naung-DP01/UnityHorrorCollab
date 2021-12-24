using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MnmGhostScript : MonoBehaviour
{
    //Basic ---> Check if player is proctected by either Salt Circle or Pray.
    //If player is protected ---> Blink away and go to other  room.
    //P1 ---> Walk Around The Map Randomly, Stop by in some room, Enjoy the stay in the room for a while.
    //P2 ---> Search for Player but not always.
    //P3 ---> Destory Salt Circles and Make Pray ineffective by Chance. 
    //      ---> Blink Positions

    //Getting Variables From Another Script

    //Ghost/MidNightMan's Attributes
    public float gSpeed = 5f;
    public float gDash = 1.5f;

    //PhaseControl
    public float phaseTimer = 0;
    public bool phaseOne = true;
    public bool phaseTwo = false;
    public bool phaseThree = false;

    //Phase One Variables
    public float wanderRadius = 10f;
    public float wanderTimer = 10f;
    public float timer = 0;
    public Transform target;
    public NavMeshAgent agentEnemy;

    //Phase Two Variables

    //Phase Three Variables


    // Start is called before the first frame update
    void Start()
    {
        agentEnemy = GetComponent<NavMeshAgent>();
        timer = wanderTimer; // for AI to start moving as soon as the game starts  
    }

    // Update is called once per frame
    void Update()
    {
        PhaseChanger();  
    }

    void PhaseOne()
    {
        WanderAround();
    }

    void PhaseTwo()
    {

    }

    void PhaseThree()
    {

    }

    void PhaseChanger() //it does what it sounds like
    {
        if(phaseOne == true && phaseTwo == false && phaseThree == false)
        {
            PhaseOne();
        }
        if (phaseOne == false  && phaseTwo == true && phaseThree == false)
        {
            PhaseTwo();
        }
        if (phaseOne == false && phaseTwo == false && phaseThree == true)
        {
            PhaseThree();
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
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agentEnemy.SetDestination(newPos);
            timer = 0;
        }
    }

}
