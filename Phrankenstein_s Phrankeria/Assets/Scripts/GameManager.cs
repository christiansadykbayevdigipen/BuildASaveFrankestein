// Author: Christian Sadykbayev
// This script handles the behaviour of the game. This script contains locations for each seperate mini game, since all of the minigames will be in the same scene. This is so that there is seamlessness between each minigame, and there isn't any hiccups trying to copy information over scenes.
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    CustomerInteraction,
    Minigame0Started,
    Minigame0Complete,
    Minigame1Started,
    Minigame1Complete,
    AllGamesDone,
    Failure
}

public class GameManager : MonoBehaviour
{
    // Editor Fields
    public Vector2 StoreLocation;
    public Vector2 MiniGame0CameraLocation;
    public Vector2 MiniGame1CameraLocation;
    public MG0 MiniGame0;
    public MG1 MiniGame1;
    public Customer Customer1;
    public TMP_Text InfoText;

    // Private Fields
    private int m_Strikes;
    private bool m_AlreadyPrintedFailMsg;
    private GameState m_CurrentState;

    private void Awake()
    {
        m_CurrentState = GameState.CustomerInteraction;
        Customer1.State = CustomerState.Walking;
        m_Strikes = 0;
        m_AlreadyPrintedFailMsg = false;
    }

    private void _InvokeFailure()
    {
        StartCoroutine(InformPlayer("Terrible job! If you mess up three times, you will be demoted to customer!", 2.0f));
        Customer1.State = CustomerState.Angry;
        m_Strikes++;
    }

    private void Update()
    {
        if(m_Strikes >= 3)
        {
            if(!m_AlreadyPrintedFailMsg)
            {
                //m_Strikes = 0;
                InfoText.text = "You have failed.";
                SceneManager.LoadScene("FailureScene");
                m_AlreadyPrintedFailMsg = true;
            }
            return;
        }

        if (Customer1.State == CustomerState.Waiting && m_CurrentState == GameState.CustomerInteraction)
        {
            print("Starting minigame 0");
            m_CurrentState = GameState.Minigame0Started;
            Camera.main.transform.position = MiniGame0CameraLocation;
            MiniGame0.GiveCustomerParameters(Customer1.Order[0], Customer1.Order[1], Customer1.Order[2]);
            MiniGame0.StartMinigame();
        }

        if(MiniGame0.IsComplete() && m_CurrentState == GameState.Minigame0Started)
        {
/*            if(!MiniGame0.GetWinState() && Customer1.State == CustomerState.Waiting)
            {
                print("Minigame 0 failure invoking failure");
                Camera.main.transform.position = StoreLocation;
                _InvokeFailure();
                m_CurrentState = GameState.CustomerInteraction;
                return;
            }*/

            print("Starting minigame 1");
            m_CurrentState = GameState.Minigame0Complete;
            m_CurrentState = GameState.Minigame1Started;
            Camera.main.transform.position = MiniGame1CameraLocation;

            MiniGame1.Head = GameObject.Instantiate(MiniGame0.Head);
            MiniGame1.Torso = GameObject.Instantiate(MiniGame0.Torso);
            MiniGame1.Legs = GameObject.Instantiate(MiniGame0.Legs);

            MiniGame0.StopMinigame();
            MiniGame1.StartMinigame();
        }

        if(MiniGame1.IsComplete() && m_CurrentState == GameState.Minigame1Started)
        {
            m_CurrentState = GameState.Minigame1Complete;
            m_CurrentState = GameState.AllGamesDone;
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.2f);

        m_CurrentState = GameState.CustomerInteraction;
        Camera.main.transform.position = StoreLocation;
        if (MiniGame1.GetWinState() && MiniGame0.GetWinState())
        {
            StartCoroutine(InformPlayer("Successfully completed Customer's order! Points alloted: " + MiniGame1.Points, 2.0f));
            Customer1.State = CustomerState.Received;
        }
        else
        {
            _InvokeFailure();
        }

        MiniGame1.StopMinigame();
        GameObject.Destroy(MiniGame1.Head.gameObject);
        GameObject.Destroy(MiniGame1.Torso.gameObject);
        GameObject.Destroy(MiniGame1.Legs.gameObject);
    }

    private IEnumerator InformPlayer(string text, float autoRemoveTime)
    {
        InfoText.text = text;

        yield return new WaitForSeconds(autoRemoveTime);

        InfoText.text = "";
    }
}
