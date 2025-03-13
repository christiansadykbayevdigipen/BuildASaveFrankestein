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
    Received
}

public class Customer : MonoBehaviour
{
    // Editor Fields

    // The end point that the customer walks to. They will then stop at this position
    public GameObject EndPoint;

    public float CustomerWalkSpeed;

    public TMP_Text CustomerSpeech;

    // Private Fields
    private CustomerState m_State;

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
            StartCoroutine(Typewriter("Hi, can I get a Phrankenstein with double Phrankey's, hold the Phrankenmayo?", 0.08f));
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
    }

    private int m_TypewriterIndex = 0;
    private IEnumerator Typewriter(string toDisplay, float delayBetweenChars)
    {
        while(true)
        {
            if (m_TypewriterIndex >= toDisplay.Length)
            {
                m_State = CustomerState.Ask;
                yield break;
            }

            CustomerSpeech.text += toDisplay.ToCharArray()[m_TypewriterIndex];

            m_TypewriterIndex++;
            yield return new WaitForSeconds(delayBetweenChars);

        }

    }

}
