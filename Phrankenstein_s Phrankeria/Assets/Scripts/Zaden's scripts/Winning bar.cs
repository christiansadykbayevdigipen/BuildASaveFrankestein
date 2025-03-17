//Zaden Fetrow
//The bar within the voltage mini game that you have to click

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Winningbar : MonoBehaviour
{
    //Public bools
    public Transform LinePoint;
    public float size = 1f;
    //Private bools


    //Edit points


    void Update()
    { 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win thing") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("collision");

        }
    }

    void OnDrawGizmos()
    {
        Color black = Color.black;
        Color green = Color.green;
        Color yellow = Color.yellow;
        Color red = Color.red;

        Vector3 topLeft = transform.position + new Vector3(-0.5f, 1, 0);
        Vector3 topRight = transform.position + new Vector3(0.5f, 1, 0);
        Vector3 bottom = transform.position + new Vector3(0, -1, 0);

        Vector3 greenTopLeft = (topLeft * 0.3f) + (topRight * 0.7f);
        Vector3 greenTopRight = (topLeft * 0.7f) + (topRight * 0.3f);
        Vector3 yellowOuterLeft = (topLeft * 0.1f) + (topRight * 0.9f);
        Vector3 yellowOuterRight = (topLeft * 0.9f) + (topRight * 0.1f);

#if UNITY_EDITOR
        Handles.color = yellow;
        FillTriangle(topLeft, yellowOuterLeft, greenTopLeft);
        FillTriangle(topRight, yellowOuterRight, greenTopRight);


        Handles.color = green;
        FillTriangle(greenTopLeft, greenTopRight, bottom);
#endif

        Gizmos.color = red;
        Gizmos.DrawLine(yellowOuterLeft, bottom);
        Gizmos.DrawLine(yellowOuterRight, bottom);

        Gizmos.color = black;
        Gizmos.DrawLine(topLeft, bottom);
        Gizmos.DrawLine(topRight, bottom);
        Gizmos.DrawLine(topLeft, topRight);
    }

#if UNITY_EDITOR
    void FillTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        Handles.DrawAAConvexPolygon(new Vector3[] { a, b, c });
    }
#endif
}

