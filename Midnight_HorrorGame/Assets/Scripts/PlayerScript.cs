using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //Salt Circle Related Variables
    public bool isProctected = false;//set to public to see the update status in Inspector;
    public float protectedTime = 0;
    private float maxPTime = 10f; // Time allowed for the player to stay inside of salt circle

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PTimeCount();
    }

    private void PTimeCount()
    {
        if(isProctected == true)
        {
            protectedTime += Time.deltaTime;
            if(protectedTime >= maxPTime)
            {
                Debug.Log("Game Over!");
            }
        }
        else if(isProctected == false)
        {
            protectedTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "saltCircle")
        {
            Debug.Log("Player is now protected");
            isProctected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "saltCircle")
        {
            Debug.Log("Player is no longer protected");
            isProctected = false;
        }
    }
}
