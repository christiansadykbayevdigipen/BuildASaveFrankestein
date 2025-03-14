// Author: Christian Sadykbayev
// This script covers the behaviour of each customer.
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CustomerState
{
    None,
    Walking,
    Talking,
    Ask,
    Waiting,
    Received,
    RemoveYourself, // Basically removes the customer and spawns a new customer in
    Angry
}

public class Customer : MonoBehaviour
{
    // Editor Fields

    // The end point that the customer walks to. They will then stop at this position
    public GameObject EndPoint;

    public float CustomerWalkSpeed;

    public TMP_Text CustomerSpeech;

    public Vector2 StartingPosition;

    // Affects the speed at which speech is displayed to the screen
    public float SpeechCharacterDelay; 

    // Private Fields
    private CustomerState m_State;
    private float m_CurrentCharacterDelay;

    // Public Fields
    public CustomerState State
    {
        get { return m_State; }
        set { m_State = value; }
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if(m_State == CustomerState.Walking)
        {
            if(transform.position.x <= EndPoint.transform.position.x)
            {
                m_State = CustomerState.Talking;
                return;
            }

            transform.position = new Vector3(transform.position.x - CustomerWalkSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        }

        if(m_State == CustomerState.Talking)
        {
            StartCoroutine(Typewriter("Hi, can I get a Phrankenstein with double Phrankey's, hold the Phrankenmayo?", 0.08f, CustomerState.Ask));
            m_State = CustomerState.None;
        }

        if(m_State == CustomerState.Ask)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                m_State = CustomerState.Waiting;
                CustomerSpeech.text = "";
            }
        }

        if(m_State == CustomerState.Received)
        {
            StartCoroutine(Typewriter("Thank's for doing my order man! Pleasure doing business with you.", 0.08f, CustomerState.RemoveYourself));
            m_State = CustomerState.None;
        }

        if(m_State == CustomerState.RemoveYourself)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                transform.position = StartingPosition;
                CustomerSpeech.text = "";
                m_State = CustomerState.Walking;
            }
        }

        if(m_State == CustomerState.Angry)
        {
            StartCoroutine(Typewriter("What is this? This is nothing like what I ordered! I ordered an intact Phrankenstein, instead I got whatever this is! I am DONE!", 0.02f, CustomerState.RemoveYourself));
            m_State = CustomerState.None;
        }

        if (Input.GetKeyDown(KeyCode.Return))
            m_CurrentCharacterDelay = 0;
    }

    private int m_TypewriterIndex = 0;
    private IEnumerator Typewriter(string toDisplay, float delayBetweenChars, CustomerState stateAfterFinished)
    {
        CustomerSpeech.text = "";
        m_CurrentCharacterDelay = delayBetweenChars;

        while (true)
        {
            if (m_TypewriterIndex >= toDisplay.Length)
            {
                m_State = stateAfterFinished;
                m_TypewriterIndex = 0;
                yield break;
            }

            CustomerSpeech.text += toDisplay.ToCharArray()[m_TypewriterIndex];

            m_TypewriterIndex++;
            yield return new WaitForSeconds(m_CurrentCharacterDelay);

        }

    }

}
