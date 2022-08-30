using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPickUp : MonoBehaviour
{


    public Transform theDest;
    public bool isPickUpAble = false;
    public bool pickedUp = false;
    public bool holdingItemAlready = false;
    public bool equipped;
    public static bool slotFull;
    public GameObject hand;
    public Vector3 collision = Vector3.zero;
    public float pickUpRange = 5f;
    public GameObject guide;

    [SerializeField] public GameObject[] heldItems;
    [SerializeField] public bool[] isItemEquipped;
    public int selectedItem = 0;
    public int selectedItem2;
    public string selectableTag = "Selectable";
    public RaycastHit hit;
    
         
    private void Start()
    {
        selectedItem = 0;
        selectedItem2 = guide.transform.childCount;
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
       

        // Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && Physics.Raycast(ray, out hit, pickUpRange) && !slotFull)
        {
            PickUp();
        }
        if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }

        //SCROLL TROUGH ARRAY
       /* if (Input.GetAxis("Mouse ScrollWheel") > 0f)
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
       */
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
        if (selectedItem <= 2)
        {

        
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit, pickUpRange) && Input.GetKeyDown(KeyCode.E))
             {
                selectedItem++;
                var selection = hit.transform;
                 if (selection.CompareTag(selectableTag))
                 {
                    

                    string poopName = hit.collider.gameObject.name; //Grab the name of the object that is hit by raycast and save to string

                     hand = GameObject.Find(poopName); //Assigning 'hand' the object with the name that was hit by raycast
                     hand.GetComponent<Rigidbody>().isKinematic = true; // setting isKinematic to true on the GameObject stored in 'hand'
                     hand.transform.position = theDest.position; // moving the GameObject stored in 'hand' to the players hand(in the case the object named 'guide'

                     hit.transform.parent = GameObject.Find("guide").transform; // move the GameObject parent to the players hand parent

                        //GRAB DEFAULT ROTATION ON ITEM
                     float rotX = hand.transform.rotation.x;
                     float rotY = hand.transform.rotation.y;
                     float rotZ = hand.transform.rotation.z;

                     //ROTATE ITEM TO BE STRAIGHT WHILE HOLDING
                     hand.transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
                     // YOU ARE HOLDING SOMETHING SO SET TO TRUE
                     equipped = true;
                     slotFull = true;
                     
                    selectedItem2++;
                 }
             }
        }   
    }

    void Drop()
    {


        equipped = false;
        slotFull = false;


        hand.transform.parent = null;
        hand.GetComponent<Rigidbody>().isKinematic = false;
        //RESET ITEM TO BE ALLOWED TO BE PICKED UP AGAIN
        holdingItemAlready = false;

    }

    void selectHeldItem()
    {
        int i = 0;
        heldItems[selectedItem].SetActive(false);
    }







}