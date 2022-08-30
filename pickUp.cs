using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{

    public Transform theDest;
    public Transform player;
    public bool isPickUpAble = false;
    public bool pickedUp = false;
    public bool holdingItemAlready = false;
    public float pickUpRange;
    public bool equipped;
    public static bool slotFull;
    public Camera mainCamera;

    [SerializeField]
    public GameObject[] heldItems;
    [SerializeField]
    public bool[] isItemEquipped;
    public int selectedItem = 0;
    // public GameObject texts;


    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }
        if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            //DISPLAY MESSAGE TELLING ME WE ARE ALREADY HOLDING SOMETHING
        }


        //SCROLL TROUGH ARRAY
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedItem >= heldItems.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectHeldItem();
                selectedItem++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedItem <= 0)
            {
                selectedItem = heldItems.Length - 1;
            }
            else
            {
                selectHeldItem();
                selectedItem--;
            }
        }

        ///////////////////////////////////////////////////
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
                transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
                // YOU ARE HOLDING SOMETHING SO SET TO TRUE
                equipped = true;
                slotFull= true;
               

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

        

    }

    void selectHeldItem()
    {
        int i = 0;
        heldItems[selectedItem].SetActive(false);
    }


    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    }




}