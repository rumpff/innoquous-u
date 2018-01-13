using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public enum PlayerStates
    {
        inactive,
        normal,
        inFlipper
    }

    private GameObject m_modelPar;

    private GameManager m_gameManager;
    private SFXManager m_sfxManager;

    private Rigidbody m_rigidbody;

    private PlayerStates m_playerState;

    private float m_zRotation;
    private float m_rotationSpeed = 666;

    private float m_moveSpeed = 18;

    private float m_jumpSpeed = 12f;
    private float m_jumpTimer = 0;
    private float m_jumpMaxTime = 0.18f;

    private void Start()
    {
        m_modelPar = transform.GetChild(0).gameObject;

        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerState = PlayerStates.inactive;

        m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.LevelEnter);
    }

    private void Update()
    {
        Vector3 grav = Physics.gravity;

        switch (m_playerState)
        {
            case PlayerStates.inactive:
                m_rigidbody.useGravity = false;
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                { SetStateToNormal(); m_gameManager.GameState = GameManager.GameStates.PlayerAlive; }
                break;
            case PlayerStates.normal:
                m_rigidbody.useGravity = true;

                // Control the rotation of the player
                if (grav.x != 0) // Gravity is either left or right
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        m_zRotation += m_rotationSpeed * Time.deltaTime;
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        m_zRotation -= m_rotationSpeed * Time.deltaTime;
                    }

                    
                }
                else // Gravity is either up or down
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        m_zRotation += m_rotationSpeed * Time.deltaTime;
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        m_zRotation -= m_rotationSpeed * Time.deltaTime;
                    }
                }

                // Apply the rotation
                m_modelPar.transform.eulerAngles = new Vector3(0, 0, m_zRotation);
                break;

            case PlayerStates.inFlipper:
                m_rigidbody.useGravity = false;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                { SetStateToNormal(); }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                { m_gameManager.AddDimension(-1); }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                { m_gameManager.AddDimension(1); }

                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_gameManager.UIManager.SetDeathCouse("being a whimp");
            Kill();
        }
    }

    private void FixedUpdate()
    {
        Vector3 vel = m_rigidbody.velocity;
        Vector3 grav = Physics.gravity;
        switch (m_playerState)
        {
            case PlayerStates.inactive:
                m_rigidbody.velocity = new Vector3(0, 0, 0); // Reset the velocity of the movement's axis
                break;

            case PlayerStates.normal:
                if (grav.x != 0) // Gravity is either left or right
                {
                    Vector2 endVel = new Vector3(vel.x, 0, 0);

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        Vector3 moveVector = new Vector3(0, -m_moveSpeed * Mathf.Sign(grav.x), 0);
                        Ray ray = new Ray(transform.position, moveVector);

                        if (!Physics.Raycast(ray, transform.localScale.x / 2 + 0.001f))
                        {
                            endVel.y = moveVector.y;
                        }
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        Vector3 moveVector = new Vector3(0, m_moveSpeed * Mathf.Sign(grav.x), 0);
                        Ray ray = new Ray(transform.position, moveVector);

                        if (!Physics.Raycast(ray, transform.localScale.x / 2 + 0.001f))
                        {
                            endVel.y = moveVector.y;
                        }
                    }

                    if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
                    {
                        m_jumpTimer = m_jumpMaxTime;
                        m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.PlayerJump);
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && m_jumpTimer > 0)
                    {
                        endVel.x = (m_jumpSpeed * Mathf.Sign(-grav.x));
                        m_jumpTimer -= Time.deltaTime;
                    }
                    if (Input.GetKeyUp(KeyCode.UpArrow) && m_jumpTimer > 0)
                    {
                        m_jumpTimer = 0;
                    }

                    m_rigidbody.velocity = endVel;
                }
                else // Gravity is either up or down
                {
                    Vector2 endVel = new Vector3(0, vel.y, 0);

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        Vector3 moveVector = new Vector3(m_moveSpeed * Mathf.Sign(grav.y), 0, 0);
                        Ray ray = new Ray(transform.position, moveVector);

                        if (!Physics.Raycast(ray, transform.localScale.x / 2 + 0.001f))
                        {
                            endVel.x = moveVector.x;
                        }
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        Vector3 moveVector = new Vector3(-m_moveSpeed * Mathf.Sign(grav.y), 0, 0);
                        Ray ray = new Ray(transform.position, moveVector);

                        if (!Physics.Raycast(ray, transform.localScale.x / 2 + 0.001f))
                        {
                            endVel.x = moveVector.x;
                        }
                    }

                    if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
                    {
                        m_jumpTimer = m_jumpMaxTime;
                        m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.PlayerJump);
                    }
                    if(Input.GetKey(KeyCode.UpArrow) && m_jumpTimer > 0)
                    {
                        endVel.y = (m_jumpSpeed * Mathf.Sign(-grav.y));
                        m_jumpTimer -= Time.deltaTime;
                    }
                    if(Input.GetKeyUp(KeyCode.UpArrow) && m_jumpTimer > 0)
                    {
                        m_jumpTimer = 0;
                    }

                    m_rigidbody.velocity = endVel;
                }
                break;

            case PlayerStates.inFlipper:
                // Check for activation of jump, the rest is handled in the normal state
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    m_jumpTimer = m_jumpMaxTime;
                    m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.PlayerJump);
                }
                break;
        }
    }

    public void SetStateToNormal()
    {
        m_rigidbody.useGravity = true;
        m_playerState = PlayerStates.normal;
    }

    public void Kill()
    {
        m_gameManager.GameState = GameManager.GameStates.PlayerDead;
        m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.PlayerDeath);
        gameObject.SetActive(false);
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Physics.gravity);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, transform.localScale.x / 2 + 0.001f);
        if (hit.transform == null) { return false; }
        return (hit.transform.tag == "Platform");
    }

    public PlayerStates PlayerState
    {
        get { return m_playerState; }
        set { m_playerState = value; }
    }
}
