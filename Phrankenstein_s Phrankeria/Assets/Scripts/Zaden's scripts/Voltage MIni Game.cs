/*
 * Zaden Fetrow
 * A mini game where you have a knob that spins around and you have to click at spisific time
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltageMIniGame : MonoBehaviour
{
    //Public bools
    public Transform pivotPoint;
    public Transform linePart;
    public float rotationSpeed = 50f;
    public float radius = 2f;
    public float maxAngle = 90f;
    public float minAngle = -90f;
    public float currentAngle = 0;
    public float startingPoint = 45f;
    public Quaternion startRotation = Quaternion.identity;
    //public Transform Win;


    //Privet bools
    private Vector3 startPoint;
    
    //Edit points

    private void Start()
    {
        startPoint = transform.position;
        Debug.Log("Start Position: " + startPoint);
    }

    private void Update()
    {

        float input = Input.GetAxis("Horizontal");

        currentAngle += input * 100f * Time.deltaTime;

        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        float raidianAngel = currentAngle * Mathf.Rad2Deg;

        float zRotation = transform.eulerAngles.z;

        //Vector2 newPosition = new Vector2(pivotPoint.position.x + Mathf.Cos(raidianAngel) * pivotPoint.y + Mathf.Sin(raidianAngel) * radius);

        //transform.position = newPosition; 

        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space down");
            //transform.(pivotPoint.position, Vector3.forward, rotationspeed  );
            rotationSpeed = -rotationSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            linePart.gameObject.CompareTag("Win thing");
            Debug.Log("woring for now");
        }

        if (rotationSpeed != 50f && zRotation > 179 && zRotation < 181)
        {
            Debug.Log($"rotationSpeed: {rotationSpeed}, zRotation: {zRotation}");
            Debug.Log("180's working");
            rotationSpeed = -rotationSpeed;
        }

        if (rotationSpeed != -50 && zRotation > 0.1 && zRotation < 1)
        {
            Debug.Log("0's working");
            rotationSpeed = -rotationSpeed;
        }

        /*if (rotationSpeed != -50 && zRotation > 1 && zRotation < -1)
        {
            Debug.Log($"rotationSpeed: {rotationSpeed}, zRotation: {zRotation}");
            rotationSpeed = -rotationSpeed; 
        }*/

        /*if (rotationSpeed != -50 && zRotation > 1 && zRotation <  -1)
        {
            Debug.Log("0's working");
            rotationSpeed = -rotationSpeed;
        }*/

        /* if (Mathf.Approximately(zRotation,0))
         {
             Debug.Log("0's working");
             rotationSpeed = -rotationSpeed;
         }*/
    }

    

   /* private void OnCollisionEnter2D(Collision2D other)
    {
        if (rotationSpeed != -50)
        {
            Debug.Log("0's working");
            rotationSpeed = -rotationSpeed;
        }
    }*/


    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win thing"))
        {
            Debug.Log("working!");
        }
    }*/
}
