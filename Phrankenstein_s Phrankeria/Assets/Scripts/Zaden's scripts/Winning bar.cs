//Zaden Fetrow
//The bar within the voltage mini game that you have to click

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Winningbar : MonoBehaviour
{
    //Public bools
    public Transform LinePoint;
    //Private bools


    //Edit points


    void Update()
    { 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win thing") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Colliding");
           
        }
    }
}

