using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public bool shove = false;
    public bool pick = false;
    public bool addArm = false;
    public bool addLeg = false;
    public PlayerGod player = null;
    public Vector2 moveVals = Vector2.zero;

    float shoveStartTime;
    float pickStartTime;
    float armStartTime;
    float legStartTime;
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

    public void Pick(CallbackContext context)
    {
        if (context.performed)
        {
            pick = true;
            pickStartTime = Time.time;
            Debug.Log("Attempting Pick");
        }
    }

    public void AddArm(CallbackContext context)
    {
        if (context.performed)
        {
            addArm = true;
            armStartTime = Time.time;
        }
    }
    public void AddLeg(CallbackContext context)
    {
        if (context.performed)
        {
            addLeg = true;
            legStartTime = Time.time;
        }
    }

    public void UseShove()
    {
        shove = false;
    }
    public void UsePick()
    {
        pick = false;
    }
    public void UseArm()
    {
        addArm = false;
    }
    public void UseLeg()
    {
        addLeg = false;
    }

    void CheckInputStartTime()
    {
        if(Time.time >= shoveStartTime + 0.2f)
        {
            shove = false;
        }
        if(Time.time >= pickStartTime + 0.2f)
        {
            pick = false;
        }
        if (Time.time >= armStartTime + 0.2f)
        {
            addArm = false;
        }
        if (Time.time >= legStartTime + 0.2f)
        {
            addLeg = false;
        }
    }
}
