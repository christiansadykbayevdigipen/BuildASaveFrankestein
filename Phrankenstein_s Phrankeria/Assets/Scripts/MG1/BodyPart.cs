// Author: Christian Sadykbayev
// This script controls the behaviour of the body part oscillator for the MiniGame #1.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Accuracy
{
    Perfect,
    Great,
    Okay,
    Terrible
}

public enum BodyPartType
{
    Head,
    Body,
    Legs
}

public enum Oscillation
{
    Left,
    Right
}

[System.Serializable]
public enum BodyType
{
    Jacked,
    Normal
}

[System.Serializable]
public class BodyPart : MonoBehaviour
{
    // Editor Fields
    public BodyPartType PartType;
    public Transform PerfectPosition;
    public float PerfectDistance, GreatDistance, OkayDistance;
    public BodyType BodyPartType;

    // Controls how fast the body part oscilates back and forth between the bed. Effectively changes the difficulty of that particular body part.
    public float OscillatorSpeed;


    // Controls how far the body part goes down. Just a way to tell the oscillator where to stop. Also, the oscillator assumes that the starting position is wherever the Game Object is located at Scene load.
    public float EndingLocation;

    // Private Fields
    private Vector2 m_StartingLocation;
    private Vector2 m_EndingLocation;
    private Oscillation m_CurrentDirection;
    private bool m_PerformedFirstTime = false;

    // Public Fields
    public bool Activated = false;


    private void Awake()
    {

    }

    private void Update()
    {
        if (!Activated)
            return;

        if(Activated && !m_PerformedFirstTime)
        {
            m_StartingLocation = transform.position;
            m_EndingLocation = new Vector2(transform.position.x+EndingLocation, transform.position.y);
            m_CurrentDirection = Oscillation.Right;
            m_PerformedFirstTime = true;
        }

        switch (m_CurrentDirection)
        {
        case Oscillation.Left:
                transform.position = new Vector3(transform.position.x - OscillatorSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                if(transform.position.x < m_StartingLocation.x)
                {
                    transform.position = new Vector3(m_StartingLocation.x, transform.position.y, transform.position.z);
                    m_CurrentDirection = Oscillation.Right;
                }
                break;
        case Oscillation.Right:
                transform.position = new Vector3(transform.position.x + OscillatorSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                if(transform.position.x > m_EndingLocation.x)
                {
                    transform.position = new Vector3(m_EndingLocation.x, transform.position.y, transform.position.z);
                    m_CurrentDirection = Oscillation.Left;
                }
                break;
        default:
                Debug.Log("Error! Oscillator variable m_CurrentDirection is not set to a proper Oscillation value?");
                return;
        }
    }

    public Accuracy GetAccuracy()
    {
        float dist = Vector2.Distance(transform.position, PerfectPosition.position);

        if (dist <= PerfectDistance) return Accuracy.Perfect;
        else if (dist <= GreatDistance) return Accuracy.Great;
        else if (dist <= OkayDistance) return Accuracy.Okay;
        else return Accuracy.Terrible;
    }
}
