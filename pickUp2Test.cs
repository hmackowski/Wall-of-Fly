using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp2Test : MonoBehaviour
{

    public Transform theDest;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
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
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        float rotX = transform.rotation.x;
        float rotY = transform.rotation.y;
        float rotZ = transform.rotation.z;

        //MOVE OBJECT TO PLAYER HAND
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("guide").transform;

        //ROTATE ITEM TO BE STRAIGHT WHILE HOLDING
        transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        this.transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;

    }
}
