// Author: Christian Sadykbayev
// This script handles the behaviour of the game. This script contains locations for each seperate mini game, since all of the minigames will be in the same scene. This is so that there is seamlessness between each minigame, and there isn't any hiccups trying to copy information over scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Editor Fields
    public Vector2 StoreLocation;
    public Vector2 MiniGame1CameraLocation;
    public MG1 MiniGame1;
    public Customer Customer1;

    // Private Fields
    private bool m_PlayingMinigames;
    private int m_Strikes;
    private bool m_AlreadyPrintedFailMsg;

    private void Awake()
    {
        Customer1.State = CustomerState.Walking;
        m_PlayingMinigames = false;
        m_Strikes = 0;
        m_AlreadyPrintedFailMsg = false;
    }

    private void Update()
    {
        if(m_Strikes >= 3)
        {
            if(!m_AlreadyPrintedFailMsg)
            {
                Debug.Log("You Failed!!! Bye!");
                m_AlreadyPrintedFailMsg = true;
            }
            return;
        }

        if(Customer1.State == CustomerState.Waiting && !m_PlayingMinigames)
        {
            m_PlayingMinigames = true;

            Camera.main.transform.position = MiniGame1CameraLocation;
            MiniGame1.StartMinigame();
        }

        if(MiniGame1.IsComplete() && m_PlayingMinigames)
        {
            Camera.main.transform.position = StoreLocation;
            m_PlayingMinigames = false;
            if (MiniGame1.GetWinState())
            {
                Debug.Log("Successfully completed Customer's order! Points alloted: " + MiniGame1.Points);
                Customer1.State = CustomerState.Received;
            }
            else
            {
                Debug.Log("Terrible job! If you mess up three times, you will be demoted to customer!");
                m_Strikes++;
            }
        }
    }
}
