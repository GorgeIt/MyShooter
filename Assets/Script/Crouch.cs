using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Crouch : MonoBehaviour
{

    private CharacterController m_CharacterController;
    private bool m_Crouch = false;
    private float m_OriginalHeight;
    [SerializeField] private float m_CrouchHeight = 0.5f;
    private KeyCode crouchKey = KeyCode.LeftControl;
    public FirstPersonController controller;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_OriginalHeight = m_CharacterController.height;
    }
    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            m_Crouch = !m_Crouch;
            CheckCrouch();
        }
        if (!Input.GetKey(crouchKey) && m_Crouch)
        {
            m_Crouch = !m_Crouch;
            CheckCrouch();
        }

    }
    void CheckCrouch()
    {
        if (m_Crouch == true)
        {
            m_CharacterController.height = m_CrouchHeight;
            controller.m_WalkSpeed = 1f;
        }
        else
        {
            m_CharacterController.height = m_OriginalHeight;
            controller.m_WalkSpeed = 5f;
        }
    }
}