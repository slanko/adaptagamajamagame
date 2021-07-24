using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public bool shove = false;
    public PlayerGod player = null;
    public Vector2 moveVals = Vector2.zero;

    float shoveStartTime;
    private void Start()
    {
        player = GetComponent<PlayerGod>();
    }
    private void Update()
    {
        CheckInputStartTime();
    }

    public void Move(CallbackContext context)
    {
        Vector2 moveVals = context.ReadValue<Vector2>();
        if (player != null)
        {
            this.moveVals = moveVals;
        }
    }

    public void Shove(CallbackContext context)
    {
        if (context.performed)
        {
            shove = true;
            shoveStartTime = Time.time;
            Debug.Log("Attempting Shove");
        }
    }

    public void useShove()
    {
        shove = false;
    }

    void CheckInputStartTime()
    {
        if(Time.time > shoveStartTime + 0.2f)
        {
            shove = false;
        }
    }
}
