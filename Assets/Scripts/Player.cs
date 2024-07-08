using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedModifier = 10f;
    [SerializeField] private float rotationModifier = 10f;
    private bool isWalking = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player created");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        isWalking = inputVector != Vector2.zero;
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir*Time.deltaTime*speedModifier;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotationModifier);
        Debug.Log(moveDir);

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
