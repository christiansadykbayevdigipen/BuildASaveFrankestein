// Author: Christian Sadykbayev
// This script controls the MiniGame0. Minigame0 is about collecting body parts before you have to arrange them. Just a fun little minigame that doesn't really have a losing state.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG0 : MiniGame
{
    // Editor Fields
    public List<BodyPart> BodyPartPool;
    
    // The distance the mouse has to be within a body part for it to register as a click.
    public float ClickDistance;

    // Public Fields
    public BodyPart Head;
    public BodyPart Torso;
    public BodyPart Legs;

    // Private Fields
    private bool m_MinigameRunning;

    private BodyType RequestedHead;
    private BodyType RequestedTorso;
    private BodyType RequestedLegs;

    public override void StartMinigame()
    {
        Head = null;
        Torso = null;
        Legs = null;
        m_MinigameRunning = true;
    }

    public void GiveCustomerParameters(BodyType HeadPart, BodyType TorsoPart, BodyType LegsPart)
    {
        RequestedHead = HeadPart;
        RequestedTorso = TorsoPart;
        RequestedLegs = LegsPart;
    }

    public override void StopMinigame()
    {
        m_MinigameRunning = false;
    }

    private void Update()
    {
        if (!m_MinigameRunning)
            return;

        // Left Click
        if(Input.GetMouseButtonDown(0))
        {
            foreach(BodyPart part in BodyPartPool)
            {
                Vector2 mpos = Input.mousePosition;
                mpos = Camera.main.ScreenToWorldPoint(mpos);

                if(Vector2.Distance(part.transform.position, mpos) <= ClickDistance)
                {
                    if(part.PartType == BodyPartType.Head)
                    {
                        Head = part;
                    }
                    if(part.PartType == BodyPartType.Body)
                    {
                        Torso = part;
                    }
                    if(part.PartType == BodyPartType.Legs)
                    {
                        Legs = part;
                    }
                } 
            }
        }


    }

    public override bool IsComplete()
    {
        if (m_debugOverriden)
            return true;

        if (Head != null && Torso != null && Legs != null)
        {
            return true;
        }

        return false;
    }

    public override bool GetWinState()
    {
        if (Head.BodyPartType == RequestedHead && Torso.BodyPartType == RequestedTorso && Legs.BodyPartType == RequestedLegs)
        {
            return true;
        }

        return false;
    }

    private bool m_debugOverriden = false;

    public void debug_OverrideComplete()
    {
        m_debugOverriden = true;
    }
}
