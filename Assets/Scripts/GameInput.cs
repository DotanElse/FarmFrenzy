using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputControls playerInputControls;
    void Awake()
    {
        playerInputControls = new PlayerInputControls();
        playerInputControls.Player.Enable();

        playerInputControls.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementsVector()
    {
        Vector2 inputVector = playerInputControls.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
