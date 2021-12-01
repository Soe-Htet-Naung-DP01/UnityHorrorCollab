using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Def System Pray
    public bool isPraying = false; // for later use, like casting time.
    public bool canPray = true;// pray avialablilty
    public bool isProtectedByPray = false;// being protected by pray
    private float grantedPrayProtectionTime = 7f;//buff Duration
    public float prayTimer = 0; // it is what it sounds like
    private float prayDuration = 4f;// currently not in use
    private float prayCooldown = 10f; // again it is what it sounds like

    //Salt Circle Related Variables
    public bool isProctected = false;//set to public to see the update status in Inspector;
    public float protectedTime = 0;
    private float maxPTime = 10f; // Time allowed for the player to stay inside of salt circle

    //Collection System Related Variables
    public Image itemImageOne;
    public Image itemImageTwo;
    public Image itemImageThree;
    public Image itemImageFour;
    public bool hasItemOne = false;
    public bool hasItemTwo = false;
    public bool hasItemThree = false;
    public bool hasItemFour = false;
    public bool gainedAllItems = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Pray();
        PTimeCount();
        CheckItemGain();
    }

    private void Pray()
    {
        if(prayTimer <= 0) //check if under cooldown
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
            if (canPray == true) //start praying
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

    void CheckItemGain() // Check if player has gained all the required items for one of the winning condition
    {
        if(hasItemOne == true && hasItemTwo == true && hasItemThree == true && hasItemFour ==true)
        {
            gainedAllItems = true;
        }
        else
        {
            gainedAllItems = false;
        }
    }
    private IEnumerator PrayProtectionTimeCount()//pray buff timer
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
        if(other.tag == "saltCircle") //check if entered into salt circle
        {
            Debug.Log("Player is now protected");
            isProctected = true;
        }
        if(other.tag == "ItemOne")
        {
            Debug.Log("Collected Item One");
            hasItemOne = true;
            itemImageOne.color = new Color (itemImageOne.color.r, itemImageOne.color.b, itemImageOne.color.g, 1f);
        }
        if (other.tag == "ItemTwo")
        {
            Debug.Log("Collected Item Two");
            hasItemTwo = true;
            itemImageTwo.color = new Color(itemImageTwo.color.r, itemImageTwo.color.b, itemImageTwo.color.g, 1f);
        }
        if (other.tag == "ItemThree")
        {
            Debug.Log("Collected Item Three");
            hasItemThree = true;
            itemImageThree.color = new Color(itemImageThree.color.r, itemImageThree.color.b, itemImageThree.color.g, 1f);
        }
        if (other.tag == "ItemFour")
        {
            Debug.Log("Collected Item Four");
            hasItemFour = true;
            itemImageFour.color = new Color(itemImageFour.color.r, itemImageFour.color.b, itemImageFour.color.g, 1f);
        }
    }

 

private void OnTriggerExit(Collider other)
    {
        if (other.tag == "saltCircle") //check if exited into salt circle
        {
            Debug.Log("Player is no longer protected");
            isProctected = false;
        }
    }
}
