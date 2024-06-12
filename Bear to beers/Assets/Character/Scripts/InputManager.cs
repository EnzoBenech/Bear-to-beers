using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.APiedActions aPied;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        aPied = playerInput.APied;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        aPied.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Dire au player de bouger en utilisant les valeurs de notre action de mouvement (APied)
        motor.ProcessMove(aPied.Mouvement.ReadValue<Vector2>());
    }

    // Assurez-vous que la m�thode est correctement orthographi�e
    void LateUpdate()
    {
        look.ProcessLook(aPied.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        aPied.Enable();
    }

    private void OnDisable()
    {
        aPied.Disable();
    }
}
