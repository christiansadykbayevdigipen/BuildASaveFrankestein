// Author: Christian Sadykbayev
// I mean, just a play button script. Nothing really special here.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Store Setting Final");
    }
}
