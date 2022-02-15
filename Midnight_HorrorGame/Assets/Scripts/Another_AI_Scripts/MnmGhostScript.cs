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
    public GameObject playerObject;
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
    public float detectionRange = 5f;
    public Transform traget;
    public NavMeshAgent agentEnemy;


    //Phase Two Variables

    //Phase Three Variables

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = playerObject.GetComponent<PlayerScript>();
        agentEnemy = GetComponent<NavMeshAgent>();
        w_timer = wanderTimer; // for AI to start moving as soon as the game starts
        currentPhaseText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        // checks if the monster has detected or seen the player
        // if true, the monster will go to the players location no matter what, then it checks again after that if it can see the player again
        // if false and not going to the players last seen location, then it will do its phase activities
        if (!PlayerDetection()) {
            PhaseConditionsChecker();
        }
        else
        {
            TargetPlayer();
        }
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
        if(_playerScript.totalCollectedItem < 2)
        {
            phaseOne = true;
            phaseTwo = false;
            phaseThree = false;
            PhaseChanger();
        }
        else if(_playerScript.totalCollectedItem == 2)
        {
            phaseOne = false;
            phaseTwo = true;
            phaseThree = false;
            PhaseChanger();
        }
        else if(_playerScript.totalCollectedItem >= 3)
        {
            phaseOne = false;
            phaseTwo = false;
            phaseThree = true;
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

    // first checks if the player can be seen and then checks if the player is within the sight detection range
    // returns information if the player can be seen by the Midnight Man
    bool PlayerDetection()
    {
        float distanceFromPlayer = Vector3.Distance(playerObject.transform.position, transform.position);
        Vector3 rayDirection = playerObject.transform.position - transform.position;

        // Debug code to see how the raycast works
        // Debug.DrawRay(transform.position, (rayDirection.normalized + new Vector3(0f, 0.05f, 0f)) * 100);

        if (Physics.Raycast(transform.position, rayDirection.normalized, out RaycastHit hit))
        {
            if (hit.transform.tag == "Player")
            {
                if (distanceFromPlayer >= detectionRange)
                {
                    return false; // Player can be seen but too far
                }
                else
                {
                    return true; // Player can be seen
                }
            }
            else
            {
                return false; // Player cannot be seen
            }
        }
        return false;
    }

    // follows the player if the player can be detected
    void TargetPlayer()
    {
        Vector3 playerPosition = playerObject.transform.position;
        agentEnemy.SetDestination(playerPosition);
    }
}
