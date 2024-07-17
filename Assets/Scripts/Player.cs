using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set;}

    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField] private float speedModifier = 10f;
    [SerializeField] private float rotationModifier = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    float playerHeight = 2f;
    float playerRadius = 0.7f;
    private bool isWalking = false;
    private Vector3 lastMovement;

    private ClearCounter selectedCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    void Awake()
    {
        if(Instance = null)
            Debug.LogError("More than one player");
        Instance = this;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
            selectedCounter.Interact();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        float interactDistance = 2.5f;
        Vector2 inputVector = gameInput.GetMovementsVector();
        if(inputVector != Vector2.zero)
            lastMovement = new Vector3(inputVector.x, 0f, inputVector.y);

        if(Physics.Raycast(transform.position, lastMovement, out RaycastHit rayHit, interactDistance, countersLayerMask))
        {   
            if(rayHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }        
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementsVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = Time.deltaTime*speedModifier;

        if(CanMove(moveDir, moveDistance))
            transform.position += moveDir*moveDistance;
        else
        {
            //try move only in X
            Vector3 MoveX = new Vector3(moveDir.x, 0, 0).normalized;
            if(CanMove(MoveX, moveDistance))
                transform.position += MoveX*moveDistance;
            //try move only in X
            Vector3 MoveZ = new Vector3(0, 0, moveDir.z).normalized;
            if(CanMove(MoveZ, moveDistance))
                transform.position += MoveZ*moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotationModifier);
        isWalking = inputVector != Vector2.zero;
    }
    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        return !Physics.CapsuleCast(transform.position,
         transform.position + Vector3.up*playerHeight, playerRadius, moveDir, moveDistance);

    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs{
            selectedCounter = selectedCounter
            });
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
