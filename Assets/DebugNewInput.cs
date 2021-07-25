using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DebugNewInput : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(InputSystem.devices.Count);
    }
}
