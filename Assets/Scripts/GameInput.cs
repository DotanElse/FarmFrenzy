using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputControls playerInputControls;
    public static GameInput Instance {get; private set;}
    void Awake()
    {
        Instance = this;
        Debug.Log("instance set");
        playerInputControls = new PlayerInputControls();
        playerInputControls.Player.Enable();

        playerInputControls.Player.Interact.performed += Interact_performed;
        playerInputControls.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputControls.Player.Pause.performed += Pause_performed;
    }

    void OnDestroy()
    {
        playerInputControls.Player.Interact.performed -= Interact_performed;
        playerInputControls.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputControls.Player.Pause.performed -= Pause_performed;

        playerInputControls.Dispose();
    }
    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
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
