// Author: Christian Sadykbayev
// This script covers the behaviour of each customer.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    None,
    Walking,
    Waiting,
    Received
}

public class Customer : MonoBehaviour
{
    // Editor Fields

    // The end point that the customer walks to. They will then stop at this position
    public GameObject EndPoint;

    public float CustomerWalkSpeed;

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
                m_State = CustomerState.Waiting;
                return;
            }

            transform.position = new Vector3(transform.position.x - CustomerWalkSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        }

    }
}
