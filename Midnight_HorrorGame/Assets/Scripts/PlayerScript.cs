using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public bool isProctected = false;//set to public to see the update status in Inspector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
