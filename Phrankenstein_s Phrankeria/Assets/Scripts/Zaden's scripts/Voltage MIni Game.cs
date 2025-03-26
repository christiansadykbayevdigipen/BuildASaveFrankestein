//Zaden Fetrow
//A mini game where you have a knob that spins around and you have to click at spisific time


using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoltageMIniGame : MonoBehaviour
{
    // Public Fields
    public Transform pivotPoint;
    public GameObject LinePart;
    public GameObject WinBar;
    public GameObject GameOver;
    public TMP_Text finalScoreText;
    public float rotationSpeed = 50f;
    public float Seconds = 30f;
    //public Transform Win;


    //Privet bools
    private Vector3 startPoint;
    private int LastPositionsIndex = -1;
    private int score = 0;

    //Random points for the point to click
    private Vector3[] positions = new Vector3[]
   {
        new Vector3(0f, 2f, 0f),
        new Vector3(-1.25f, 1.5f, 0f),
        new Vector3(1.25f, 1.5f, 0f),
        new Vector3(-1.9f, 0.5f, 0f),
        new Vector3(1.9f, 0.5f, 0f)
   };


    //Edit points
    

    private void Start()
    {
        startPoint = transform.position;
        Debug.Log("Start Position: " + startPoint);
        rotationSpeed = -rotationSpeed;
        StartCoroutine(CountDown(Seconds));
        StartCoroutine(NeedleSpeed());
    }


    // This increases the needle speed based off the amount of time the game has been going on for
    private IEnumerator NeedleSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (rotationSpeed > 0)
            {
                rotationSpeed = Mathf.Min(rotationSpeed + 4.3f, 180f); // Increase but cap at 180
            }
            else if (rotationSpeed < 0)
            {
                rotationSpeed = Mathf.Max(rotationSpeed - 4.3f, -180f); // Decrease but cap at -180
            }
            else
            {
                rotationSpeed = 1f;
            }

            Debug.Log("Rotation Speed: " + rotationSpeed);
        }
    }


    // 30 second timer for game phase
    private IEnumerator CountDown(float seconds)
    {
        float counter = seconds; 
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        CountDownOver();
    }

    // This activates after the game phase is over and shows you your score
    void CountDownOver()
    {
        Debug.Log("Count down over! Final Score: " + score);

        finalScoreText.text = "Final Score: " + score;
        GameOver.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
     
        //float raidianAngel = currentAngle * Mathf.Rad2Deg;

        float zRotation = transform.eulerAngles.z;

        //The point where the needle is moving around
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        //when you click space the needle swaps directions
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space down");
            rotationSpeed = -rotationSpeed;
        }

        //To stop the needle from going more then 180 degrees 
        if (rotationSpeed <= 0f && zRotation > 179 && zRotation < 181)
        {
            Debug.Log($"rotationSpeed: {rotationSpeed}, zRotation: {zRotation}");
            Debug.Log("180's working");
            rotationSpeed = -rotationSpeed;
        }

        //To stop the needle from going more then 0 degrees
        if (rotationSpeed >= -0f && zRotation > 0.1 && zRotation < 2)
        {
            Debug.Log("0's working");
            rotationSpeed = -rotationSpeed;
        }

        //Detection for if the needle is on the right point and if so then makes the point go to a new position
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (LinePart.GetComponent<Collider2D>().IsTouching(WinBar.GetComponent<Collider2D>()))
            {
                score += 1;
                WinBar.gameObject.SetActive(false);
                SetRandomPosition();
                Debug.Log("Correct! +1 Point | Score: " + score);
            }
            else
            {
                score -= 1;
                Debug.Log("Miss! -1 Point | Score: " + score);
            }
        } 
    }



    //Setting the random position
    void SetRandomPosition()
    {
        int newIndex;

        do
        {
            newIndex = Random.Range(0, positions.Length);
        } while (newIndex == LastPositionsIndex);

        WinBar.transform.position = positions[newIndex];
        WinBar.gameObject.SetActive(true);

        LastPositionsIndex = newIndex;
    }
}


//stuff i dont think ill need 
/*
float input = Input.GetAxis("Horizontal");

currentAngle += input * 100f * Time.deltaTime;

currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
*/