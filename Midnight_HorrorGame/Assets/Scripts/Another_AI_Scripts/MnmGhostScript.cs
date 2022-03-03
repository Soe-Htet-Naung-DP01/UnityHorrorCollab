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
    public int currentPhase = 1;


    //Phase One Variables
    public float wanderRadius = 10f;
    public float wanderTimer = 10f;
    public float w_timer = 0;
    public float detectionRange = 10f;
    public Transform traget;
    public NavMeshAgent agentEnemy;

    //Phase Two Variables
    public float h_timer = 0.0f;
    public float huntPlayerTime = 10.0f;
    public float playerHuntChance = 0.1f; // 10% chance to chase player instead of doing a wander
    public bool isDoingHunt = false; // for use in P2 onwards
    public bool isWandering = false; // for use in P2 onwards

    //Phase Three Variables

    //Targeting Variables
    public bool isTargetingPlayer = false;

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
        // checks the current phase before anything else
        PhaseConditionsChecker(); 

        // if player can be detected, chase player else do the normal Phase activites (wander / hunt player)
        if (!PlayerDetection()) {
            if (isTargetingPlayer)
            {
                agentEnemy.ResetPath();
                isTargetingPlayer = false;
            }
            PhaseActivities();
        }
        else
        {
            // Normal play chase
            TargetPlayer();
        }
    }

    void PhaseOne()
    {
        agentEnemy.speed = 3.5f;
        wanderTimer = 10.0f;

        WanderAround();
    }

    void PhaseTwo()
    {
        // from phase 1 variables
        agentEnemy.speed = 4f;
        wanderTimer = 7.0f;
        wanderRadius = 15.0f;

        // from phase 2 variables
        playerHuntChance = 0.1f;
        huntPlayerTime = 10.0f;

        // finds out if the midnightman is wandering or not
        if (w_timer + Time.deltaTime >= wanderTimer)
        {
            isWandering = false;
        }
        else
        {
            isWandering = true;
        }

        // finds out if the midnightman is hunting or not
        if (h_timer >= huntPlayerTime)
        {
            isDoingHunt = false;
        }
        else
        {
            isDoingHunt = false;
        }

        Debug.Log("Is midnight man wandering: " + isWandering);
        Debug.Log("Is midnight man hunting: " + isDoingHunt);

        // if wandering is not active, roll a chance for a hunt, else start wandering instead
        if (isWandering == false)
        {
            if (isDoingHunt == true)
            {
                // midnight man is still doing the hunt
                HuntPhase();
            }
            else
            {
                if (Random.value > playerHuntChance) // 10% chance to do a hunt
                {
                    // midnight man starts doing the hunt
                    h_timer = 0;
                    isDoingHunt = true;
                    HuntPhase();
                }
                else
                {
                    // midnight man starts doing the wandering
                    WanderAround();
                }
            }
        }
        else
        {
            // midnight man is still doing the wandering
            WanderAround();
        }
    }

    void PhaseThree()
    {
        agentEnemy.speed = 5f;
    }

    void PhaseActivities() // checks the current phase number and leads it to the current function
    {
        switch (currentPhase)
        {
            case 1:
                PhaseOne();
                break;
            case 2:
                PhaseTwo();
                break;
            case 3:
                PhaseThree();
                break;
        }
    }

    void PhaseChanger() //it does what it sounds like
    {
        switch (currentPhase)
        {
            case 1:
                currentPhaseText.text = "Phase ONE";
                break;
            case 2:
                currentPhaseText.text = "Phase TWO";
                break;
            case 3:
                currentPhaseText.text = "Phase Three";
                break;
        }
    }

    void PhaseConditionsChecker() // determine why and when a phase should be changed.
    {
        if(_playerScript.totalCollectedItem < 2)
        {
            phaseOne = true;
            phaseTwo = false;
            phaseThree = false;
            currentPhase = 1;
        }
        else if(_playerScript.totalCollectedItem == 2)
        {
            phaseOne = false;
            phaseTwo = true;
            phaseThree = false;
            currentPhase = 2;
        }
        else if(_playerScript.totalCollectedItem >= 3)
        {
            phaseOne = false;
            phaseTwo = false;
            phaseThree = true;
            currentPhase = 3;
        }
        PhaseChanger();
    }

    public static Vector3 RandomNavSphere(Vector3 _origin, float dist, int layermask) //Pick a random point to move using NavMesh.
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += _origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    void WanderAround() //Enemy AI moves around every "w_timer" time to a random position.
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
        w_timer = 0;
        h_timer = 0;

        isTargetingPlayer = true;
        Vector3 playerPosition = playerObject.transform.position;
        agentEnemy.SetDestination(playerPosition);
    }

    void HuntPhase()
    {
        Debug.Log("In Hunt Phase");
    }
}
