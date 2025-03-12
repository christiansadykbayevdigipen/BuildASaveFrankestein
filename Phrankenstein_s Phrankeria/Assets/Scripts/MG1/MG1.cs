// Author: Christian Sadykbayev
// This script controls the MG1 which is the oscillating body parts between the bed, and trying to align it perfectly.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG1 : MonoBehaviour
{
    // Editor Fields
    public BodyPart Head;
    public BodyPart Torso;
    public BodyPart Legs;

    /*These reward fields specify how many points to give when you get the respective accuracy level on a body part.*/
    
    public float PerfectReward;
    public float GreatReward;
    public float OkayReward;

    // Private Fields
    private BodyPart m_CurrentlySelectedBP;
    private float m_Points;

    // Public Fields
    public float Points
    {
        get { return m_Points; }
    }

    private void Awake()
    {
        // Start the Minigame.
        Head.Activated = true;
        m_CurrentlySelectedBP = Head;
    }

    /// <summary>
    /// For the current selected bodypart, tally up the points for it.
    /// </summary>
    public void TallyBodyPart()
    {
        switch(m_CurrentlySelectedBP.GetAccuracy())
        {
        case Accuracy.Perfect:
            m_Points += PerfectReward;
            break;
        case Accuracy.Great:
            m_Points += GreatReward;
            break;
        case Accuracy.Okay:
            m_Points += OkayReward;
            break;
        case Accuracy.Terrible:
            // Do nothing. No points for you!
            break;
        }
    }

    private void Update()
    {
        // Stops the oscillation of the body part
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_CurrentlySelectedBP.Activated = false;

            switch(m_CurrentlySelectedBP.PartType)
            {
            case BodyPartType.Head:
                    Debug.Log("The head bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    TallyBodyPart();
                    m_CurrentlySelectedBP = Torso;
                    m_CurrentlySelectedBP.Activated = true;

                    break;
            case BodyPartType.Body:
                    Debug.Log("The body bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    TallyBodyPart();
                    m_CurrentlySelectedBP = Legs;
                    m_CurrentlySelectedBP.Activated = true;
                    break;
                case BodyPartType.Legs:
                    // This is the last option! Don't switch to anything else...
                    TallyBodyPart();
                    Debug.Log("The legs bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    break;
                default:
                    Debug.Log("Error! BodyPartType variable m_CurrentlySelectedBP.PartType is not set to a proper BodyPartType value?");
                    return;
            }
        }
    }

    public bool GetWinState()
    {
        if(Head.GetAccuracy() != Accuracy.Terrible && Torso.GetAccuracy() != Accuracy.Terrible && Legs.GetAccuracy() != Accuracy.Terrible)
        {
            return true;
        }

        return false;
    }
}
