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
    Up,
    Down
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
    public Vector2 PerfectPosition;
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
            m_EndingLocation = new Vector2(transform.position.x, transform.position.y - EndingLocation);
            m_CurrentDirection = Oscillation.Down;
            m_PerformedFirstTime = true;
        }

        switch (m_CurrentDirection)
        {
        case Oscillation.Up:
                transform.position = new Vector3(transform.position.x, transform.position.y + OscillatorSpeed * Time.deltaTime, transform.position.z);

                if(transform.position.y > m_StartingLocation.y)
                {
                    transform.position = new Vector3(transform.position.x, m_StartingLocation.y, transform.position.z);
                    m_CurrentDirection = Oscillation.Down;
                }
                break;
        case Oscillation.Down:
                transform.position = new Vector3(transform.position.x, transform.position.y - OscillatorSpeed * Time.deltaTime, transform.position.z);

                if(transform.position.y < m_EndingLocation.y)
                {
                    transform.position = new Vector3(transform.position.x, m_EndingLocation.y, transform.position.z);
                    m_CurrentDirection = Oscillation.Up;
                }
                break;
        default:
                Debug.Log("Error! Oscillator variable m_CurrentDirection is not set to a proper Oscillation value?");
                return;
        }
    }

    public Accuracy GetAccuracy()
    {
        float dist = Vector2.Distance(transform.position, PerfectPosition);

        if (dist <= PerfectDistance) return Accuracy.Perfect;
        else if (dist <= GreatDistance) return Accuracy.Great;
        else if (dist <= OkayDistance) return Accuracy.Okay;
        else return Accuracy.Terrible;
    }
}
