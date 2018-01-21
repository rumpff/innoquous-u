using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorBlue : MonoBehaviour
{

    private GameManager m_gameManager;
    private BoxCollider m_boxCollider;
    private SpriteRenderer m_razorSprite;

    [SerializeField]
    private bool m_startingPosition;

    [SerializeField]
    private Color m_activeColor;
    [SerializeField]
    private Color m_inactiveColor;

    private float m_rotateSpeed;
    private float m_lerpSpeed = 10;

    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_boxCollider = GetComponent<BoxCollider>();
        m_razorSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (m_startingPosition)
        {
            if (m_gameManager.BlueSwitch == 0)
            { Active(); }
            else
            { Inactive(); }
        }
        else
        {
            if (m_gameManager.BlueSwitch == 1)
            { Active(); }
            else
            { Inactive(); }
        }

        m_razorSprite.transform.eulerAngles = new Vector3(0, 0, m_razorSprite.transform.eulerAngles.z + (Time.deltaTime * m_rotateSpeed));
    }

    private void Active()
    {
        m_boxCollider.enabled = true;
        m_rotateSpeed = Mathf.Lerp(m_rotateSpeed, 800, m_lerpSpeed * Time.deltaTime);
        m_razorSprite.color = Color.Lerp(m_razorSprite.color, m_activeColor, m_lerpSpeed * Time.deltaTime);
    }

    private void Inactive()
    {
        m_boxCollider.enabled = false;
        m_rotateSpeed = Mathf.Lerp(m_rotateSpeed, 0, m_lerpSpeed * Time.deltaTime);
        m_razorSprite.color = Color.Lerp(m_razorSprite.color, m_inactiveColor, m_lerpSpeed * Time.deltaTime);
    }
}
