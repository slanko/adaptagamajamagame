using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject[] armArray, legArray;
    public int armCount, legCount;
    [SerializeField] int armMax, legMax; // should be hard capped to 10 but hey shut up
    PlayerInputHandler input;

    private void Start()
    {
        input = gameObject.GetComponentInParent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (input.addArm)
        {
            input.UseArm();
            SwitchAnArm();
        }
        if (input.addLeg)
        {
            input.UseLeg();
            SwitchALeg();
        }
    }

    void SwitchAnArm()
    {
        armCount++;
        legCount--;
        if (armCount > armMax) armCount = armMax;
        if (legCount < 0) legCount = 0;
        armArray[armCount - 1].SetActive(true);
        legArray[legCount - 1].SetActive(false);
    }

    void SwitchALeg()
    {
        legCount++;
        armCount--;
        if (legCount > legMax) legCount = legMax;
        if (armCount < 0) armCount = 0;
        legArray[legCount - 1].SetActive(true);
        armArray[armCount - 1].SetActive(false);
    }


}
