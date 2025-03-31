// Author: Christian Sadykbayev
// This script controls the MG1 which is the oscillating body parts between the bed, and trying to align it perfectly.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MG1 : MiniGame
{
    // Editor Fields
    public Transform HeadStart;
    public Transform TorsoStart;
    public Transform LegsStart;

    public Transform HeadEnd;
    public Transform TorsoEnd;
    public Transform LegsEnd;

    public Transform PerfectHeadPosition;
    public Transform PerfectTorsoPosition;
    public Transform PerfectLegsPosition;

    public TMP_Text Feedback;

    /*These reward fields specify how many points to give when you get the respective accuracy level on a body part.*/

    public float PerfectReward;
    public float GreatReward;
    public float OkayReward;

    // Private Fields
    private BodyPart m_CurrentlySelectedBP;
    private float m_Points;
    private bool m_IsComplete = false;
    private bool m_IsRunning = false;
    private bool m_BreakFeedbackRoutineEarly = false;

    // Public Fields
    public BodyPart Head;
    public BodyPart Torso;
    public BodyPart Legs;

    // Public Fields
    public float Points
    {
        get { return m_Points; }
    }

    private void Awake()
    {
    }

    public override void StartMinigame()
    {
        /*-----Refactor later-----*/
        Head.transform.position = HeadStart.position;
        Torso.transform.position = TorsoStart.position;
        Legs.transform.position = LegsStart.position;

        Head.Activated = false;
        Torso.Activated = false;
        Legs.Activated = false;
        m_CurrentlySelectedBP = null;
        /*!-----Refactor later-----*/


        m_IsRunning = true;
        Head.transform.position = HeadStart.position;
        Torso.transform.position = TorsoStart.position;
        Legs.transform.position = LegsStart.position;

        Head.EndingLocation = Mathf.Abs(Vector2.Distance(HeadStart.position, HeadEnd.position));
        Torso.EndingLocation = Mathf.Abs(Vector2.Distance(TorsoStart.position, TorsoEnd.position));
        Legs.EndingLocation = Mathf.Abs(Vector2.Distance(LegsStart.position, LegsEnd.position));

        Head.PerfectPosition = PerfectHeadPosition;
        Torso.PerfectPosition = PerfectTorsoPosition;
        Legs.PerfectPosition = PerfectLegsPosition;


        // Start the Minigame.
        Head.Activated = true;
        m_CurrentlySelectedBP = Head;
        m_IsComplete = false;
        m_Points = 0;
    }

    public override void StopMinigame()
    {
        m_IsRunning = false;
        m_IsComplete = true;
    }

    /// <summary>
    /// For the current selected bodypart, tally up the points for it.
    /// </summary>
    public void TallyBodyPart()
    {
        if(Feedback.text != "")
        {
            m_BreakFeedbackRoutineEarly = true;
            Feedback.text = "";
        }

        StartCoroutine(GiveFeedback(m_CurrentlySelectedBP.GetAccuracy().ToString()));

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
        if (m_IsComplete) return;
        if (!m_IsRunning) return;
        

        // Stops the oscillation of the body part
        //if(Input.GetKeyDown(KeyCode.Space))
        if (Input.GetMouseButtonDown(0))
        {
            m_CurrentlySelectedBP.Activated = false;

            switch(m_CurrentlySelectedBP.PartType)
            {
            case BodyPartType.Head:
                    //Debug.Log("The head bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    TallyBodyPart();
                    m_CurrentlySelectedBP = Torso;
                    m_CurrentlySelectedBP.Activated = true;

                    break;
            case BodyPartType.Body:
                    //Debug.Log("The body bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    TallyBodyPart();
                    m_CurrentlySelectedBP = Legs;
                    m_CurrentlySelectedBP.Activated = true;
                    break;
                case BodyPartType.Legs:
                    // This is the last option! Don't switch to anything else...
                    TallyBodyPart();
                    //Debug.Log("The legs bodypart got an accuracy of: " + m_CurrentlySelectedBP.GetAccuracy().ToString());
                    m_IsComplete = true;
                    break;
                default:
                    Debug.Log("Error! BodyPartType variable m_CurrentlySelectedBP.PartType is not set to a proper BodyPartType value?");
                    return;
            }
        }
    }

    public override bool GetWinState()
    {
        if(Head.GetAccuracy() != Accuracy.Terrible && Torso.GetAccuracy() != Accuracy.Terrible && Legs.GetAccuracy() != Accuracy.Terrible)
        {
            return true;
        }

        return false;
    }

    public override bool IsComplete()
    {
        return m_IsComplete;
    }

    public IEnumerator GiveFeedback(string feedback)
    {
        Feedback.text = feedback;
        yield return new WaitForSeconds(1.0f);

        if(m_BreakFeedbackRoutineEarly)
        {
            m_BreakFeedbackRoutineEarly = false;
            yield break;
        }

        Feedback.text = "";
    }
}
