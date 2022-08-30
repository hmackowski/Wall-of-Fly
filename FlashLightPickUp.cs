using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickUp : MonoBehaviour
{

    public float FLRotation = 10;
    public Transform theDest;
    public Transform player;
    public bool isPickUpAble = false;
    public bool pickedUp = false;
    public bool holdingItemAlready = false;
    public float pickUpRange;
    public bool equipped;
    public static bool slotFull;
    float timer;
    float waitingTime = 3;
   
    public GameObject Flashlightcheck;


    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }
        if (equipped && Input.GetKeyDown(KeyCode.G))
        {
          //  Drop();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            //DISPLAY MESSAGE TELLING ME WE ARE ALREADY HOLDING SOMETHING






        }

    }

    // WHEN MOUSE IS HOVERING ITEM, ALLOW ITEM TO BE PICKED UP
    void OnMouseEnter()
    {
        if (!holdingItemAlready)
        {
            isPickUpAble = true;
        }
        else
        {
            isPickUpAble = false;
        }

    }

    //WHEN YOU MOVE MOUSE AWAY FROM ITEM, YOU CAN NO LONGER PICK UP
    void OnMouseExit()
    {
        isPickUpAble = false;
        GetComponent<Outline>().enabled = false;



    }

    void PickUp()
    {

        //PRESS "e" TO PICK UP OBJECT, WHICH WILL GO TO THE GAMEOBJECT "guide" TRANSFORM LOCATION
        if (!holdingItemAlready && Input.GetKey("e"))
        {
            if (isPickUpAble)
            {
                //GRAB DEFAULT ROTATION ON ITEM
                float rotX = transform.rotation.x;
                float rotY = transform.rotation.y;
                float rotZ = transform.rotation.z;

                //MOVE OBJECT TO PLAYER HAND
                GetComponent<Rigidbody>().isKinematic = true;
                this.transform.position = theDest.position;
                this.transform.parent = GameObject.Find("guide").transform;

                //ROTATE ITEM TO BE STRAIGHT WHILE HOLDING
                transform.localRotation = Quaternion.Euler(90, FLRotation , rotZ);
                // YOU ARE HOLDING SOMETHING SO SET TO TRUE
                equipped = true;
                slotFull = true;
                Flashlightcheck.GetComponent<checkEquipped>().enabled = true;



            }



        }
    }

    void Drop()
    {


        equipped = false;
        slotFull = false;


        this.transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        //RESET ITEM TO BE ALLOWED TO BE PICKED UP AGAIN
        holdingItemAlready = false;
        Flashlightcheck.GetComponent<checkEquipped>().enabled = false;



    }

    void OnMouseOver()
    {


        GetComponent<Outline>().enabled = true;


    }






}