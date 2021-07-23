using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    
    public PlayerGod player = null;
    private void Start()
    {
        player = GetComponent<PlayerGod>();
    }

    public void Move(CallbackContext context)
    {
        Vector2 moveVals = context.ReadValue<Vector2>();
        if (player != null)
        {
            player.moveVal = moveVals;
        }
        else
        {
            print("player = null");
        }
    }
}
