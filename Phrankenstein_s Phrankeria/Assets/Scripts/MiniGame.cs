using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public virtual void StartMinigame()
    {

    }

    public virtual void StopMinigame()
    {

    }

    public virtual bool GetWinState()
    {
        return false;
    }

    public virtual bool IsComplete()
    {
        return false;
    }
}
