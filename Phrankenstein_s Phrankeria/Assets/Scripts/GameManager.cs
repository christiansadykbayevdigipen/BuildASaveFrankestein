// Author: Christian Sadykbayev
// This script handles the behaviour of the game. This script contains locations for each seperate mini game, since all of the minigames will be in the same scene. This is so that there is seamlessness between each minigame, and there isn't any hiccups trying to copy information over scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Editor Fields
    public Vector2 MiniGame1CameraLocation;
    public MG1 MiniGame1;
    public Customer Customer1;

    // Private Fields
    private bool m_PlayingMinigames;

    private void Awake()
    {
        Customer1.State = CustomerState.Walking;
        m_PlayingMinigames = false;
    }

    private void Update()
    {
        if(Customer1.State == CustomerState.Waiting && !m_PlayingMinigames)
        {
            m_PlayingMinigames = true;

            Camera.main.transform.position = MiniGame1CameraLocation;
            MiniGame1.StartMinigame();
        }
    }
}
