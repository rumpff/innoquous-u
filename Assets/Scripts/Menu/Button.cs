using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public enum ButtonType
    {
        Play,
        CustomLevel,
        LevelEditor,
        Help,
        Options,
        Credits,
        Satistics,
        SecretPoem,
        ExitGame
    }

    [SerializeField]
    private ButtonType m_buttonType;
    private Vector2 dest;
    private RectTransform m_rectTransform;
    private Image m_image;
    private Color m_activeColor, m_inactiveColor, m_errorColor, m_currentColor;

    private bool m_selected;

    void Awake ()
    {
        m_rectTransform = transform as RectTransform;
        m_image = GetComponent<Image>();
    }

    private void Start()
    {
        m_activeColor = new Color(1, 1, 1, 1);
        m_inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1);
        m_errorColor = new Color(1, 0, 0, 1);

        m_currentColor = m_activeColor;
    }

    void Update ()
    {
        m_rectTransform.anchoredPosition = Vector2.Lerp(m_rectTransform.anchoredPosition, dest, 10 * Time.deltaTime);

        if(m_selected)
        {
            m_currentColor = Color.Lerp(m_currentColor, m_activeColor, 6f * Time.deltaTime);
        }
        else
        {
            m_currentColor = m_inactiveColor;
        }

        m_image.color = m_currentColor;

    }

    public void SetDestination(Vector2 pos)
    {
        dest = pos;
    }

    public void SetPosition(Vector2 pos)
    {
        dest = pos;
        m_rectTransform.anchoredPosition = pos;
    }

    public RectTransform RectTransform
    {
        get { return m_rectTransform; }
    }

    public bool Selected
    {
        get { return m_selected; }
        set { m_selected = value; }
    }

    public void Confirm()
    {
        switch(m_buttonType)
        {
            default:
                m_currentColor = m_errorColor;
                break;
        }
    }
}
