using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Def System Pray
    public bool isPraying = false; // for later use, like casting time.
    public bool canPray = true;
    public bool isProtectedByPray = false;
    private float grantedPrayProtectionTime = 7f;
    public float prayTimer = 0;
    private float prayDuration = 4f;
    private float prayCooldown = 10f;

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
        Pray();
        PTimeCount();
    }

    private void Pray()
    {
        if(prayTimer <= 0)
        {
            canPray = true;
        }
        else if(prayTimer != 0)
        {
            canPray = false;
            prayTimer -= Time.deltaTime;
            if(prayTimer <= 0)
            {
                prayTimer = 0;
                canPray = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (canPray == true)
            {
                Debug.Log("is Praying");
                prayTimer = prayCooldown;
                Debug.Log("Prayed and Cooldown has started");
                isProtectedByPray = true;
                StartCoroutine("PrayProtectionTimeCount");
            }
            else if(canPray == false)
            {
                Debug.Log("can't pray 'cuz of cool down");
            }
        }

    }

    private IEnumerator PrayProtectionTimeCount()
    {

        yield return new WaitForSeconds(grantedPrayProtectionTime);
            isProtectedByPray = false;
        yield return null;
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
