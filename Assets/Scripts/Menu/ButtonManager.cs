using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private Canvas m_canvas;
    private RectTransform m_rectTransform;

    [SerializeField]
    private Button[] m_ButtonArray;

    private bool[] m_teleportIndex;

    private float m_buttonOffset;
    private float m_scrollSpeed, m_scrollTimer, m_maxScrollSpeed, m_minScrollSpeed;

    private void Awake()
    {
        m_minScrollSpeed = 0.6f;
        m_maxScrollSpeed = 0.1f;
        m_scrollTimer = 0;
    }

    private void Start()
    {
        m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        m_rectTransform = transform as RectTransform;
        m_teleportIndex = new bool[m_ButtonArray.Length];

        for (int i = 0; i < m_teleportIndex.Length; i++)
        {
            m_teleportIndex[i] = false;
        }

        m_buttonOffset = m_ButtonArray[0].RectTransform.sizeDelta.y + 4;

        MoveButtons(Mathf.FloorToInt(m_ButtonArray.Length / 2));
    }

    private void Update()
    {
        // Set the button position
        for (int i = 0; i < m_ButtonArray.Length; i++)
        {
            var pos = new Vector2(m_rectTransform.anchoredPosition.x, ((m_buttonOffset) * -i) + (m_ButtonArray.Length / 2 * m_buttonOffset) + m_rectTransform.anchoredPosition.y);

            if (m_teleportIndex[i] == true) // Button needs to teleport to new pos
            { m_ButtonArray[i].SetPosition(pos); }
            else // Button can lerp to new pos
            { m_ButtonArray[i].SetDestination(pos); }

            m_ButtonArray[i].Selected = (i == Mathf.FloorToInt(m_ButtonArray.Length / 2));

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                m_ButtonArray[i].Confirm();
                AudioManager.SFX.PlaySound(SFXManager.Sounds.MenuClick);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (m_scrollTimer <= 0)
            {
                if (m_scrollSpeed > m_maxScrollSpeed) { m_scrollSpeed = m_scrollSpeed * 0.6f; }

                MoveButtons(1);
                m_scrollTimer = m_scrollSpeed;
                AudioManager.SFX.PlaySound(SFXManager.Sounds.MenuSelect);
            }
            else
            {
                m_scrollTimer -= Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (m_scrollTimer <= 0)
            {
                if (m_scrollSpeed > m_maxScrollSpeed) { m_scrollSpeed = m_scrollSpeed * 0.6f; }

                MoveButtons(-1);
                m_scrollTimer = m_scrollSpeed;
                AudioManager.SFX.PlaySound(SFXManager.Sounds.MenuSelect);
            }
            else
            {
                m_scrollTimer -= Time.deltaTime;
            }
            
        }
        else { m_scrollSpeed = m_minScrollSpeed; m_scrollTimer = 0; }
    }

    private void MoveButtons(int amount)
    {
        int length = m_ButtonArray.Length;
        Button[] bArray = new Button[length];

        for (int i = 0; i < length; i++)
        {
            int newIndex = i + amount;
            bool teleport = false;

            if(newIndex > length - 1)
            {
                newIndex = (newIndex - (length));
                teleport = true;
            }

            if(newIndex < 0)
            {
                newIndex = (newIndex + (length));
                teleport = true;
            }

            bArray[newIndex] = m_ButtonArray[i];
            m_teleportIndex[newIndex] = teleport;
        }

        m_ButtonArray = bArray;
    }
}
