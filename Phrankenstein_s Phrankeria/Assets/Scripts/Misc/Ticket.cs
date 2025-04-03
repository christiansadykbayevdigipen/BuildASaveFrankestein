// Christian Sadykbayev
// This script covers the functionality of the ticket. The ticket essentially just shows the player what body parts they must use so that they do not forget.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    // Editor Fields
    public Image TicketSheet;
    public Image TicketHead;
    public Image TicketTorso;
    public Image TicketLegs;

    // 0 = Jacked, 1 = Normal, 2 = Robot, 3 = Skeleton

    public List<Sprite> HeadSprites;
    public List<Sprite> TorsoSprites;
    public List<Sprite> LegsSprites;

    private Dictionary<BodyType, Sprite> m_HeadSprites;
    private Dictionary<BodyType, Sprite> m_TorsoSprites;
    private Dictionary<BodyType, Sprite> m_LegsSprites;

    // Private Fields

    private bool m_Clicked = false;

    private void Start()
    {
        m_HeadSprites = new Dictionary<BodyType, Sprite>();
        m_TorsoSprites = new Dictionary<BodyType, Sprite>();
        m_LegsSprites = new Dictionary<BodyType, Sprite>();

        for (int i = 0; i < HeadSprites.Count; i++)
        {
            m_HeadSprites.Add((BodyType)i, HeadSprites[i]);
        }

        for (int i = 0; i < TorsoSprites.Count; i++)
        {
            m_TorsoSprites.Add((BodyType)i, TorsoSprites[i]);
        }

        for (int i = 0; i < LegsSprites.Count; i++)
        {
            m_LegsSprites.Add((BodyType)i, LegsSprites[i]);
        }
    }

    public void OnClick()
    {
        if(!m_Clicked)
        {
            _ShowTicket();
            m_Clicked = true;
        }
        else
        {
            _HideTicket();
            m_Clicked = false;
        }
    }

    private void _ShowTicket()
    {
        TicketSheet.gameObject.SetActive(true);
        TicketHead.sprite = m_HeadSprites[GameManager.MasterManager.Customer1.Order[0]];
        TicketTorso.sprite = m_TorsoSprites[GameManager.MasterManager.Customer1.Order[1]];
        TicketLegs.sprite = m_LegsSprites[GameManager.MasterManager.Customer1.Order[2]];
    }

    private void _HideTicket()
    {
        TicketSheet.gameObject.SetActive(false);
    }
}
