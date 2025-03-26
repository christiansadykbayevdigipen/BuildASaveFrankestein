// Author: Originally Zaden. Refactored by Christian. This version of "Voltage MIni Game" is a refactored version that is made to fit in my minigame system.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MG2 : MiniGame
{
    // Public Fields
    public Transform PivotPoint;
    public GameObject LinePart;
    public GameObject WinBar;
    public float StartingRotationSpeed = 50f;
    public float RotationAcceleration;
    public float Seconds = 30f;
    public int RequiredHits;
    public Vector3[] Positions = new Vector3[]
    {
        new Vector3(0f, 2f, 0f),
        new Vector3(-1.25f, 1.5f, 0f),
        new Vector3(1.25f, 1.5f, 0f),
        new Vector3(-1.9f, 0.5f, 0f),
        new Vector3(1.9f, 0.5f, 0f)
    };

    // Private Fields
    private float m_RotationSpeed;
    private bool m_IsComplete = false;
    private bool m_IsRunning = false;
    private Vector3 m_StartPoint;
    private int m_LastPositionsIndex = -1;
    private int m_Score = 0;
    private int m_Losses = 0;


    public override void StartMinigame()
    {
        m_IsRunning = true;
        m_IsComplete = false;
        m_Losses = 0;
        m_Score = 0;

        m_StartPoint = transform.position;
        m_RotationSpeed = -StartingRotationSpeed;
        StartCoroutine(Timer(Seconds));
        StartCoroutine(MoveNeedle());
    }

    public override void StopMinigame()
    {
        m_IsRunning = false;
        m_IsComplete = true;
    }

    public override bool GetWinState()
    {
        return m_Score >= RequiredHits;
    }

    public override bool IsComplete()
    {
        return m_IsComplete;
    }

    private IEnumerator Timer(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        EndTimer();
    }

    void EndTimer()
    {
        m_IsComplete = true;
    }

    // Moves the needle and accelerates it as times increases.
    private IEnumerator MoveNeedle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (m_RotationSpeed > 0)
            {
                m_RotationSpeed = /*Mathf.Min*/(m_RotationSpeed + RotationAcceleration/*, 180f*/); // Increase but cap at 180
            }
            else if (m_RotationSpeed < 0)
            {
                m_RotationSpeed = /*Mathf.Max*/(m_RotationSpeed - RotationAcceleration/*, -180f*/); // Decrease but cap at -180
            }
            else
            {
                m_RotationSpeed = 1f;
            }
        }
    }

    private void Update()
    {
        if (!m_IsRunning)
            return;

        float zRotation = transform.eulerAngles.z;

        // The point where the needle is moving around
        if (PivotPoint != null)
        {
            transform.RotateAround(PivotPoint.position, Vector3.forward, m_RotationSpeed * Time.deltaTime);
        }

        //To stop the needle from going more then 180 degrees 
        if (m_RotationSpeed <= 0f && zRotation > 179 && zRotation < 181)
        {
            m_RotationSpeed = -m_RotationSpeed;
        }

        //To stop the needle from going more then 0 degrees
        if (m_RotationSpeed >= -0f && zRotation > 0.1 && zRotation < 2)
        {
            m_RotationSpeed = -m_RotationSpeed;
        }

        // Detection for if the needle is on the right point and if so then makes the point go to a new position
        if (Input.GetMouseButtonDown(0))
        {

            // Then, do the detection for the if the needle is correct position.
            if (LinePart.GetComponent<Collider2D>().IsTouching(WinBar.GetComponent<Collider2D>()))
            {
                m_Score += 1;
                WinBar.gameObject.SetActive(false);
                Vector3 oldPos = WinBar.transform.position;
                SetRandomPosition();

                Vector3 newPos = WinBar.transform.position;

                float newDir = newPos.x - oldPos.x;

                if (newDir == 0)
                    newDir = 1;

                // This checks to see if the needle is rotating in the direction of the new dot. If not, then flip it.
                if (m_RotationSpeed / Mathf.Abs(m_RotationSpeed) == newDir / Mathf.Abs(newDir))
                {
                    m_RotationSpeed = -m_RotationSpeed;
                }
            }
            else
            {
                m_Losses++;
            }
        }
    }

    // Sets the next voltage position to be a random position.
    void SetRandomPosition()
    {
        int newIndex;

        do
        {
            newIndex = Random.Range(0, Positions.Length);
        } while (newIndex == m_LastPositionsIndex);

        WinBar.transform.position = Positions[newIndex];
        WinBar.gameObject.SetActive(true);

        m_LastPositionsIndex = newIndex;
    }
}
