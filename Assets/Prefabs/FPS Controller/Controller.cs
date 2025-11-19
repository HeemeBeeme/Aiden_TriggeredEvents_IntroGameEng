using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.UI;



#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]


public class Controller : MonoBehaviour
{
    
    public static Controller Instance { get; protected set; }

    public Camera MainCamera;

    public Transform CameraPosition;

    public UnityEngine.UI.Slider SprintStamina;
    public GameObject PlayUI;
    public GameObject PauseMenu;
    public GameObject PauseTitle;
    public GameObject SettingsMenu;

    [Header("Control Settings")]
    public float MouseSensitivity = 100.0f;
    public float PlayerSpeed = 5.0f;
    public float RunningSpeed = 7.0f;
    public float JumpSpeed = 5.0f;
    public float Stamina = 100.0f;

    float m_VerticalSpeed = 0.0f;
    public bool m_IsPaused = false;

    float m_VerticalAngle, m_HorizontalAngle;
    public float Speed { get; private set; } = 0.0f;

    public float StaminaRefillDelay = 0;

    public bool LockControl { get; set; }
    public bool CanPause { get; set; } = true;

    public bool RecentlyRan = false;

    public bool HasStamina = true;

    public bool InSettings = false;

    public bool Grounded => m_Grounded;

    CharacterController m_CharacterController;

    bool m_Grounded;
    float m_GroundedTimer;
    float m_SpeedAtJump = 0.0f;

    

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SprintStamina.value = Stamina;

        m_IsPaused = false;
        m_Grounded = true;

        MainCamera.transform.SetParent(CameraPosition, false);
        MainCamera.transform.localPosition = Vector3.zero;
        MainCamera.transform.localRotation = Quaternion.identity;
        m_CharacterController = GetComponent<CharacterController>();

        m_VerticalAngle = 0.0f;
        m_HorizontalAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        MouseSensitivity = GameManager.Instance.MouseSensitivity;

        bool wasGrounded = m_Grounded;
        bool loosedGrounding = false;

        //we define our own grounded and not use the Character controller one as the character controller can flicker
        //between grounded/not grounded on small step and the like. So we actually make the controller "not grounded" only
        //if the character controller reported not being grounded for at least .5 second;
        if (!m_CharacterController.isGrounded)
        {
            if (m_Grounded)
            {
                m_GroundedTimer += Time.deltaTime;
                if (m_GroundedTimer >= 0.5f)
                {
                    loosedGrounding = true;
                    m_Grounded = false;
                }
            }
        }
        else
        {
            m_GroundedTimer = 0.0f;
            m_Grounded = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!m_IsPaused)
            {
                if(!InSettings)
                m_IsPaused = true;
                PlayUI.SetActive(false);
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (m_IsPaused)
            {
                if (!InSettings)
                {
                    m_IsPaused = false;
                    PlayUI.SetActive(true);
                    PauseMenu.SetActive(false);
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            else if (m_IsPaused)
            {
                if(InSettings)
                {
                    SettingsMenu.SetActive(false);
                    PauseTitle.SetActive(true);
                }
            }

            
        }

        Speed = 0;
        Vector3 move = Vector3.zero;
        if (!m_IsPaused && !LockControl)
        {

            if (m_Grounded)
            {
                m_VerticalSpeed = JumpSpeed;
                m_Grounded = false;
                loosedGrounding = true;                
            }

            if(Stamina > 0)
            {
                HasStamina = true;
            }
            else if(Stamina <= 0)
            {
                Stamina = 0;
                HasStamina = false;
            }

                bool running = Input.GetKey(KeyCode.LeftShift);
            float actualSpeed = running && HasStamina ? RunningSpeed : PlayerSpeed;

            if(running)
            {
                Stamina -= 0.3f;
                SprintStamina.value = Stamina;
                RecentlyRan = true;
            }
            else if(!running)
            {
                if (RecentlyRan)
                {
                    StaminaRefillDelay += Time.deltaTime;

                    if(StaminaRefillDelay >= 5f)
                    {
                        RecentlyRan = false;
                        StaminaRefillDelay = 0f;
                    }
                }
                else if(!RecentlyRan)
                {
                    Stamina += 0.1f;
                    SprintStamina.value = Stamina;

                    if(Stamina >= 100)
                    {
                        Stamina = 100;
                    }
                }
            }

            if (loosedGrounding)
            {
                m_SpeedAtJump = actualSpeed;
            }

            // Move around with WASD
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (move.sqrMagnitude > 1.0f)
                move.Normalize();

            float usedSpeed = m_Grounded ? actualSpeed : m_SpeedAtJump;

            move = move * usedSpeed * Time.deltaTime;

            move = transform.TransformDirection(move);
            m_CharacterController.Move(move);

            // Turn player
            float turnPlayer = Input.GetAxis("Mouse X") * MouseSensitivity;
            m_HorizontalAngle = m_HorizontalAngle + turnPlayer;

            if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
            if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;

            Vector3 currentAngles = transform.localEulerAngles;
            currentAngles.y = m_HorizontalAngle;
            transform.localEulerAngles = currentAngles;

            // Camera look up/down
            var turnCam = -Input.GetAxis("Mouse Y");
            turnCam = turnCam * MouseSensitivity;
            m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
            currentAngles = CameraPosition.transform.localEulerAngles;
            currentAngles.x = m_VerticalAngle;
            CameraPosition.transform.localEulerAngles = currentAngles;



            Speed = move.magnitude / (PlayerSpeed * Time.deltaTime);



            // Fall down / gravity
            m_VerticalSpeed = m_VerticalSpeed - 10.0f * Time.deltaTime;
            if (m_VerticalSpeed < -10.0f)
                m_VerticalSpeed = -10.0f; // max fall speed
            var verticalMove = new Vector3(0, m_VerticalSpeed * Time.deltaTime, 0);
            var flag = m_CharacterController.Move(verticalMove);
            if ((flag & CollisionFlags.Below) != 0)
                m_VerticalSpeed = 0;

            if (!wasGrounded && m_Grounded)
            {
                //play a sound after being in air and landing
            }
        }

    }
}
