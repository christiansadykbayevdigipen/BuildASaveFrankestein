// Author: Christian Sadykbayev
// This script covers the behaviour of each customer.
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Image CustomerSpeechBox;

    public Vector2 StartingPosition;

    // Affects the speed at which speech is displayed to the screen
    public float SpeechCharacterDelay; 

    // Private Fields
    private CustomerState m_State;
    private float m_CurrentCharacterDelay;
    private List<BodyType> m_Order;

    // Public Fields
    public CustomerState State
    {
        get { return m_State; }
        set { m_State = value; }
    }

    public List<BodyType> Order
    {
        get { return m_Order; }
    }

    private void Awake()
    {
        m_Order = new List<BodyType>();
    }

    private void Update()
    {
        if(CustomerSpeech.text == "")
        {
            CustomerSpeechBox.enabled = false;
        }
        else
        {
            CustomerSpeechBox.enabled = true;
        }

        if (m_State == CustomerState.Walking)
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
            m_Order.Clear();
            BodyType[] availableColors = { BodyType.Jacked, BodyType.Normal, BodyType.Robot, BodyType.Skeleton };

            for (int i = 0; i < 3; i++)
            {
                int randomint = Random.Range(0, 4);
                m_Order.Add(availableColors[randomint]);
            }

            string message = "Hello, can I get a Phrankenstein with a " + m_Order[0].ToString() + " head, a " + m_Order[1].ToString() + " body, and a " + m_Order[2].ToString() + " pair of legs?";

            //StartCoroutine(Typewriter("Hi, can I get a Phrankenstein with double Phrankey's, hold the Phrankenmayo? (" + m_Order[0].ToString() + ", " + m_Order[1].ToString() + ", " + m_Order[2].ToString() + ")", 0.08f, CustomerState.Ask));
            StartCoroutine(Typewriter(message, 0.08f, CustomerState.Ask));
            m_State = CustomerState.None;
        }

        if(m_State == CustomerState.Ask)
        {
            //if(Input.GetKeyDown(KeyCode.Return))
            if(Input.GetMouseButtonDown(0))
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
            //if(Input.GetKeyDown(KeyCode.Return))
            if(Input.GetMouseButtonDown(0))
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

        //if (Input.GetKeyDown(KeyCode.Return))
        if(Input.GetMouseButtonDown(0))
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
