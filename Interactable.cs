using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    public float pickUpRange = 5f;
    public Transform theDest;
    public GameObject hand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
           // Debug.Log(hit.rigidbody.gameObject);
            if (Input.GetKey("r") && GetComponent<Outline>().enabled == true)
            {
                               
                string poopName = hit.collider.gameObject.name; //Grab the name of the object that is hit by raycast and save to string

                hand = GameObject.Find(poopName); //Assigning 'hand' the object with the name that was hit by raycast
                hand.GetComponent<Rigidbody>().isKinematic = true; // setting isKinematic to true on the GameObject stored in 'hand'
                hand.transform.position = theDest.position; // moving the GameObject stored in 'hand' to the players hand(in the case the object named 'guide'

                hit.transform.parent = GameObject.Find("guide").transform; // move the GameObject parent to the players hand parent

            }
            
        }
    }
}
