using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public bool shovePlease = false;
    public bool pickPlease = false;
    public bool addArmPlease2 = false;
    public bool addLegPlease2 = false;
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
            shovePlease = true;
            shoveStartTime = Time.time;
            Debug.Log("Attempting Shove");
        }
    }

    public void Pick(CallbackContext context)
    {
        if (context.performed)
        {
            pickPlease = true;
            pickStartTime = Time.time;
            Debug.Log("Attempting Pick");
        }
    }

    public void AddArm(CallbackContext context)
    {
        if (context.performed)
        {
            addArmPlease2 = true;
            armStartTime = Time.time;
        }
    }
    public void AddLeg(CallbackContext context)
    {
        if (context.performed)
        {
            addLegPlease2 = true;
            legStartTime = Time.time;
        }
    }

    public void UseShove()
    {
        shovePlease = false;
    }
    public void UsePick()
    {
        pickPlease = false;
    }
    public void UseArm()
    {
        addArmPlease2 = false;
    }
    public void UseLeg()
    {
        addLegPlease2 = false;
    }

    void CheckInputStartTime()
    {
        if(Time.time >= shoveStartTime + 0.2f)
        {
            shovePlease = false;
        }
        if(Time.time >= pickStartTime + 0.2f)
        {
            pickPlease = false;
        }
        if (Time.time >= armStartTime + 0.2f)
        {
            addArmPlease2 = false;
        }
        if (Time.time >= legStartTime + 0.2f)
        {
            addLegPlease2 = false;
        }
    }
}
