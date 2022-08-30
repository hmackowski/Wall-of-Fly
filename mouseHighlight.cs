using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHighlight : MonoBehaviour
{
    
    public Transform player;
    public float highLightRange = 8;

    void OnMouseOver()
    {

        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= highLightRange)
        {
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            GetComponent<Outline>().enabled = false;
        }
           


    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }




}











